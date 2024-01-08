using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using Coinbase.AdvancedTrade.Models;
using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Provides asynchronous methods for managing products on the Coinbase platform.
    /// </summary>
    public interface IProductsManager
    {
        /// <summary>
        /// Asynchronously retrieves a list of products.
        /// </summary>
        /// <param name="productType">The type of product. Defaults to "SPOT".</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of products or null if none are found.</returns>
        Task<List<Product>> ListProductsAsync(string productType = "SPOT");

        /// <summary>
        /// Asynchronously retrieves a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product corresponding to the given ID or null if not found.</returns>
        Task<Product> GetProductAsync(string productId);

        /// <summary>
        /// Asynchronously retrieves the candles data for a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="start">The start date for the data.</param>
        /// <param name="end">The end date for the data.</param>
        /// <param name="granularity">The granularity of the data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of candle data or null if none are found.</returns>
        Task<List<Candle>> GetProductCandlesAsync(string productId, string start, string end, Granularity granularity);

        /// <summary>
        /// Asynchronously retrieves the market trades for a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="limit">The maximum number of trades to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the market trades or null if none are found.</returns>
        Task<MarketTrades> GetMarketTradesAsync(string productId, int limit);

        /// <summary>
        /// Asynchronously retrieves the product book for a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="limit">The maximum number of items to retrieve from the book. Defaults to 25.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product book or null if none are found.</returns>
        Task<ProductBook> GetProductBookAsync(string productId, int limit = 25);

        /// <summary>
        /// Asynchronously retrieves the best bid and ask for a list of products.
        /// </summary>
        /// <param name="productIds">The list of product IDs.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of product books with the best bid and ask or null if none are found.</returns>
        Task<List<ProductBook>> GetBestBidAskAsync(List<string> productIds);
    }
}
