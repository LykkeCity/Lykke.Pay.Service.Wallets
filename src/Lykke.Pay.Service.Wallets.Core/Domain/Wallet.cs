namespace Lykke.Pay.Service.Wallets.Core.Domain
{
    public class Wallet : IWallet
    {
        
        public string WalletAddress { get; set; }

        public static Wallet Create(IWallet src)
        {
            return new Wallet
            {
                WalletAddress = src.WalletAddress
            };
        }

        
    }
}