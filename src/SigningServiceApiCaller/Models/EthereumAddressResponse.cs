// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace SigningServiceApiCaller.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class EthereumAddressResponse
    {
        /// <summary>
        /// Initializes a new instance of the EthereumAddressResponse class.
        /// </summary>
        public EthereumAddressResponse()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the EthereumAddressResponse class.
        /// </summary>
        public EthereumAddressResponse(string address = default(string))
        {
            Address = address;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

    }
}
