using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Pay.Service.Wallets.Core.Domain;

namespace Lykke.Pay.Service.Wallets.Core.Services
{
    public interface IWalletsManager<TWallet> 
        where TWallet : IWallet
    {
        Task<IEnumerable<TWallet>> GetLykkeWalletsAsync(IEnumerable<string> walletAddresses);
        Task<IEnumerable<TWallet>> GetAllAsync();
        void InsertNewAddress(TWallet wallet);
        Task UpdateCacheAsync();
        bool ValideteWallets(string walletAddress);
    }
}