// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Bitcoint.Api.Client.Models
{
    using Bitcoint.Api;
    using Bitcoint.Api.Client;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class MultisigBalanceInfo
    {
        /// <summary>
        /// Initializes a new instance of the MultisigBalanceInfo class.
        /// </summary>
        public MultisigBalanceInfo()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MultisigBalanceInfo class.
        /// </summary>
        public MultisigBalanceInfo(string multisig = default(string), decimal? clientAmount = default(decimal?), decimal? hubAmount = default(decimal?))
        {
            Multisig = multisig;
            ClientAmount = clientAmount;
            HubAmount = hubAmount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "multisig")]
        public string Multisig { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "clientAmount")]
        public decimal? ClientAmount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hubAmount")]
        public decimal? HubAmount { get; set; }

    }
}
