using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents the order book for a specific product on Coinbase.
    /// </summary>
    public class ProductBook
    {
        /// <summary>
        /// Gets or sets the product identifier associated with the order book.
        /// </summary>
        [JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the list of buy orders for the product.
        /// </summary>
        [JsonPropertyName("bids")]
        public List<Offer>? Bids { get; set; }

        /// <summary>
        /// Gets or sets the list of sell orders for the product.
        /// </summary>
        [JsonPropertyName("asks")]
        public List<Offer>? Asks { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the order book was captured.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// Represents an individual offer (bid or ask) in the order book.
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Gets or sets the price at which the offer is made.
        /// </summary>
        [JsonPropertyName("price")]
        public string? Price { get; set; }

        /// <summary>
        /// Gets or sets the size or quantity of the offer.
        /// </summary>
        [JsonPropertyName("size")]
        public string? Size { get; set; }
    }
}
