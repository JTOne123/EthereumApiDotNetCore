// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.EthereumCoreClient.Models
{
    
    using Lykke.EthereumCoreClient;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class CoinResult
    {
        /// <summary>
        /// Initializes a new instance of the CoinResult class.
        /// </summary>
        public CoinResult()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CoinResult class.
        /// </summary>
        public CoinResult(string blockchain = default(string), string id = default(string), string name = default(string), string adapterAddress = default(string), string externalTokenAddress = default(string), int? multiplier = default(int?), bool? blockchainDepositEnabled = default(bool?), bool? containsEth = default(bool?))
        {
            Blockchain = blockchain;
            Id = id;
            Name = name;
            AdapterAddress = adapterAddress;
            ExternalTokenAddress = externalTokenAddress;
            Multiplier = multiplier;
            BlockchainDepositEnabled = blockchainDepositEnabled;
            ContainsEth = containsEth;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "blockchain")]
        public string Blockchain { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "adapterAddress")]
        public string AdapterAddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "externalTokenAddress")]
        public string ExternalTokenAddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "multiplier")]
        public int? Multiplier { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "blockchainDepositEnabled")]
        public bool? BlockchainDepositEnabled { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "containsEth")]
        public bool? ContainsEth { get; set; }

    }
}