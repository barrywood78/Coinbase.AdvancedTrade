using Newtonsoft.Json;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents the result of an order cancellation request.
    /// </summary>
    public class CancelOrderResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the order cancellation was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the reason for the failure, if applicable.
        /// </summary>
        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the order that was requested to be canceled.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }
}
