// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Signing.Api.Models
{
    using Lykke.Signing;
    using Lykke.Signing.Api;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class PubKeyResponse
    {
        /// <summary>
        /// Initializes a new instance of the PubKeyResponse class.
        /// </summary>
        public PubKeyResponse()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PubKeyResponse class.
        /// </summary>
        public PubKeyResponse(string pubKey = default(string), string address = default(string))
        {
            PubKey = pubKey;
            Address = address;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pubKey")]
        public string PubKey { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

    }
}
