// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.EthereumCore.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class EthereumAddressRequest
    {
        /// <summary>
        /// Initializes a new instance of the EthereumAddressRequest class.
        /// </summary>
        public EthereumAddressRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the EthereumAddressRequest class.
        /// </summary>
        public EthereumAddressRequest(string address = default(string))
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
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

    }
}
