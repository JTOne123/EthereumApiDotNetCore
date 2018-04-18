// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.EthereumCore.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TokenAddressTransactions
    {
        /// <summary>
        /// Initializes a new instance of the TokenAddressTransactions class.
        /// </summary>
        public TokenAddressTransactions()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TokenAddressTransactions class.
        /// </summary>
        public TokenAddressTransactions(int start, int count, string tokenAddress = default(string), string address = default(string))
        {
            TokenAddress = tokenAddress;
            Address = address;
            Start = start;
            Count = count;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TokenAddress")]
        public string TokenAddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Start")]
        public int Start { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}
