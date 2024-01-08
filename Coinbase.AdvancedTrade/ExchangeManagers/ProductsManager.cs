using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;


namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Provides methods to manage products on the Coinbase Advanced Trade API.
    /// </summary>
    public class ProductsManager : BaseManager, IProductsManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsManager"/> class.
        /// </summary>
        /// <param name="authenticator">The authenticator for Coinbase API requests.</param>
        public ProductsManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

        /// <inheritdoc/>
        public async Task<List<Product>> ListProductsAsync(string productType = "SPOT")
        {
            if (string.IsNullOrEmpty(productType))
            {
                throw new ArgumentException("Product type cannot be null or empty", nameof(productType));
            }

            // Assuming SendAuthenticatedRequest becomes asynchronous
            if (_authenticator == null)
            {
                throw new InvalidOperationException("Authenticator is not initialized.");
            }

            var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/products", ConvertToDictionary(new { product_type = productType })) ?? new Dictionary<string, object>();
            return DeserializeJsonElement<List<Product>>(response, "products");
        }

        /// <inheritdoc/>
        public async Task<Product> GetProductAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            Dictionary<string, object> response = await _authenticator.SendAuthenticatedRequestAsync("GET", $"/api/v3/brokerage/products/{productId}");
            if (response != null)
            {
                // Convert the response dictionary back to JSON string and then deserialize it
                string rawText = JsonConvert.SerializeObject(response);
                return JsonConvert.DeserializeObject<Product>(rawText);
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<List<Candle>> GetProductCandlesAsync(string productId, string start, string end, Granularity granularity)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                throw new ArgumentException("Product ID, start, and end time cannot be null or empty");
            }

            var parameters = new { start, end, granularity };
            var response = await _authenticator.SendAuthenticatedRequestAsync("GET", $"/api/v3/brokerage/products/{productId}/candles", ConvertToDictionary(parameters)) ?? new Dictionary<string, object>();
            return DeserializeJsonElement<List<Candle>>(response, "candles");
        }

        /// <inheritdoc/>
        public async Task<MarketTrades> GetMarketTradesAsync(string productId, int limit)
        {
            if (string.IsNullOrEmpty(productId) || limit <= 0)
            {
                throw new ArgumentException("Product ID cannot be null or empty, and limit must be greater than 0");
            }

            var parameters = new { limit };
            var response = await _authenticator.SendAuthenticatedRequestAsync("GET", $"/api/v3/brokerage/products/{productId}/ticker", ConvertToDictionary(parameters)) ?? throw new InvalidOperationException("Response is null");

            // Extract trades data from response
            if (!response.TryGetValue("trades", out object tradesObj) || !(tradesObj is JArray tradesArray))
            {
                throw new InvalidOperationException("Invalid 'trades' data in the response");
            }

            List<Trade> trades = JsonConvert.DeserializeObject<List<Trade>>(tradesArray.ToString());

            // Extract best bid and best ask
            string bestBid = response.TryGetValue("best_bid", out object bestBidObj) ? bestBidObj?.ToString() : null;
            string bestAsk = response.TryGetValue("best_ask", out object bestAskObj) ? bestAskObj?.ToString() : null;

            return new MarketTrades { Trades = trades, BestBid = bestBid, BestAsk = bestAsk };
        }

        /// <inheritdoc/>
        public async Task<ProductBook> GetProductBookAsync(string productId, int limit)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            var parameters = new { product_id = productId, limit };
            var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/product_book", ConvertToDictionary(parameters)) ?? new Dictionary<string, object>();
            return DeserializeJsonElement<ProductBook>(response, "pricebook");
        }

        /// <inheritdoc/>
        public async Task<List<ProductBook>> GetBestBidAskAsync(List<string> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                throw new ArgumentException("Product IDs list cannot be null or empty", nameof(productIds));
            }

            // Construct the URL with multiple product_ids
            string url = "/api/v3/brokerage/best_bid_ask?" + string.Join("&", productIds.Select(pid => $"product_ids={pid}"));
            var response = await _authenticator.SendAuthenticatedRequestAsync("GET", url);

            if (response != null && response.TryGetValue("pricebooks", out object pricebooksObj) && pricebooksObj is JArray pricebooksArray)
            {
                return JsonConvert.DeserializeObject<List<ProductBook>>(pricebooksArray.ToString());
            }

            return null;
        }
    }
}
