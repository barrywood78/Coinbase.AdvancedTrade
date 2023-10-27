using System;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestFees : TestBase
    {
        [TestMethod]
        [Description("Test to verify that GetTransactionsSummary returns a valid summary of transactions.")]
        public async Task Test_Fees_GetTransactionsSummaryAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                DateTime startDate = new(2023, 1, 1);
                DateTime endDate = new(2023, 12, 31);
                string userNativeCurrency = "CAD";
                string productType = "SPOT";

                // Directly await the method call, instead of using null-coalescing operator
                var result = await _coinbaseClient!.Fees.GetTransactionsSummaryAsync(startDate, endDate, userNativeCurrency, productType);

                Assert.IsNotNull(result, "Result should not be null.");
                Assert.IsNotNull(result?.FeeTier?.PricingTier, "FeeTier should not be null.");
            });
        }
    }
}
