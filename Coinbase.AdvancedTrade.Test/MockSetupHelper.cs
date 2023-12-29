using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;

namespace Coinbase.AdvancedTradeTest.Helpers
{
    public static class MockSetupHelper
    {
        // Helper method to initialize a mock of IAccountsManager
        public static Mock<IAccountsManager> InitializeAccountsMock()
        {
            var mock = new Mock<IAccountsManager>();

            // Simplified async setup with ReturnsAsync
            mock.Setup(a => a.ListAccountsAsync(25, null))
                .ReturnsAsync(MockDataHelper.GetMockAccountData());

            mock.Setup(a => a.GetAccountAsync(It.IsAny<string>()))
                .Returns<string>(uuid =>
                    Task.FromResult(MockDataHelper.GetMockAccountData().FirstOrDefault(account => account.Uuid == uuid)));

            return mock;
        }

        // Helper method to initialize a mock of IProductsManager
        public static Mock<IProductsManager> InitializeProductsMock()
        {
            var mock = new Mock<IProductsManager>();

            mock.Setup(a => a.ListProductsAsync("SPOT"))
                .Returns(Task.FromResult<List<Product>?>(MockDataHelper.GetMockProductList()));

            mock.Setup(p => p.GetProductAsync("BTC-USDC"))
                .Returns((string productId) =>
                Task.FromResult(MockDataHelper.GetMockProductList().FirstOrDefault(product => product.ProductId == productId)));


            mock.Setup(p => p.GetMarketTradesAsync("BTC-USDC", 10))
                .ReturnsAsync(MockDataHelper.GetMockMarketTrades());

            mock.Setup(p => p.GetProductCandlesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Granularity>()))
                .Returns<string, string, string, Granularity>((productId, start, end, granularity) =>
                    Task.FromResult<List<Candle>?>(MockDataHelper.GetMockCandleData()));

            mock.Setup(p => p.GetProductBookAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns<string, int>((productId, limit) =>
                    Task.FromResult(MockDataHelper.GetMockProductBookData().FirstOrDefault(book => book.ProductId == productId)));

            mock.Setup(p => p.GetBestBidAskAsync(It.IsAny<List<string>>()))
                .Returns<List<string>>(productIds =>
                    Task.FromResult<List<ProductBook>?>(MockDataHelper.GetMockProductBookData()
                        .Where(book => book.ProductId != null && productIds.Contains(book.ProductId))
                        .ToList()));

            return mock;
        }

        // Helper method to initialize a mock of IOrdersManager
        public static Mock<IOrdersManager> InitializeOrdersMock()
        {
            var mock = new Mock<IOrdersManager>();

            mock.Setup(o => o.CancelOrdersAsync(It.IsAny<string[]>()))
                .ReturnsAsync(MockDataHelper.GetMockCancelOrderResults());

            mock.Setup(o => o.ListOrdersAsync("BTC-USDC", new OrderStatus[] { OrderStatus.CANCELLED }, new(2023, 10, 1), new(2023, 10, 31), OrderType.LIMIT, OrderSide.BUY))
                .ReturnsAsync(MockDataHelper.GetMockOrderList());

            mock.Setup(o => o.GetOrderAsync(It.IsAny<string>()))
                .Returns<string>(orderId =>
                    Task.FromResult(MockDataHelper.GetMockOrderList().FirstOrDefault(order => order.OrderId == orderId)));

            mock.Setup(o => o.ListFillsAsync(null, "BTC-USDC", new DateTime(2023, 10, 1), new DateTime(2023, 10, 31)))
                .ReturnsAsync(MockDataHelper.GetMockFillList());

            var fixedOrderId = "8e643561-e5d3-44a6-9da9-c1744eded24e";

            mock.Setup(o => o.CreateMarketOrderAsync(It.IsAny<string>(), It.IsAny<OrderSide>(), It.IsAny<string>()))
                .ReturnsAsync(fixedOrderId);

            mock.Setup(o => o.CreateLimitOrderGTCAsync(It.IsAny<string>(), It.IsAny<OrderSide>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(fixedOrderId);

            mock.Setup(o => o.CreateLimitOrderGTDAsync(It.IsAny<string>(), It.IsAny<OrderSide>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<bool>()))
                .ReturnsAsync(fixedOrderId);

            mock.Setup(o => o.CreateStopLimitOrderGTCAsync(It.IsAny<string>(), It.IsAny<OrderSide>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync(fixedOrderId);

            mock.Setup(o => o.CreateStopLimitOrderGTDAsync(It.IsAny<string>(), It.IsAny<OrderSide>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(fixedOrderId);

            return mock;
        }

        // Helper method to initialize a mock of IFeesManager
        public static Mock<IFeesManager> InitializeFeesMock()
        {
            var mock = new Mock<IFeesManager>();

            mock.Setup(f => f.GetTransactionsSummaryAsync(new(2023, 1, 1), new(2023, 12, 31), "CAD", "SPOT"))
                .ReturnsAsync(MockDataHelper.GetMockTransactionsSummary());

            return mock;
        }

        // Helper method to initialize a mock of ICommonManager
        public static Mock<ICommonManager> InitializeCommonMock()
        {
            var mock = new Mock<ICommonManager>();

            // Set up mock behavior for GetCoinbaseServerTimeAsync
            mock.Setup(c => c.GetCoinbaseServerTimeAsync())
                .ReturnsAsync(MockDataHelper.GetMockServerTime());

            return mock;
        }

    }
}
