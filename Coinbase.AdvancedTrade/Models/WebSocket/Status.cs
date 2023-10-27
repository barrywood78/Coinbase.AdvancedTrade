using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a status message from the Coinbase WebSocket API.
    /// </summary>
    public class StatusMessage
    {
        /// <summary>
        /// Gets or sets the channel for the status message.
        /// </summary>
        [JsonPropertyName("channel")]
        public string? Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the status message.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the status message was sent.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the status message.
        /// </summary>
        [JsonPropertyName("sequence_num")]
        public int SequenceNum { get; set; }

        /// <summary>
        /// Gets or sets the list of status events.
        /// </summary>
        [JsonPropertyName("events")]
        public List<StatusEvent>? Events { get; set; }
    }

    /// <summary>
    /// Represents an individual status event within a <see cref="StatusMessage"/>.
    /// </summary>
    public class StatusEvent
    {
        /// <summary>
        /// Gets or sets the type of the status event.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the list of products associated with the status event.
        /// </summary>
        [JsonPropertyName("products")]
        public List<Product>? Products { get; set; }
    }

    /// <summary>
    /// Represents product details in the status event.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the base currency of the product.
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string? BaseCurrency { get; set; }

        /// <summary>
        /// Gets or sets the quote currency of the product.
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string? QuoteCurrency { get; set; }

        /// <summary>
        /// Gets or sets the base increment of the product.
        /// </summary>
        [JsonPropertyName("base_increment")]
        public string? BaseIncrement { get; set; }

        /// <summary>
        /// Gets or sets the quote increment of the product.
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public string? QuoteIncrement { get; set; }

        /// <summary>
        /// Gets or sets the display name of the product.
        /// </summary>
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the status of the product.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets any status message associated with the product.
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the minimum market funds for the product.
        /// </summary>
        [JsonPropertyName("min_market_funds")]
        public string? MinMarketFunds { get; set; }
    }
}
