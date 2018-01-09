using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Lykke.Pay.Service.Wallets.Core.Domain;
using Lykke.Pay.Service.Wallets.Core.Services;

namespace Lykke.Pay.Service.Wallets.Services
{
    [UsedImplicitly]
    public class WalletsCacheService<TWallet> : IWalletsCacheService<TWallet> 
        where TWallet : IWallet
    {
        private List<TWallet> _items = new List<TWallet>();

        public void Update(IEnumerable<TWallet> items)
        {
            _items = items.ToList();
        }

        public IEnumerable<TWallet> GetLykkeWallets(IEnumerable<string> walletAddresses)
        {
            if (walletAddresses == null)
            {
                walletAddresses = new List<string>();
            }
            return (from i in _items
                join wa in walletAddresses on i.WalletAddress equals wa
                select i).ToList();
        }

       

       
        public void InsertNewAddress(TWallet wallet)
        {
            if (_items.All(i => !i.WalletAddress.Equals(wallet.WalletAddress)))
            {
                _items.Add(wallet);
            }
        }

        IReadOnlyList<TWallet> IWalletsCacheService<TWallet>.GetAll()
        {
            return _items;
        }
    }
}