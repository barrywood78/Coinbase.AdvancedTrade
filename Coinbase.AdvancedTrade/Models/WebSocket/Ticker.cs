using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a ticker message from the Coinbase WebSocket API.
    /// </summary>
    public class TickerMessage
    {
        /// <summary>
        /// Gets or sets the channel for the ticker message.
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the ticker message.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the ticker message was sent.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the ticker message.
        /// </summary>
        [JsonProperty("sequence_num")]
        public int SequenceNum { get; set; }

        /// <summary>
        /// Gets or sets the list of ticker events.
        /// </summary>
        [JsonProperty("events")]
        public List<TickerEvent> Events { get; set; }
    }

    /// <summary>
    /// Represents an individual ticker event within a <see cref="TickerMessage"/>.
    /// </summary>
    public class TickerEvent
    {
        /// <summary>
        /// Gets or sets the type of the ticker event.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the list of tickers associated with the ticker event.
        /// </summary>
        [JsonProperty("tickers")]
        public List<Ticker> Tickers { get; set; }
    }

    /// <summary>
    /// Represents details about a specific ticker.
    /// </summary>
    public class Ticker
    {
        /// <summary>
        /// Gets or sets the type of the ticker.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product associated with the ticker.
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the price of the ticker.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the volume of the product over the last 24 hours.
        /// </summary>
        [JsonProperty("volume_24_h")]
        public string Volume24H { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the product over the last 24 hours.
        /// </summary>
        [JsonProperty("low_24_h")]
        public string Low24H { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the product over the last 24 hours.
        /// </summary>
        [JsonProperty("high_24_h")]
        public string High24H { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the product over the last 52 weeks.
        /// </summary>
        [JsonProperty("low_52_w")]
        public string Low52W { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the product over the last 52 weeks.
        /// </summary>
        [JsonProperty("high_52_w")]
        public string High52W { get; set; }

        /// <summary>
        /// Gets or sets the percentage change in price of the product over the last 24 hours.
        /// </summary>
        [JsonProperty("price_percent_chg_24_h")]
        public string PricePercentChg24H { get; set; }
    }
}
