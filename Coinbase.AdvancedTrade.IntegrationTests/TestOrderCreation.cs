﻿using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTradeTest
{
    [TestClass]
    public class TestOrderCreation : TestBase
    {
        [TestMethod]
        [Description("Test to verify that CreateMarketOrderBuy creates an order for BTC-USDC.")]
        public async Task Test_Orders_CreateMarketOrderBuyAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.BUY, "1");
                Assert.IsNotNull(orderNumber, "Order Number should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that CreateMarketOrderSell creates a sell order for BTC-USDC.")]
        public async Task Test_Orders_CreateMarketOrderSellAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.SELL, "0.00002111");
                Assert.IsNotNull(orderNumber, "Order Number should not be null.");
            });
        }



        [TestMethod]
        [Description("Test to verify that CreateLimitOrderGTCBuy creates a limit order for BTC-USDC.")]
        public async Task Test_Orders_CreateLimitOrderGTCBuyAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync("BTC-USDC", OrderSide.BUY, "0.000035", "10000", true);
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that CreateLimitOrderGTCSell creates a limit sell order for BTC-USDC.")]
        public async Task Test_Orders_CreateLimitOrderGTCSellAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync("BTC-USDC", OrderSide.SELL, "0.000035", "50000", true);
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }




        [TestMethod]
        [Description("Test to verify that CreateLimitOrderGTDBuy creates a limit order for BTC-USDC.")]
        public async Task Test_Orders_CreateLimitOrderGTDBuyAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync("BTC-USDC", OrderSide.BUY, "0.000035", "10000", DateTime.UtcNow.AddDays(1), true);
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that CreateLimitOrderGTDSell creates a limit sell order for BTC-USDC.")]
        public async Task Test_Orders_CreateLimitOrderGTDSellAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync("BTC-USDC", OrderSide.SELL, "0.000035", "50000", DateTime.UtcNow.AddDays(1), true);
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }



        [TestMethod]
        [Description("Test to verify that CreateStopLimitOrderGTCBuy creates a stop-limit order with GTC for BTC-USDC.")]
        public async Task Test_Orders_CreateStopLimitOrderGTCBuyAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.BUY, "0.0001", "48000.00", "47000.00");
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that CreateStopLimitOrderGTCSel creates a stop-limit sell order with GTC for BTC-USDC.")]
        public async Task Test_Orders_CreateStopLimitOrderGTCSellAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.SELL, "0.0001", "40000.00", "40500.00");
                //var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.SELL, "5", "43200.00", "43500.00");
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }



        [TestMethod]
        [Description("Test to verify that CreateStopLimitOrderGTDBu creates a limit order for BTC-USDC.")]
        public async Task Test_Orders_CreateStopLimitOrderGTDBuyAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                    "BTC-USDC", OrderSide.BUY, "0.0001", "48000.00", "47000.00", DateTime.UtcNow.AddDays(1));
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }

        [TestMethod]
        [Description("Test to verify that CreateStopLimitOrderGTDSell creates a stop-limit sell order for BTC-USDC.")]
        public async Task Test_Orders_CreateStopLimitOrderGTDSellAsync()
        {
            await ExecuteRateLimitedTest(async () =>
            {
                Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
                var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                    "BTC-USDC", OrderSide.SELL, "0.0001", "40000.00", "40500.00", DateTime.UtcNow.AddDays(1));
                Assert.IsNotNull(orderNumber, "Order number should not be null.");
            });
        }



    }
}
