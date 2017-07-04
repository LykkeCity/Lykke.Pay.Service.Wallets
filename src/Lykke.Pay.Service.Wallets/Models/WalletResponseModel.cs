using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lykke.Pay.Service.Wallets.Core.Domain;

namespace Lykke.Pay.Service.Wallets.Models
{
    public class WalletResponseModel
    {
        public List<Wallet> Wallets { get; set; }

        public static WalletResponseModel Create(IEnumerable<IWallet> src)
        {
            return new WalletResponseModel()
            {
                Wallets = new List<Wallet>(src.Select(Wallet.Create))
            };
        }
    }
}