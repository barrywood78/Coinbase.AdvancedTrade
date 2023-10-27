using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents an individual trade on the market.
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// Gets or sets the unique identifier for the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trade_id")]
        public string? TradeId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier associated with the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the price at which the trade occurred.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("price")]
        public string? Price { get; set; }

        /// <summary>
        /// Gets or sets the size or quantity of the asset that was traded.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("size")]
        public string? Size { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("time")]
        public string? Time { get; set; }

        /// <summary>
        /// Gets or sets the side of the trade (e.g., "buy" or "sell").
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("side")]
        public string? Side { get; set; }

        /// <summary>
        /// Gets or sets the bid price at the time of the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("bid")]
        public string? Bid { get; set; }

        /// <summary>
        /// Gets or sets the ask price at the time of the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("ask")]
        public string? Ask { get; set; }
    }

    /// <summary>
    /// Represents a collection of market trades along with the best bid and ask at the time.
    /// </summary>
    public class MarketTrades
    {
        /// <summary>
        /// Gets or sets the list of trades.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trades")]
        public List<Trade>? Trades { get; set; }

        /// <summary>
        /// Gets or sets the best bid price at the time of the data collection.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_bid")]
        public string? BestBid { get; set; }

        /// <summary>
        /// Gets or sets the best ask price at the time of the data collection.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("best_ask")]
        public string? BestAsk { get; set; }
    }
}
