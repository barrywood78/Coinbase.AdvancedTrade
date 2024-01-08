using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a market trades message from the Coinbase WebSocket API.
    /// </summary>
    public class MarketTradesMessage
    {
        /// <summary>
        /// Gets or sets the channel for the market trades message.
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the market trades message.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the market trades message was sent.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the market trades message.
        /// </summary>
        [JsonProperty("sequence_num")]
        public long SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of market trade events.
        /// </summary>
        [JsonProperty("events")]
        public List<MarketTradeEvent> Events { get; set; }

        /// <summary>
        /// Represents an individual market trade event within a <see cref="MarketTradesMessage"/>.
        /// </summary>
        public class MarketTradeEvent
        {
            /// <summary>
            /// Gets or sets the type of the market trade event.
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the list of trades associated with the market trade event.
            /// </summary>
            [JsonProperty("trades")]
            public List<Trade> Trades { get; set; }
        }
    }

    /// <summary>
    /// Represents details about a specific trade.
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// Gets or sets the ID of the trade.
        /// </summary>
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product associated with the trade.
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the price of the trade.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the size of the trade.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the side of the trade (e.g., "buy" or "sell").
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the trade.
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Gets the trade time as a <see cref="DateTime"/> object. If parsing fails, it returns <see cref="DateTime.MinValue"/>.
        /// </summary>
        [JsonIgnore]
        public DateTime TradeTime => DateTime.TryParse(Time, out DateTime parsedTime) ? parsedTime : DateTime.MinValue;
    }
}
