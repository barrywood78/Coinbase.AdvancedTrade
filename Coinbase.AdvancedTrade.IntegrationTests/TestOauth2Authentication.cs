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
    public class TestOauth2Authentication
    {
        private CoinbaseOauth2Client _coinbaseClient = null!;

        [TestInitialize]
        public void Setup()
        {
            // Setup for OAuth2 authenticated client
            var oAuth2Token = Environment.GetEnvironmentVariable("COINBASE_OAUTH2_TOKEN", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("COINBASE_OAUTH2_TOKEN not found");

            _coinbaseClient = new CoinbaseOauth2Client(oAuth2Token);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await Task.Delay(1000); // 1-second delay
        }


        [TestMethod]
        [Description("Test to verify Oauth2 authentication works to return list of accounts.")]
        public async Task Test_Oauth2_ListAccounts()
        {
            var accounts = await _coinbaseClient!.Accounts.ListAccountsAsync(25);

            Assert.IsNotNull(accounts, "Accounts list should not be null.");

            var usdcAccount = accounts?.FirstOrDefault(account => account.Currency == "USDC");
            Assert.IsNotNull(usdcAccount, "USDC account should not be null.");
            Assert.IsNotNull(usdcAccount?.Uuid, "USDC Uuid should not be null.");
        }


        [TestMethod]
        [Description("Test to verify Oauth2 authentication works to return account.")]
        public async Task Test_Oauth2_GetAccount()
        {
            // Read account ID from environment variable
            var accountUuid = "bae8ad62-f752-54f2-af7b-7b7566947202";

            // Directly await the async method instead of using null-coalescing operator
            var account = await _coinbaseClient!.Accounts.GetAccountAsync(accountUuid);

            Assert.IsNotNull(account, "Account should not be null.");
            Assert.IsNotNull(account?.Uuid, "Uuid should not be null.");
        }


        [TestMethod]
        [Description("Test to verify Oauth2 authentication works to place a Market Buy order.")]
        public async Task Test_Oauth2_CreateMarketOrderBuy()
        {
            var orderNumber = await _coinbaseClient!.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.BUY, "1");
            Assert.IsNotNull(orderNumber, "Order Number should not be null.");
        }


    }
}