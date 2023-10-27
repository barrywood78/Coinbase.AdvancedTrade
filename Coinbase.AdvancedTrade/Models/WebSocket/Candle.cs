using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a message containing websocket candle data.
    /// </summary>
    public class CandleMessage
    {
        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("sequence_num")]
        public long SequenceNumber { get; set; }

        [JsonPropertyName("events")]
        public List<Event>? Events { get; set; }

        /// <summary>
        /// Represents a specific event within the candle message.
        /// </summary>
        public class Event
        {
            [JsonPropertyName("type")]
            public string? Type { get; set; }

            [JsonPropertyName("candles")]
            public List<Candle>? Candles { get; set; }
        }
    }

    /// <summary>
    /// Represents a single candle's data.
    /// </summary>
    public class Candle
    {
        [JsonPropertyName("start")]
        public string? StartUnix { get; set; }

        [JsonPropertyName("high")]
        public string? High { get; set; }

        [JsonPropertyName("low")]
        public string? Low { get; set; }

        [JsonPropertyName("open")]
        public string? Open { get; set; }

        [JsonPropertyName("close")]
        public string? Close { get; set; }

        [JsonPropertyName("volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        /// <summary>
        /// Translates the Unix timestamp to a DateTime object.
        /// </summary>
        [JsonIgnore]
        public DateTime StartDate => !string.IsNullOrEmpty(StartUnix) ? UnixTimeStampToDateTime(StartUnix) : DateTime.MinValue;

        /// <summary>
        /// Converts a Unix timestamp string to a DateTime object.
        /// </summary>
        /// <param name="unixTimeStamp">The Unix timestamp string.</param>
        /// <returns>The converted DateTime object.</returns>
        private static DateTime UnixTimeStampToDateTime(string? unixTimeStamp)
        {
            if (long.TryParse(unixTimeStamp, out long parsedUnixTime))
            {
                return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTime).UtcDateTime;
            }
            return DateTime.MinValue;
        }
    }
}
