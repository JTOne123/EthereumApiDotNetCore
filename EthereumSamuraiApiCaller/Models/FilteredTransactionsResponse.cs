// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace EthereumSamuraiApiCaller.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class FilteredTransactionsResponse
    {
        /// <summary>
        /// Initializes a new instance of the FilteredTransactionsResponse
        /// class.
        /// </summary>
        public FilteredTransactionsResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FilteredTransactionsResponse
        /// class.
        /// </summary>
        public FilteredTransactionsResponse(IList<TransactionResponse> transactions = default(IList<TransactionResponse>))
        {
            Transactions = transactions;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactions")]
        public IList<TransactionResponse> Transactions { get; set; }

    }
}
