using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.AzureRepositories;
using Lykke.Core;
using Lykke.Pay.Service.Wallets.Core.Domain;


namespace Lykke.Pay.Service.Wallets.Core.Repositories
{
    public class WalletsRepository : IWalletsRepository

    {
        private readonly IMerchantWalletRepository _walletRepo;

        public WalletsRepository(IMerchantWalletRepository walletRepo)
        {
            _walletRepo = walletRepo;
        }

        public async Task<IEnumerable<IWallet>> GetAllAsync()
        {
            var result = await _walletRepo.GetAllAddressAsync();
            return (from w in result
                    select  w.WalletAddress).Distinct().Select(wa=>new Wallet{WalletAddress = wa}).ToList();
        }
    }
}
