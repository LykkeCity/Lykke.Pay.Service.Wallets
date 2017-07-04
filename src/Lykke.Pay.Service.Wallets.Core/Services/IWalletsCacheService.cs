using System.Collections.Generic;
using Lykke.Pay.Service.Wallets.Core.Domain;

namespace Lykke.Pay.Service.Wallets.Core.Services
{
    public interface IWalletsCacheService<TWallet>
        where TWallet : IWallet
    {
        void Update(IEnumerable<TWallet> item);
        IEnumerable<TWallet> GetLykkeWallets(IEnumerable<string> walletAddresses);
        IReadOnlyList<TWallet> GetAll();
        void InsertNewAddress(TWallet wallet);
    }
}