
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestProducts : TestBase
    {
        [TestMethod]
        [Description("Test to verify that ListProducts returns a valid list of products.")]
        public async Task Test_Products_ListProductsAsync()
        {
            await ExecuteRateLimitedTest(async () => {
                try
                {
                    var products = await(_coinbaseClient?.Products.ListProductsAsync("SPOT") ?? Task.FromResult<List<Product>?>(null));
                    Assert.IsNotNull(products, "Products list should not be null.");
                    Assert.IsTrue(products?.Count > 0, "Products list should not be empty.");
                    Assert.IsNotNull(products?[0].BaseCurrencyId, "BaseCurrencyId should not be null.");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Test failed: {ex.Message}");
                }
            });
        }


        [TestMethod]
        [Description("Test to verify that GetProduct returns a valid product based on a given productId.")]
        public async Task Test_Products_GetProductAsync()
        {
            await ExecuteRateLimitedTest(async () => {
                try
                {
                    string productId = "BTC-USDC";
                    var product = await (_coinbaseClient?.Products.GetProductAsync(productId) ?? Task.FromResult<Product?>(null));
                    Assert.IsNotNull(product, "Product should not be null.");
                    Assert.IsNotNull(product?.ProductId, "ProductId should not be null.");
                }
                catch (Exception e)
                {
                    Assert.Fail($"An exception occurred: {e.Message}");
                }

            });
        }


        [TestMethod]
        [Description("Test to verify that GetProductCandles returns a valid list of candles for a given product.")]
        public async Task Test_Products_GetProductCandlesAsync()
        {
            await ExecuteRateLimitedTest(async () => {
                try
                {
                    string productId = "BTC-USDC";
                    string start = "1693526400";
                    string end = "1693612800";
                    var granularity = AdvancedTrade.Enums.Granularity.FIVE_MINUTE;

                    var candles = await (_coinbaseClient?.Products.GetProductCandlesAsync(productId, start, end, granularity) ?? Task.FromResult<List<Candle>?>(null));

                    Assert.IsNotNull(candles, "Candles list should not be empty.");
                    Assert.IsTrue(candles?.Count > 0, "Candles list should have at least one item.");
                }
                catch (Exception e)
                {
                    Assert.Fail($"An exception occurred: {e.Message}");
                }

            });
        }


        [TestMethod]
        [Description("Test to verify that GetMarketTrades returns a valid list of market trades for a given product.")]
        public async Task Test_Products_GetMarketTradesAsync()
        {
            await ExecuteRateLimitedTest(async () => {
                try
                {
                    var productId = "BTC-USDC";
                    var limit = 10;

                    var result = await (_coinbaseClient?.Products.GetMarketTradesAsync(productId, limit) ?? Task.FromResult<MarketTrades?>(null));

                    Assert.IsNotNull(result, "Result should not be null.");
                    Assert.IsFalse(string.IsNullOrEmpty(result?.BestBid), "BestBid should not be empty.");
                    Assert.IsTrue(result?.Trades?.Count > 0, "Trades list should have at least one item.");
                }
                catch (Exception e)
                {
                    Assert.Fail($"An exception occurred: {e.Message}");
                }

            });
        }



        [TestMethod]
        [Description("Test to verify that the GetProductBook method returns valid product book.")]
        public async Task Test_Products_GetProductBookAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                var productId = "BTC-USDC";
                var limit = 3;
                var result = await _coinbaseClient!.Products.GetProductBookAsync(productId, limit);

                Assert.IsNotNull(result, "Product Book should not be null.");

                Assert.AreEqual(productId, result?.ProductId, "Product ID does not match.");
                Assert.IsTrue(result?.Bids?.Count > 0 && result?.Bids?.Count <= limit, "Bids count should be between 1 and the defined limit.");
                Assert.IsTrue(result?.Asks?.Count > 0 && result?.Asks?.Count <= limit, "Asks count should be between 1 and the defined limit.");
            });
        }



        [TestMethod]
        [Description("Test to verify that the GetBestBidAsk method returns valid product books.")]
        public async Task Test_Products_GetBestBidAskAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                var productIds = new List<string> { "BTC-USDC", "ETH-USDC" };
                var results = await _coinbaseClient!.Products.GetBestBidAskAsync(productIds);

                Assert.IsNotNull(results, "Product Books list should not be null.");
                Assert.AreEqual(productIds.Count, results?.Count, "Number of returned ProductBooks should match the number of queried product IDs.");

                foreach (var result in results ?? new List<ProductBook>())
                {
                    Assert.IsTrue(productIds.Contains(result?.ProductId ?? string.Empty), $"Unexpected Product ID: {result?.ProductId}");
                    Assert.IsTrue(result?.Bids?.Count > 0, $"Bids list for Product ID: {result?.ProductId} should not be empty.");
                    Assert.IsTrue(result?.Asks?.Count > 0, $"Asks list for Product ID: {result?.ProductId} should not be empty.");
                }
            });
        }



    }
}
