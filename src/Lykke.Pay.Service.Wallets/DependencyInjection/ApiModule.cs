using System;
using Autofac;
using Bitcoint.Api.Client;
using Common.Log;
using Lykke.Pay.Service.Wallets.Core;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Repositories;
using Lykke.Pay.Service.Wallets.Core.Services;
using Lykke.Pay.Service.Wallets.Services;
using Lykke.Signing.Api;

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

            builder.RegisterType<BitcoinApi>()
                .As<IBitcoinApi>()
                .WithParameter(new TypedParameter(typeof(Uri), new Uri(_settings.WalletsService.WalletList.BitcoinApiUrl)))
                .SingleInstance();

            builder.RegisterType<LykkeSigningAPI>()
                .As<ILykkeSigningAPI>()
                .WithParameter(new TypedParameter(typeof(Uri), new Uri(_settings.WalletsService.WalletList.LykkeSigningApiUrl)))
                .SingleInstance();

            builder.RegisterType<WalletsRepository>()
                .As<IWalletsRepository>()
                .SingleInstance();

            builder.RegisterType<WalletsCacheService<IWallet>>()
                .As<IWalletsCacheService<IWallet>>()
                .SingleInstance();

            builder.RegisterType<WalletsManager<IWallet>>()
                .As<IWalletsManager<IWallet>>()
                .WithParameter(new TypedParameter(typeof(TimeSpan), _settings.WalletsService.WalletList.CacheExpirationPeriod))
                .SingleInstance();
        }

       
    }
}