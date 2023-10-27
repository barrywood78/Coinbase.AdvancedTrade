using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a heartbeat message from the Coinbase WebSocket API.
    /// </summary>
    public class HeartbeatMessage
    {
        /// <summary>
        /// Gets or sets the channel for the heartbeat message.
        /// </summary>
        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the heartbeat message.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the heartbeat message was sent.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the heartbeat message.
        /// </summary>
        [JsonPropertyName("sequence_num")]
        public int SequenceNum { get; set; }

        /// <summary>
        /// Gets or sets the list of heartbeat events.
        /// </summary>
        [JsonPropertyName("events")]
        public List<HeartbeatEvent>? Events { get; set; }
    }

    /// <summary>
    /// Represents an individual heartbeat event within a <see cref="HeartbeatMessage"/>.
    /// </summary>
    public class HeartbeatEvent
    {
        /// <summary>
        /// Gets or sets the current time for the heartbeat event.
        /// </summary>
        [JsonPropertyName("current_time")]
        public string? CurrentTime { get; set; }

        /// <summary>
        /// Gets or sets the counter for the heartbeat event.
        /// </summary>
        [JsonPropertyName("heartbeat_counter")]
        public int? HeartbeatCounter { get; set; }
    }
}
