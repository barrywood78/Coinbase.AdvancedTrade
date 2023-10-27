using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestOrders : TestBase
    {
        [TestMethod]
        [Description("Test to verify that ListOrders returns a list of valid orders.")]
        public async Task Test_Orders_ListOrdersAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                var result = await _coinbaseClient!.Orders.ListOrdersAsync(
                    "BTC-USDC",
                    new OrderStatus[] { OrderStatus.CANCELLED },
                    new(2023, 10, 1),
                    new(2023, 10, 31),
                    OrderType.LIMIT,
                    OrderSide.BUY
                );

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.IsTrue(result.Count > 0, "Should return at least one order.");
                Assert.IsTrue(result.All(r => r.OrderType == "LIMIT"), "All orders should be of type LIMIT.");
                Assert.IsTrue(result.All(r => r.Side == "BUY"), "All orders should be of side BUY.");
            });
        }




        [TestMethod]
        [Description("Test to verify that ListFills returns a list of valid fills.")]
        public async Task Test_Orders_ListFillsAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                var result = await _coinbaseClient!.Orders.ListFillsAsync(
                    null,
                    "BTC-USDC",
                    new(2023, 10, 1),
                    new(2023, 10, 31)
                );

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.IsTrue(result.Count > 0, "Should return at least one fill.");
                Assert.IsTrue(result.All(r => r.Price != null), "All fills should have a price.");
                Assert.IsTrue(result.All(r => r.Size != null), "All fills should have a size.");
            });
        }

        [TestMethod]
        [Description("Test to verify that GetOrder returns a valid order.")]
        public async Task Test_Orders_GetOrderAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                string testOrderId = "75e5d09c-c0c7-4089-802e-69f6d672ec75";
                var result = await _coinbaseClient!.Orders.GetOrderAsync(testOrderId);

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.AreEqual(testOrderId, result?.OrderId, "Returned order ID should match the test order ID.");
            });
        }



        [TestMethod]
        [Description("Test to verify that CancelOrders cancels the provided orders and returns their results.")]
        public async Task Test_Orders_CancelOrdersAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                string[] testOrderIds = { "9f4dec91-44a0-4a6c-93e2-db7c0da7a495" };
                var result = await _coinbaseClient!.Orders.CancelOrdersAsync(testOrderIds);

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.IsTrue(result.Count > 0, "Should return at least one cancel order result.");
                Assert.IsTrue(result.All(r => r.Success), "All cancel requests should be successful.");
            });
        }


    }
}
