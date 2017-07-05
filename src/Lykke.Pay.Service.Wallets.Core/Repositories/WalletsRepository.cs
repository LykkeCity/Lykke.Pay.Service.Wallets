using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitcoint.Api.Client;
using Bitcoint.Api.Client.Models;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Signing.Api;
using Lykke.Signing.Api.Models;

namespace Lykke.Pay.Service.Wallets.Core.Repositories
{
    public class WalletsRepository : IWalletsRepository

    {
        private IBitcoinApi _bitcoinApi;
        private ILykkeSigningAPI _signinApi;
        public WalletsRepository(IBitcoinApi bitcoinApi, ILykkeSigningAPI signinApi)
        {
            _bitcoinApi = bitcoinApi;
            _signinApi = signinApi;
        }

        public async Task<IEnumerable<IWallet>> GetAllAsync()
        {
            var result = new List<string>();
            var multiSign = (await _bitcoinApi.ApiWalletAllGetWithHttpMessagesAsync()).Body as GetAllWalletsResult;
            var privWall = (await _signinApi.ApiBitcoinAddressesGetWithHttpMessagesAsync()).Body;
            if (multiSign != null)
            {
                result.AddRange(multiSign.Multisigs);
            }

            if (privWall != null)
            {
                result.AddRange(privWall.Addresses);
            }

            return (result.Distinct().Select(s => new Wallet {WalletAddress = s})).ToList();
        }
    }
}
