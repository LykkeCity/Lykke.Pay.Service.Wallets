using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using Common.Log;
using JetBrains.Annotations;
using Lykke.AzureQueueIntegration;
using Lykke.Common.ApiLibrary.Middleware;
using Lykke.Common.ApiLibrary.Swagger;
using Lykke.Logs;
using Lykke.Pay.Service.Wallets.Core;
using Lykke.Pay.Service.Wallets.DependencyInjection;
using Lykke.Pay.Service.Wallets.Models;
using Lykke.SettingsReader;
using Lykke.SlackNotification.AzureQueue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;

namespace Lykke.Pay.Service.Wallets
{
    [UsedImplicitly]
    public class Startup
    {
        private IContainer ApplicationContainer { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });

            services.AddSwaggerGen(options =>
            {
                options.DefaultLykkeConfiguration("v1", "Pay Wallet service");
            });

            var appSettings = Configuration.LoadSettings<ApplicationSettings>();

            var log = CreateLogWithSlack(services, appSettings);
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApiModule(appSettings.CurrentValue, log));
            builder.Populate(services);

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        
        private static ILog CreateLogWithSlack(IServiceCollection services, IReloadingManager<ApplicationSettings> appSettings)

        {

            var consoleLogger = new LogToConsole();

            var aggregateLogger = new AggregateLogger();



            aggregateLogger.AddLog(consoleLogger);



            // Creating slack notification service, which logs own azure queue processing messages to aggregate log

            var slackService = services.UseSlackNotificationsSenderViaAzureQueue(new AzureQueueIntegration.AzureQueueSettings

            {

                ConnectionString = appSettings.CurrentValue.WalletsService.Logs.DbConnectionString,

                QueueName = appSettings.CurrentValue.SlackNotifications.AzureQueue.QueueName

            }, aggregateLogger);



            var dbLogConnectionStringManager = appSettings.Nested(x => x.WalletsService.Logs.DbConnectionString);

            var dbLogConnectionString = dbLogConnectionStringManager.CurrentValue;



            // Creating azure storage logger, which logs own messages to concole log

            if (!string.IsNullOrEmpty(dbLogConnectionString) && !(dbLogConnectionString.StartsWith("${") && dbLogConnectionString.EndsWith("}")))

            {

                var persistenceManager = new LykkeLogToAzureStoragePersistenceManager(

                    AzureTableStorage<LogEntity>.Create(dbLogConnectionStringManager, "BitcoinTransactionAggregatorLog", consoleLogger),

                    consoleLogger);



                var slackNotificationsManager = new LykkeLogToAzureSlackNotificationsManager(slackService, consoleLogger);



                var azureStorageLogger = new LykkeLogToAzureStorage(

                    persistenceManager,

                    slackNotificationsManager,

                    consoleLogger);



                azureStorageLogger.Start();



                aggregateLogger.AddLog(azureStorageLogger);

            }



            return aggregateLogger;

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseLykkeMiddleware(Constants.ComponentName, ex => ErrorResponse.Create("Technical problem"));

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}