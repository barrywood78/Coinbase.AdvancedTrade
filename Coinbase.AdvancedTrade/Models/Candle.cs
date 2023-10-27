using System;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents a candlestick data point for a specific time frame in a trading chart.
    /// </summary>
    public class Candle
    {
        /// <summary>
        /// Gets or sets the start time of the candlestick in UNIX timestamp format.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("start")]
        public string? StartUnix { get; set; }

        /// <summary>
        /// Gets the start date and time of the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime StartDate => !string.IsNullOrEmpty(StartUnix) ? UnixTimeStampToDateTime(StartUnix) : DateTime.MinValue;

        /// <summary>
        /// Gets or sets the lowest traded price of the asset during the time interval represented by the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("low")]
        public string? Low { get; set; }

        /// <summary>
        /// Gets or sets the highest traded price of the asset during the time interval represented by the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("high")]
        public string? High { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the asset at the beginning of the time interval represented by the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("open")]
        public string? Open { get; set; }

        /// <summary>
        /// Gets or sets the closing price of the asset at the end of the time interval represented by the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("close")]
        public string? Close { get; set; }

        /// <summary>
        /// Gets or sets the trading volume of the asset during the time interval represented by the candlestick.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("volume")]
        public string? Volume { get; set; }

        /// <summary>
        /// Converts a UNIX timestamp string to its corresponding DateTime value.
        /// </summary>
        /// <param name="unixTimeStamp">The UNIX timestamp string.</param>
        /// <returns>The converted DateTime value, or DateTime.MinValue if the conversion fails.</returns>
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
