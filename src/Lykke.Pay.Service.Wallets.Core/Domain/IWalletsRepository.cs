using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Pay.Service.Wallets.Core.Domain
{
    public interface IWalletsRepository
        
    {
        Task<IEnumerable<IWallet>> GetAllAsync();
    }
}