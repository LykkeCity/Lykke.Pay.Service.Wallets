using System;
using JetBrains.Annotations;
using Lykke.Pay.Service.Wallets.Core.Services;

namespace Lykke.Pay.Service.Wallets.Services
{
    [UsedImplicitly]
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}