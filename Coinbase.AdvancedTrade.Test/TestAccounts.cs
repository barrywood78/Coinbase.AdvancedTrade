using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestAccounts : TestBase
    {
        [TestMethod]
        [Description("Test to verify that the ListAccounts method returns valid accounts.")]
        public async Task Test_Accounts_ListAccountsAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                // Directly await the async method instead of using null-coalescing operator
                var accounts = await _coinbaseClient!.Accounts.ListAccountsAsync(25);

                Assert.IsNotNull(accounts, "Accounts list should not be null.");

                var usdcAccount = accounts?.FirstOrDefault(account => account.Currency == "USDC");
                Assert.IsNotNull(usdcAccount, "USDC account should not be null.");
                Assert.IsNotNull(usdcAccount?.Uuid, "USDC Uuid should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that GetAccount returns a valid account.")]
        public async Task Test_Accounts_GetAccount()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                // Read account ID from environment variable
                var accountUuid = "bae8ad62-f752-54f2-af7b-7b7566947202";

                // Directly await the async method instead of using null-coalescing operator
                var account = await _coinbaseClient!.Accounts.GetAccountAsync(accountUuid);

                Assert.IsNotNull(account, "Account should not be null.");
                Assert.IsNotNull(account?.Uuid, "Uuid should not be null.");
            });
        }
    }
}
