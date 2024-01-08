using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a Level 2 message from the Coinbase WebSocket API.
    /// </summary>
    public class Level2Message
    {
        /// <summary>
        /// Gets or sets the channel for the Level 2 message.
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the Level 2 message.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the Level 2 message was sent.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the Level 2 message.
        /// </summary>
        [JsonProperty("sequence_num")]
        public int SequenceNum { get; set; }

        /// <summary>
        /// Gets or sets the list of Level 2 events.
        /// </summary>
        [JsonProperty("events")]
        public List<Level2Event> Events { get; set; }
    }

    /// <summary>
    /// Represents an individual Level 2 event within a <see cref="Level2Message"/>.
    /// </summary>
    public class Level2Event
    {
        /// <summary>
        /// Gets or sets the type of the Level 2 event.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the product ID associated with the Level 2 event.
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the list of Level 2 updates.
        /// </summary>
        [JsonProperty("updates")]
        public List<Level2Update> Updates { get; set; }
    }

    /// <summary>
    /// Represents an individual update within a <see cref="Level2Event"/>.
    /// </summary>
    public class Level2Update
    {
        /// <summary>
        /// Gets or sets the side (e.g. "buy" or "sell") of the Level 2 update.
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Gets or sets the time when the Level 2 update occurred.
        /// </summary>
        [JsonProperty("event_time")]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// Gets or sets the price level for the Level 2 update.
        /// </summary>
        [JsonProperty("price_level")]
        public string PriceLevel { get; set; }

        /// <summary>
        /// Gets or sets the new quantity for the Level 2 update.
        /// </summary>
        [JsonProperty("new_quantity")]
        public string NewQuantity { get; set; }
    }
}
