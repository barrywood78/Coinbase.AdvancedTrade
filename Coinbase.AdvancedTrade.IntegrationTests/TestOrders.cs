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
                string[] testOrderIds = { "b38ad5a6-6fd5-44db-ad36-196ac426de1a" };
                var result = await _coinbaseClient!.Orders.CancelOrdersAsync(testOrderIds);

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.IsTrue(result.Count > 0, "Should return at least one cancel order result.");
                Assert.IsTrue(result.All(r => r.Success), "All cancel requests should be successful.");
            });
        }



        [TestMethod]
        [Description("Test to verify that EditOrder successfully edits an existing order.")]
        public async Task Test_Orders_EditOrderAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                string existingOrderId = "57f52fa4-7c3e-4a1b-ac67-a3bce72f4086";

                string newPrice = "12000.00";
                string? newSize = "0.000035";

                // Attempt to edit the order
                var result = await _coinbaseClient!.Orders.EditOrderAsync(existingOrderId, newPrice, newSize);

                // Assert that the edit operation was reported as successful
                Assert.IsTrue(result, "Edit operation should be reported as successful.");
            });
        }


        [TestMethod]
        [Description("Test to verify that EditOrderPreview successfully previewed the edit of an existing order.")]
        public async Task Test_Orders_EditOrderPreviewAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                string existingOrderId = "57f52fa4-7c3e-4a1b-ac67-a3bce72f4086";

                string newPrice = "12000.00";
                string? newSize = "0.000035";

                var result = await _coinbaseClient!.Orders.EditOrderPreviewAsync(existingOrderId, newPrice, newSize);

                Assert.IsNotNull(result, "Preview result should not be null.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(result.OrderTotal), "OrderTotal should have a value.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(result.CommissionTotal), "CommissionTotal should have a value.");
      
            });
        }




    }
}
