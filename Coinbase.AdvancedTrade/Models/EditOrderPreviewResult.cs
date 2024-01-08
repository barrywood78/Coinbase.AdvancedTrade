using Newtonsoft.Json;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents the result from previewing an order edit within the Coinbase system.
    /// </summary>
    public class EditOrderPreviewResult
    {
        /// <summary>
        /// Gets or sets the estimated slippage for the edited order.
        /// </summary>
        [JsonProperty("slippage")]
        public string Slippage { get; set; }

        /// <summary>
        /// Gets or sets the total order value after the edit.
        /// </summary>
        [JsonProperty("order_total")]
        public string OrderTotal { get; set; }

        /// <summary>
        /// Gets or sets the total commission for the edited order.
        /// </summary>
        [JsonProperty("commission_total")]
        public string CommissionTotal { get; set; }

        /// <summary>
        /// Gets or sets the size of the order in quote currency after the edit.
        /// </summary>
        [JsonProperty("quote_size")]
        public string QuoteSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the order in base currency after the edit.
        /// </summary>
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }

        /// <summary>
        /// Gets or sets the best bid price available for the order after the edit.
        /// </summary>
        [JsonProperty("best_bid")]
        public string BestBid { get; set; }

        /// <summary>
        /// Gets or sets the best ask price available for the order after the edit.
        /// </summary>
        [JsonProperty("best_ask")]
        public string BestAsk { get; set; }

        /// <summary>
        /// Gets or sets the average price at which the order was filled after the edit.
        /// </summary>
        [JsonProperty("average_filled_price")]
        public string AverageFilledPrice { get; set; }
    }
}
