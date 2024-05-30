using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.Public
{
    /// <summary>
    /// Represents the order book for a public product.
    /// </summary>
    public class PublicProductBook
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the list of bid price levels.
        /// </summary>
        [JsonProperty("bids")]
        public List<PriceLevel> Bids { get; set; }

        /// <summary>
        /// Gets or sets the list of ask price levels.
        /// </summary>
        [JsonProperty("asks")]
        public List<PriceLevel> Asks { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the order book.
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// Represents a price level in the order book.
    /// </summary>
    public class PriceLevel
    {
        /// <summary>
        /// Gets or sets the price at this level.
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets the size at this level.
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }
    }
}
