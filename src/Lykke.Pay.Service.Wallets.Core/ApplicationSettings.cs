﻿using System;
using JetBrains.Annotations;

namespace Lykke.Pay.Service.Wallets.Core
{
    [UsedImplicitly]
    public class ApplicationSettings
    {
        public WalletsSettings WalletsService { get; set; }

        public SlackNotificationsSettings SlackNotifications { get; set; }

        [UsedImplicitly]
        public class WalletsSettings
        {
            public DbSettings Db { get; set; }
            public TimeSpan CacheExpirationPeriod { get; set; }
            public RabbitSettings WalletFeedRabbit { get; set; }
            public RpcSettings Rpc { get; set; }
        }

       

        [UsedImplicitly]
        public class DbSettings
        {
            public string WalletConnectionString { get; set; }
            public string LogsConnectionString { get; set; }
        }

        [UsedImplicitly]
        public class RpcSettings
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string Network { get; set; }
        }

        [UsedImplicitly]
        public class SlackNotificationsSettings
        {
            public AzureQueueSettings AzureQueue { get; set; }

            public int ThrottlingLimitSeconds { get; set; }
        }

        [UsedImplicitly]
        public class AzureQueueSettings
        {
            public string ConnectionString { get; set; }

            public string QueueName { get; set; }
        }

        [UsedImplicitly]
        public class RabbitSettings
        {
            public string ConnectionString { get; set; }
            public string ExchangeName { get; set; }
        }
    }

    
}