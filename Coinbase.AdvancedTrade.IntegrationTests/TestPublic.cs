using System;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade;
using Coinbase.AdvancedTrade.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestPublic
    {
        private CoinbaseClient _coinbaseClient = null!;
        private CoinbasePublicClient _coinbasePublicClient = null!;

        [TestInitialize]
        public void Setup()
        {
            // Setup for authenticated client
            var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
                         ?? throw new InvalidOperationException("API Key not found");
            var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
                           ?? throw new InvalidOperationException("API Secret not found");
            _coinbaseClient = new CoinbaseClient(apiKey, apiSecret);


            // Coinbase Legacy Keys
            //var apiKey = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_KEY", EnvironmentVariableTarget.User)
            //         ?? throw new InvalidOperationException("API Key not found");
            //var apiSecret = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_SECRET", EnvironmentVariableTarget.User)
            //           ?? throw new InvalidOperationException("API Secret not found");
            //_coinbaseClient = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret, apiKeyType: ApiKeyType.Legacy);


            // Setup for public client
            _coinbasePublicClient = new CoinbasePublicClient();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await Task.Delay(1000); // 1-second delay
        }


        [TestMethod]
        [Description("Test to verify that the GetCoinbaseServerTimeAsync method returns valid server time details using authenticated client.")]
        public async Task Test_Public_GetCoinbaseServerTimeAsync_WithAuth()
        {
            var serverTime = await _coinbaseClient.Public.GetCoinbaseServerTimeAsync();

            Assert.IsNotNull(serverTime, "Server Time should not be null.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.Iso), "ISO time should not be null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochSeconds), "Epoch Seconds should not be null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochMillis), "Epoch Milliseconds should not be null or whitespace.");
        }


        [TestMethod]
        [Description("Test to verify that the GetCoinbaseServerTimeAsync method returns valid server time details using public client.")]
        public async Task Test_Public_GetCoinbaseServerTimeAsync_WithoutAuth()
        {
            var serverTime = await _coinbasePublicClient.Public.GetCoinbaseServerTimeAsync();

            Assert.IsNotNull(serverTime, "Server Time should not be null.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.Iso), "ISO time should not be null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochSeconds), "Epoch Seconds should not be null or whitespace.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochMillis), "Epoch Milliseconds should not be null or whitespace.");
        }


        [TestMethod]
        [Description("Test to verify that the ListPublicProductsAsync method returns a list of public products using authenticated client with query parameters.")]
        public async Task Test_Public_ListPublicProductsAsync_WithAuth()
        {
            var limit = 10;
            var offset = 0;
            var productType = "SPOT";
            var productIds = new List<string> { "BTC-USDC", "ETH-USDC" };

            var products = await _coinbaseClient.Public.ListPublicProductsAsync(limit, offset, productType, productIds);

            Assert.IsNotNull(products, "Products should not be null.");
            Assert.IsTrue(products.Count > 0, "Products list should not be empty.");
        }


        [TestMethod]
        [Description("Test to verify that the ListPublicProductsAsync method returns a list of public products using public client with query parameters.")]
        public async Task Test_Public_ListPublicProductsAsync_WithoutAuth()
        {
            var limit = 10;
            var offset = 0;
            var productType = "SPOT";
            var productIds = new List<string> { "BTC-USDC", "ETH-USDC" };

            var products = await _coinbasePublicClient.Public.ListPublicProductsAsync(limit, offset, productType, productIds);

            Assert.IsNotNull(products, "Products should not be null.");
            Assert.IsTrue(products.Count > 0, "Products list should not be empty.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductAsync method returns the expected product details using authenticated client.")]
        public async Task Test_Public_GetPublicProductAsync_WithAuth()
        {
            var productId = "BTC-USDC"; 
            var product = await _coinbaseClient.Public.GetPublicProductAsync(productId);

            Assert.IsNotNull(product, "Product should not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(product.Price), "Price should not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(product.BaseCurrencyId), "Base currency ID should not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(product.QuoteCurrencyId), "Quote currency ID should not be null or empty.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductAsync method returns the expected product details using public client.")]
        public async Task Test_Public_GetPublicProductAsync_WithoutAuth()
        {
            var productId = "BTC-USDC"; 
            var product = await _coinbasePublicClient.Public.GetPublicProductAsync(productId);

            Assert.IsNotNull(product, "Product should not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(product.Price), "Price should not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(product.BaseCurrencyId), "Base currency ID should not be null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(product.QuoteCurrencyId), "Quote currency ID should not be null or empty.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductBookAsync method returns the expected product book details using authenticated client.")]
        public async Task Test_Public_GetPublicProductBookAsync_WithAuth()
        {
            var productId = "BTC-USDC"; 
            var limit = 10; 
            var productBook = await _coinbaseClient.Public.GetPublicProductBookAsync(productId, limit);

            Assert.IsNotNull(productBook, "Product book should not be null.");
            Assert.IsTrue(productBook.Bids.Count > 0, "Bids list should not be empty.");
            Assert.IsTrue(productBook.Asks.Count > 0, "Asks list should not be empty.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductBookAsync method returns the expected product book details using public client.")]
        public async Task Test_Public_GetPublicProductBookAsync_WithoutAuth()
        {
            var productId = "BTC-USDC"; 
            var limit = 10; 
            var productBook = await _coinbasePublicClient.Public.GetPublicProductBookAsync(productId, limit);

            Assert.IsNotNull(productBook, "Product book should not be null.");
            Assert.IsTrue(productBook.Bids.Count > 0, "Bids list should not be empty.");
            Assert.IsTrue(productBook.Asks.Count > 0, "Asks list should not be empty.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicMarketTradesAsync method returns the expected JSON for a specific product using authenticated client.")]
        public async Task Test_Public_GetPublicMarketTradesAsync_WithAuth()
        {
            var productId = "BTC-USDC"; 
            var limit = 10; 
            var start = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeSeconds(); 
            var end = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 

            var marketTrades = await _coinbaseClient.Public.GetPublicMarketTradesAsync(productId, limit, start, end);

            Assert.IsNotNull(marketTrades, "Market trades should not be null.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicMarketTradesAsync method returns the expected JSON for a specific product using public client.")]
        public async Task Test_Public_GetPublicMarketTradesAsync_WithoutAuth()
        {
            var productId = "BTC-USDC"; 
            var limit = 10; 
            var start = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeSeconds(); 
            var end = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 

            var marketTrades = await _coinbasePublicClient.Public.GetPublicMarketTradesAsync(productId, limit, start, end);

            Assert.IsNotNull(marketTrades, "Market trades should not be null.");

        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductCandlesAsync method returns the expected candle details for a specific product using authenticated client.")]
        public async Task Test_Public_GetPublicProductCandlesAsync_WithAuth()
        {
            var productId = "BTC-USDC"; 
            var start = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeSeconds(); 
            var end = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 
            var granularity = Granularity.ONE_HOUR; 

            var candles = await _coinbaseClient.Public.GetPublicProductCandlesAsync(productId, start, end, granularity);

            Assert.IsNotNull(candles, "Candles should not be null.");
            Assert.IsTrue(candles.Count > 0, "Candles list should not be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(candles[0].Start), "Start time should not be null or whitespace.");
        }


        [TestMethod]
        [Description("Test to verify that the GetPublicProductCandlesAsync method returns the expected candle details for a specific product using public client.")]
        public async Task Test_Public_GetPublicProductCandlesAsync_WithoutAuth()
        {
            var productId = "BTC-USDC"; 
            var start = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeSeconds();
            var end = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 
            var granularity = Granularity.ONE_HOUR; 

            var candles = await _coinbasePublicClient.Public.GetPublicProductCandlesAsync(productId, start, end, granularity);

            Assert.IsNotNull(candles, "Candles should not be null.");
            Assert.IsTrue(candles.Count > 0, "Candles list should not be empty.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(candles[0].Start), "Start time should not be null or whitespace.");
        }


    }
}
