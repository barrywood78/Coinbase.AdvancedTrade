using System;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestCommon : TestBase
    {
        [TestMethod]
        [Description("Test to verify that the GetCoinbaseServerTimeAsync method returns valid server time details.")]
        public async Task Test_Common_GetCoinbaseServerTimeAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                var serverTime = await _coinbaseClient!.Common.GetCoinbaseServerTimeAsync();

                Assert.IsNotNull(serverTime, "Server Time should not be null.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.Iso), "ISO time should not be null or whitespace.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochSeconds), "Epoch Seconds should not be null or whitespace.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(serverTime.EpochMillis), "Epoch Milliseconds should not be null or whitespace.");

            });
        }
    }
}
