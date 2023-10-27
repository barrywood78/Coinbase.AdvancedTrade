using System;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents a fill, which is a completed trade on the exchange. A fill is created for each side of the trade.
    /// </summary>
    public class Fill
    {
        /// <summary>
        /// Gets or sets the unique identifier for this fill entry.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("entry_id")]
        public string? EntryId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the trade associated with this fill.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trade_id")]
        public string? TradeId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the order associated with this fill.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the time the trade was executed.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trade_time")]
        public string? TradeTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("trade_type")]
        public string? TradeType { get; set; }

        /// <summary>
        /// Gets or sets the price at which the trade was executed.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("price")]
        public string? Price { get; set; }

        /// <summary>
        /// Gets or sets the size of the asset traded.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("size")]
        public string? Size { get; set; }

        /// <summary>
        /// Gets or sets the commission or fee taken by the exchange for executing the trade.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("commission")]
        public string? Commission { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product being traded.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the sequence timestamp for the fill, which indicates the order in which it was processed.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("sequence_timestamp")]
        public string? SequenceTimestamp { get; set; }

        /// <summary>
        /// Gets or sets an indicator for the liquidity of the trade. E.g., "M" for maker or "T" for taker.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("liquidity_indicator")]
        public string? LiquidityIndicator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the size value is in quote currency.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("size_in_quote")]
        public bool SizeInQuote { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user associated with the fill.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the side of the trade, e.g., "buy" or "sell".
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("side")]
        public string? Side { get; set; }
    }
}
