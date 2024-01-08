using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a message containing websocket candle data.
    /// </summary>
    public class CandleMessage
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("sequence_num")]
        public long SequenceNumber { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        /// <summary>
        /// Represents a specific event within the candle message.
        /// </summary>
        public class Event
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("candles")]
            public List<Candle> Candles { get; set; }
        }
    }

    /// <summary>
    /// Represents a single candle's data.
    /// </summary>
    public class Candle
    {
        [JsonProperty("start")]
        public string StartUnix { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("open")]
        public string Open { get; set; }

        [JsonProperty("close")]
        public string Close { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

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
        private static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            if (long.TryParse(unixTimeStamp, out long parsedUnixTime))
            {
                return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTime).UtcDateTime;
            }
            return DateTime.MinValue;
        }
    }
}
