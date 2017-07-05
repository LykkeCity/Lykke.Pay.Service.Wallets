// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Pay.Service.Wallets.Client.Models
{
    using Lykke.Pay;
    using Lykke.Pay.Service;
    using Lykke.Pay.Service.Wallets;
    using Lykke.Pay.Service.Wallets.Client;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class WalletResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the WalletResponseModel class.
        /// </summary>
        public WalletResponseModel()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the WalletResponseModel class.
        /// </summary>
        public WalletResponseModel(IList<Wallet> wallets = default(IList<Wallet>))
        {
            Wallets = wallets;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Wallets")]
        public IList<Wallet> Wallets { get; set; }

    }
}
