using System;
using System.Net;
using Autofac;
using AzureStorage.Tables;
using Common.Log;
using Lykke.AzureRepositories;
using Lykke.AzureRepositories.Log;
using Lykke.Core;
using Lykke.Pay.Service.Wallets.Core;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Repositories;
using Lykke.Pay.Service.Wallets.Core.Services;
using Lykke.Pay.Service.Wallets.Services;
using NBitcoin;
using NBitcoin.RPC;

namespace Lykke.Pay.Service.Wallets.DependencyInjection
{
    public class ApiModule : Module
    {
        private readonly ApplicationSettings _settings;
        private readonly ILog _log;

        public ApiModule(ApplicationSettings settings, ILog log)
        {
            _settings = settings;
            _log = log;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_log).SingleInstance();

            builder.RegisterInstance(_settings).SingleInstance();
            builder.RegisterInstance(_settings.WalletsService).SingleInstance();

            builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>();

            RegisterWallet(builder);

        }

        private void RegisterWallet(ContainerBuilder builder)
        {

            builder.RegisterType <WalletsBroker>()
                .As<IStartable>()
                .SingleInstance();

          
            builder.RegisterType<WalletsRepository>()
                .As<IWalletsRepository>()
                .SingleInstance();

            builder.RegisterType<WalletsCacheService<IWallet>>()
                .As<IWalletsCacheService<IWallet>>()
                .SingleInstance();

            builder.RegisterType<WalletsManager<IWallet>>()
                .As<IWalletsManager<IWallet>>()
                .WithParameter(new TypedParameter(typeof(TimeSpan), _settings.WalletsService.CacheExpirationPeriod))
                .SingleInstance();

            var repo = new MerchantWalletRepository(
                new AzureRepositories.Azure.Tables.AzureTableStorage<MerchantWalletEntity>(
                    _settings.WalletsService.Db.WalletConnectionString,
                    "MerchantWallets", new CommonLogAdapter(_log)));
            builder.RegisterInstance(repo)
                .As<IMerchantWalletRepository>()
                .SingleInstance();

            var client = new RPCClient(
                new NetworkCredential(_settings.WalletsService.Rpc.UserName,
                    _settings.WalletsService.Rpc.Password),
                new Uri(_settings.WalletsService.Rpc.Url), Network.GetNetwork(_settings.WalletsService.Rpc.Network));
            builder.RegisterInstance(client)
                .SingleInstance();
        }

       
    }
}