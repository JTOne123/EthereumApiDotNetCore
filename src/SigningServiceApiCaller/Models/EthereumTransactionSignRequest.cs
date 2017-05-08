// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace SigningServiceApiCaller.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class EthereumTransactionSignRequest
    {
        /// <summary>
        /// Initializes a new instance of the EthereumTransactionSignRequest
        /// class.
        /// </summary>
        public EthereumTransactionSignRequest()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the EthereumTransactionSignRequest
        /// class.
        /// </summary>
        public EthereumTransactionSignRequest(string fromProperty = default(string), string transaction = default(string))
        {
            FromProperty = fromProperty;
            Transaction = transaction;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string FromProperty { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transaction")]
        public string Transaction { get; set; }

    }
}
