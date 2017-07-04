using System;

namespace Lykke.Pay.Service.Wallets.Client.Custom
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}