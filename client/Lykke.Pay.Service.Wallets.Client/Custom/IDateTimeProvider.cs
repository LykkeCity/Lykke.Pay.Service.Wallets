using System;

namespace Lykke.Pay.Service.Wallets.Client.Custom
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}