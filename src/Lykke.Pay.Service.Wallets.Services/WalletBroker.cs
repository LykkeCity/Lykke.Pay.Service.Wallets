﻿using System;
using System.Threading.Tasks;
using Autofac;
using Common.Log;
using Lykke.Pay.Service.Wallets.Core;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Services;
using Lykke.Pay.Service.Wallets.Services.RabbitMq;
using Lykke.RabbitMqBroker.Subscriber;

namespace Lykke.Pay.Service.Wallets.Services
{
    public class WalletsBroker :
        IStartable,
        IDisposable
    {
        private readonly ILog _log;
        private readonly IWalletsManager<IWallet> _walletsManager;
        private readonly ApplicationSettings.WalletsSettings _settings;
        private RabbitMqSubscriber<WalletNotofication> _subscriber;

        public WalletsBroker(ILog log, IWalletsManager<IWallet> walletsManager, ApplicationSettings.WalletsSettings settings)
        {
            _log = log;
            _walletsManager = walletsManager;
            _settings = settings;
        }

        public void Start()
        {
            try
            {
                _subscriber = new RabbitMqSubscriber<WalletNotofication>(new RabbitMqSubscriberSettings
                    {
                        ConnectionString = _settings.WalletList.WalletFeedRabbit.ConnectionString,
                        QueueName = $"{_settings.WalletList.WalletFeedRabbit.ExchangeName}.wallethash",
                        ExchangeName = _settings.WalletList.WalletFeedRabbit.ExchangeName,
                        IsDurable = true
                    })
                    .SetMessageDeserializer(new JsonMessageDeserializer<WalletNotofication>())
                    .SetMessageReadStrategy(new MessageReadWithTemporaryQueueStrategy())
                    .Subscribe(ProcessQuoteAsync)
                    .SetLogger(_log)
                    .Start();
            }
            catch (Exception ex)
            {
                _log.WriteErrorAsync(Constants.ComponentName, null, null, ex).Wait();
                throw;
            }
        }

        private async Task ProcessQuoteAsync(WalletNotofication quote)
        {
            try
            {
                _walletsManager.InsertNewAddress(new Wallet{ WalletAddress = quote.Address});
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(Constants.ComponentName, null, null, ex);
            }
        }

        public void Dispose()
        {
            _subscriber.Stop();
        }
    }
}