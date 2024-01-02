using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents the server time details.
    /// </summary>
    public class ServerTime
    {
        /// <summary>
        /// Gets or sets the ISO 8601 formatted date and time.
        /// </summary>
        [JsonPropertyName("iso")]
        public string? Iso { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds since the Unix epoch as a string.
        /// </summary>
        [JsonPropertyName("epochSeconds")]
        public string? EpochSeconds { get; set; }

        /// <summary>
        /// Gets or sets the number of milliseconds since the Unix epoch as a string.
        /// </summary>
        [JsonPropertyName("epochMillis")]
        public string? EpochMillis { get; set; }
    }
}
