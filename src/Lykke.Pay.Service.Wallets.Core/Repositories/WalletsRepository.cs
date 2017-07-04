using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitcoint.Api.Client;
using Bitcoint.Api.Client.Models;
using Lykke.Pay.Service.Wallets.Core.Domain;

namespace Lykke.Pay.Service.Wallets.Core.Repositories
{
    public class WalletsRepository : IWalletsRepository

    {
        private IBitcoinApi _bitcoinApi;
        public WalletsRepository(IBitcoinApi bitcoinApi)
        {
            _bitcoinApi = bitcoinApi;
        }
        public async Task<IEnumerable<IWallet>> GetAllAsync()
        {
            var result = await _bitcoinApi.ApiWalletAllGetWithHttpMessagesAsync();
            var wallets = result.Body as GetAllWalletsResult;
            var ret = new List<IWallet>();
            if (wallets != null)
            {
                ret.AddRange(wallets.Multisigs.Select(w=>new Wallet{WalletAddress = w} as IWallet));
            }
            return ret;
        }
    }
}
