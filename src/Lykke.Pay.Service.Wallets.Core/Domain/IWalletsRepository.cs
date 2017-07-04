using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Pay.Service.Wallets.Core.Domain
{
    public interface IWalletsRepository<TWallet>
        where TWallet : IWallet
    {
        Task<IEnumerable<TWallet>> GetAllAsync();
    }
}