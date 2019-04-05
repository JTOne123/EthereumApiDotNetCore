// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace EthereumSamuraiApiCaller.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class GetErc20TransferHistoryV2Request
    {
        /// <summary>
        /// Initializes a new instance of the GetErc20TransferHistoryV2Request
        /// class.
        /// </summary>
        public GetErc20TransferHistoryV2Request()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the GetErc20TransferHistoryV2Request
        /// class.
        /// </summary>
        public GetErc20TransferHistoryV2Request(string assetHolder = default(string), long? blockNumber = default(long?), string contractAddress = default(string))
        {
            AssetHolder = assetHolder;
            BlockNumber = blockNumber;
            ContractAddress = contractAddress;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "assetHolder")]
        public string AssetHolder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "blockNumber")]
        public long? BlockNumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "contractAddress")]
        public string ContractAddress { get; set; }

    }
}
