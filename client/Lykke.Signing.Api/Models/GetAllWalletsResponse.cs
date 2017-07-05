// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Signing.Api.Models
{
    using Lykke.Signing;
    using Lykke.Signing.Api;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class GetAllWalletsResponse
    {
        /// <summary>
        /// Initializes a new instance of the GetAllWalletsResponse class.
        /// </summary>
        public GetAllWalletsResponse()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the GetAllWalletsResponse class.
        /// </summary>
        public GetAllWalletsResponse(IList<string> addresses = default(IList<string>))
        {
            Addresses = addresses;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "addresses")]
        public IList<string> Addresses { get; set; }

    }
}