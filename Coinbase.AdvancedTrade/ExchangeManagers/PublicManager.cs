using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Interfaces;
using RestSharp;
using Coinbase.AdvancedTrade.Models.Public;
using Coinbase.AdvancedTrade.Utilities;
using Newtonsoft.Json;
using System.Linq;
using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Manages public activities, including server time retrieval, for the Coinbase Advanced Trade API.
    /// </summary>
    public class PublicManager : BaseManager, IPublicManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicManager"/> class with an authenticator.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator.</param>
        public PublicManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicManager"/> class without authentication.
        /// </summary>
        public PublicManager() : base(null) { }

        /// <inheritdoc/>
        public async Task<ServerTime> GetCoinbaseServerTimeAsync()
        {
            try
            {
                var request = new RestRequest("/api/v3/brokerage/time", Method.Get);
                var response = await _client.ExecuteAsync<ServerTime>(request);
                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    throw new InvalidOperationException($"Failed to get server time. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get server time", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<List<PublicProduct>> ListPublicProductsAsync(int? limit = null, int? offset = null, string productType = null, List<string> productIds = null)
        {
            try
            {
                var request = new RestRequest("/api/v3/brokerage/market/products", Method.Get);

                // Add query parameters if provided
                if (limit.HasValue)
                    request.AddParameter("limit", limit.Value);
                if (offset.HasValue)
                    request.AddParameter("offset", offset.Value);
                if (!string.IsNullOrEmpty(productType))
                    request.AddParameter("product_type", productType);
                if (productIds != null && productIds.Any())
                {
                    foreach (var productId in productIds)
                    {
                        request.AddParameter("product_ids", productId);
                    }
                }

                var response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                    return UtilityHelper.DeserializeJsonElement<List<PublicProduct>>(responseDict, "products");
                }
                else
                {
                    throw new InvalidOperationException($"Failed to list public products. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to list public products", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<PublicProduct> GetPublicProductAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            try
            {
                var request = new RestRequest($"/api/v3/brokerage/market/products/{productId}", Method.Get);
                var response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var publicProduct = JsonConvert.DeserializeObject<PublicProduct>(response.Content);
                    return publicProduct;
                }
                else
                {
                    throw new InvalidOperationException($"Failed to get public product. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get public product", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<PublicProductBook> GetPublicProductBookAsync(string productId, int? limit = null)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            try
            {
                var request = new RestRequest("/api/v3/brokerage/market/product_book", Method.Get);
                request.AddParameter("product_id", productId);

                if (limit.HasValue)
                {
                    request.AddParameter("limit", limit.Value);
                }

                var response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                    return UtilityHelper.DeserializeJsonElement<PublicProductBook>(responseDict, "pricebook");
                }
                else
                {
                    throw new InvalidOperationException($"Failed to get public product book. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get public product book", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<PublicMarketTrades> GetPublicMarketTradesAsync(string productId, int limit, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
            }

            try
            {
                var request = new RestRequest($"/api/v3/brokerage/market/products/{productId}/ticker", Method.Get);
                request.AddParameter("limit", limit);

                if (start.HasValue)
                {
                    request.AddParameter("start", start.Value);
                }

                if (end.HasValue)
                {
                    request.AddParameter("end", end.Value);
                }

                var response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<PublicMarketTrades>(response.Content);
                }
                else
                {
                    throw new InvalidOperationException($"Failed to get public market trades. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get public market trades", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<List<PublicCandle>> GetPublicProductCandlesAsync(string productId, long start, long end, Granularity granularity)
        {
            try
            {
                var request = new RestRequest($"/api/v3/brokerage/market/products/{productId}/candles", Method.Get);

                // Add query parameters
                request.AddParameter("start", start);
                request.AddParameter("end", end);
                request.AddParameter("granularity", granularity.ToString());

                var response = await _client.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                    return UtilityHelper.DeserializeJsonElement<List<PublicCandle>>(responseDict, "candles");
                }
                else
                {
                    throw new InvalidOperationException($"Failed to get public product candles. Status: {response.StatusCode}, Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get public product candles", ex);
            }
        }
    }
}
