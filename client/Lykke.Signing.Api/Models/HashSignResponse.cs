// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Signing.Api.Models
{
    using Lykke.Signing;
    using Lykke.Signing.Api;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class HashSignResponse
    {
        /// <summary>
        /// Initializes a new instance of the HashSignResponse class.
        /// </summary>
        public HashSignResponse()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the HashSignResponse class.
        /// </summary>
        public HashSignResponse(string signedHash = default(string))
        {
            SignedHash = signedHash;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "signedHash")]
        public string SignedHash { get; set; }

    }
}