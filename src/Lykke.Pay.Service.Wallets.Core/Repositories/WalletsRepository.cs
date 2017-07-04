using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Pay.Service.Wallets.Core.Domain;

namespace Lykke.Pay.Service.Wallets.Core.Repositories
{
    public class WalletsRepository<TWallet> : IWalletsRepository<TWallet>
        where TWallet : IWallet
    {
        public async Task<IEnumerable<TWallet>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
