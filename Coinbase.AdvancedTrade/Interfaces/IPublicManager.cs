using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models.Public;
using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Interface for managing public endpoints of Coinbase Advanced Trade API.
    /// </summary>
    public interface IPublicManager
    {
        /// <summary>
        /// Asynchronously retrieves the current server time from Coinbase.
        /// </summary>
        /// <returns>The server time details including ISO 8601 formatted date and time,
        /// number of seconds since Unix epoch, and number of milliseconds since Unix epoch,
        /// or null if the information is not available.</returns>
        Task<ServerTime> GetCoinbaseServerTimeAsync();

        /// <summary>
        /// Asynchronously retrieves a list of public products from Coinbase.
        /// </summary>
        /// <param name="limit">The maximum number of products to retrieve. Null by default.</param>
        /// <param name="offset">The number of products to offset before returning results. Null by default.</param>
        /// <param name="productType">The type of products to retrieve (e.g., SPOT). Null by default.</param>
        /// <param name="productIds">A list of specific product IDs to retrieve. Null by default.</param>
        /// <returns>A list of public products or an empty list if none are found.</returns>
        Task<List<PublicProduct>> ListPublicProductsAsync(int? limit = null, int? offset = null, string productType = null, List<string> productIds = null);

        /// <summary>
        /// Asynchronously retrieves details for a specific public product by product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve details for.</param>
        /// <returns>The details of the specified public product or null if not found.</returns>
        Task<PublicProduct> GetPublicProductAsync(string productId);

        /// <summary>
        /// Asynchronously retrieves the order book for a specific public product.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve the order book for.</param>
        /// <param name="limit">The maximum number of bids/asks to retrieve. Null by default.</param>
        /// <returns>The order book details of the specified public product or null if not found.</returns>
        Task<PublicProductBook> GetPublicProductBookAsync(string productId, int? limit = null);

        /// <summary>
        /// Asynchronously retrieves market trades for a specific public product.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve market trades for.</param>
        /// <param name="limit">The maximum number of trades to retrieve.</param>
        /// <param name="start">The starting timestamp for the range of trades to retrieve. Null by default.</param>
        /// <param name="end">The ending timestamp for the range of trades to retrieve. Null by default.</param>
        /// <returns>The market trades details of the specified public product or null if not found.</returns>
        Task<PublicMarketTrades> GetPublicMarketTradesAsync(string productId, int limit, long? start = null, long? end = null);

        /// <summary>
        /// Asynchronously retrieves candlestick data for a specific public product.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve candlestick data for.</param>
        /// <param name="start">The starting timestamp for the range of candlestick data to retrieve.</param>
        /// <param name="end">The ending timestamp for the range of candlestick data to retrieve.</param>
        /// <param name="granularity">The granularity of the candlestick data.</param>
        /// <returns>A list of candlestick data or an empty list if none are found.</returns>
        Task<List<PublicCandle>> GetPublicProductCandlesAsync(string productId, long start, long end, Granularity granularity);
    }
}
