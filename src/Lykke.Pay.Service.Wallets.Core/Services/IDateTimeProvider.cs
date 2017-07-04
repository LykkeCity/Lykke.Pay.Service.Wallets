using System;

namespace Lykke.Pay.Service.Wallets.Core.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}