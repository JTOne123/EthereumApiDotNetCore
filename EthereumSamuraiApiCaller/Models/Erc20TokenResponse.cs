// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace EthereumSamuraiApiCaller.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Erc20TokenResponse
    {
        /// <summary>
        /// Initializes a new instance of the Erc20TokenResponse class.
        /// </summary>
        public Erc20TokenResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Erc20TokenResponse class.
        /// </summary>
        public Erc20TokenResponse(string address = default(string), int? decimals = default(int?), string name = default(string), string symbol = default(string), string totalSupply = default(string))
        {
            Address = address;
            Decimals = decimals;
            Name = name;
            Symbol = symbol;
            TotalSupply = totalSupply;
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

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totalSupply")]
        public string TotalSupply { get; set; }

    }
}
