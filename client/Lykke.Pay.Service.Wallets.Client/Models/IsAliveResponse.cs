// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Pay.Service.Wallets.Client.Models
{
    using Lykke.Service;
    using Lykke.Service.Assets;
    using Lykke.Pay.Service.Wallets.Client;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Checks service is alive response
    /// </summary>
    public partial class IsAliveResponse
    {
        /// <summary>
        /// Initializes a new instance of the IsAliveResponse class.
        /// </summary>
        public IsAliveResponse()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the IsAliveResponse class.
        /// </summary>
        /// <param name="version">API version</param>
        /// <param name="env">Environment variables</param>
        public IsAliveResponse(string version = default(string), string env = default(string))
        {
            Version = version;
            Env = env;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets API version
        /// </summary>
        [JsonProperty(PropertyName = "Version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets environment variables
        /// </summary>
        [JsonProperty(PropertyName = "Env")]
        public string Env { get; set; }

    }
}
