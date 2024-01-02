using System;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTradeTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.AdvancedTradeTest
{
    public class TestBase
    {
        protected CoinbaseClient? _coinbaseClient;
        private static DateTime _lastApiCallTime;
        protected static bool UseLiveClient { get; set; }
        protected WebSocketManager? _webSocketManager;
        public TestContext? TestContext { get; set; }

        [TestInitialize]
        public virtual void Setup()
        {
            var apiKey = Environment.GetEnvironmentVariable("COINBASE_API_KEY", EnvironmentVariableTarget.User)
                         ?? throw new InvalidOperationException("API Key not found");
            var apiSecret = Environment.GetEnvironmentVariable("COINBASE_API_SECRET", EnvironmentVariableTarget.User)
                           ?? throw new InvalidOperationException("API Secret not found");

            UseLiveClient = bool.TryParse(TestContext?.Properties["UseLiveClient"]?.ToString(), out var useLive) && useLive;

            _coinbaseClient = UseLiveClient
                ? new CoinbaseClient(apiKey, apiSecret)
                : new CoinbaseClient(
                    MockSetupHelper.InitializeAccountsMock().Object,
                    MockSetupHelper.InitializeProductsMock().Object,
                    MockSetupHelper.InitializeOrdersMock().Object,
                    MockSetupHelper.InitializeFeesMock().Object,
                    MockSetupHelper.InitializeCommonMock().Object);

            _webSocketManager = _coinbaseClient?.WebSocket;
        }

        protected static async Task ExecuteRateLimitedTest(Func<Task> testLogic)
        {
            if (UseLiveClient)
            {
                var timeSinceLastCall = DateTime.UtcNow - _lastApiCallTime;
                var delay = 1000 - (int)timeSinceLastCall.TotalMilliseconds;
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
                _lastApiCallTime = DateTime.UtcNow;
            }

            try
            {
                await testLogic();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed: {ex.Message}");
            }
        }
    }
}
