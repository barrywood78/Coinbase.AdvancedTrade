using Coinbase.AdvancedTrade.Models;
using System.Text.Json;

namespace Coinbase.AdvancedTradeTest.Helpers
{
    public static class MockDataHelper
    {
        public static List<Account> GetMockAccountData()
        {
            return new List<Account>
            {
                new Account
                {
                    Uuid = "bae8ad62-f752-54f2-af7b-7b7566947202",
                    Name = "USDC Wallet",
                    Currency = "USDC",
                    AvailableBalance = new Balance
                    {
                        Value = "1000", 
                        Currency = "USDC"
                    },
                    Default = true,
                    Active = true,
                    CreatedAt = new DateTime(2021, 1, 23, 3, 42, 36),
                    UpdatedAt = new DateTime(2023, 10, 12, 8, 19, 55),
                    Type = "ACCOUNT_TYPE_CRYPTO",
                    Ready = true
                },
                new Account
                {
                    Uuid = "08f06321-4384-5bf0-8f3e-b27de6cc56e5",
                    Name = "BTC Wallet",
                    Currency = "BTC",
                    AvailableBalance = new Balance
                    {
                        Value = "0.5", 
                        Currency = "BTC"
                    },
                    Default = true,
                    Active = true,
                    CreatedAt = new DateTime(2021, 1, 15, 6, 10, 14),
                    UpdatedAt = new DateTime(2023, 10, 12, 8, 19, 55),
                    Type = "ACCOUNT_TYPE_CRYPTO",
                    Ready = true
                }
            };
        }

        public static List<CancelOrderResult> GetMockCancelOrderResults()
        {
            return new List<CancelOrderResult>
            {
                new CancelOrderResult
                {
                    OrderId = "d5265ba9-35aa-46ef-afad-0cac9f480e4d",
                    Success = true,
                    FailureReason = "UNKNOWN_CANCEL_FAILURE_REASON"
                }
            };
        }


        public static TransactionsSummary GetMockTransactionsSummary()
        {
            return new TransactionsSummary
            {
                AdvancedTradeOnlyFees = 0.23399843280995314,
                AdvancedTradeOnlyVolume = 29.249803851244142,
                CoinbaseProFees = 0,
                CoinbaseProVolume = 0,
                FeeTier = new FeeTier
                {
                    MakerFeeRate = "0.006",
                    PricingTier = "",
                    TakerFeeRate = "0.008",
                    UsdFrom = "0",
                    UsdTo = "1000"
                },
                GoodsAndServicesTax = null,
                Low = 0,
                MarginRate = null,
                TotalFees = 0.23399843280995314,
                TotalVolume = 29.249803851244142
            };
        }



        public static List<Order> GetMockOrderList()
        {
            return new List<Order>
            {
                new Order
                {
                    AverageFilledPrice = "0",
                    CancelMessage = "User requested cancel",
                    ClientOrderId = "b0e19f30-530e-4334-9bf2-1e0b1c6b0b53",
                    CompletionPercentage = "0",
                    CreatedTime = new DateTime(2023, 10, 12, 20, 19, 13),
                    Fee = "",
                    FilledSize = "0",
                    FilledValue = "0",
                    IsLiquidation = false,
                    NumberOfFills = "0",
                    OrderConfiguration = new OrderConfiguration
                    {
                        LimitGtc = new LimitGtc() 
                    },
                    OrderId = "d5265ba9-35aa-46ef-afad-0cac9f480e4d",
                    OrderPlacementSource = "RETAIL_ADVANCED",
                    OrderType = "LIMIT",
                    OutstandingHoldAmount = "0",
                    PendingCancel = false,
                    ProductId = "BTC-USDC",
                    ProductType = "SPOT",
                    RejectMessage = "",
                    RejectReason = "REJECT_REASON_UNSPECIFIED",
                    Settled = false,
                    Side = "BUY",
                    SizeInQuote = false,
                    SizeInclusiveOfFees = false,
                    Status = "CANCELLED",
                    TimeInForce = "GOOD_UNTIL_CANCELLED",
                    TotalFees = "0",
                    TotalValueAfterFees = "0",
                    TriggerStatus = "INVALID_ORDER_TYPE",
                    UserId = "3d7ca25b-6e00-523e-a248-2f6c2cc1de06"
                },
                new Order
                {
                    AverageFilledPrice = "0",
                    CancelMessage = "User requested cancel",
                    ClientOrderId = "1f9e188a-6e20-4618-a43b-d6425cea35bc",
                    CompletionPercentage = "0",
                    CreatedTime = new DateTime(2023, 10, 7, 3, 16, 53),
                    Fee = "",
                    FilledSize = "0",
                    FilledValue = "0",
                    IsLiquidation = false,
                    NumberOfFills = "0",
                    OrderConfiguration = new OrderConfiguration
                    {
                        LimitGtc = new LimitGtc() 
                    },
                    OrderId = "75e5d09c-c0c7-4089-802e-69f6d672ec75",
                    OrderPlacementSource = "RETAIL_ADVANCED",
                    OrderType = "LIMIT",
                    OutstandingHoldAmount = "0",
                    PendingCancel = false,
                    ProductId = "BTC-USDC",
                    ProductType = "SPOT",
                    RejectMessage = "",
                    RejectReason = "REJECT_REASON_UNSPECIFIED",
                    Settled = false,
                    Side = "BUY",
                    SizeInQuote = false,
                    SizeInclusiveOfFees = false,
                    Status = "CANCELLED",
                    TimeInForce = "GOOD_UNTIL_CANCELLED",
                    TotalFees = "0",
                    TotalValueAfterFees = "0",
                    TriggerStatus = "INVALID_ORDER_TYPE",
                    UserId = "3d7ca25b-6e00-523e-a248-2f6c2cc1de06"
                }
            };
        }



        public static List<Fill> GetMockFillList()
        {
            return new List<Fill>
            {
                new Fill
                {
                    Commission = "0.117003218784",
                    EntryId = "60e540b300e0ae8ed1879b17be2514d0ac1b8ab385376cead8b7b5c6068a9f10",
                    LiquidityIndicator = "TAKER",
                    OrderId = "61602a73-5f57-4bea-aa58-b2536a3dddc4",
                    Price = "26711.6",
                    ProductId = "BTC-USDC",
                    SequenceTimestamp = "2023-10-12T19:38:35.699868Z",
                    Side = "SELL",
                    Size = "0.00054753",
                    SizeInQuote = false,
                    TradeId = "666ac737-a3f1-44ee-8d7f-f7abdffa12db",
                    TradeTime = "2023-10-12T19:38:35.696Z",
                    TradeType = "FILL",
                    UserId = "3d7ca25b-6e00-523e-a248-2f6c2cc1de06"
                },
                new Fill
                {
                    Commission = "0.000072307976127",
                    EntryId = "1e53f323d6f226dcf7f382cf9820b18c844a037ecc28c63ce5ea2057c46e0ab0",
                    LiquidityIndicator = "UNKNOWN_LIQUIDITY_INDICATOR",
                    OrderId = "0d720c1e-b4cc-42f6-8291-db7f2d612ed0",
                    Price = "26706.83",
                    ProductId = "BTC-USDC",
                    SequenceTimestamp = "2023-10-12T19:37:36.354039Z",
                    Side = "BUY",
                    Size = "0.009038497015873",
                    SizeInQuote = true,
                    TradeId = "500cf05b-0679-4cc7-9cc5-db6ed538f891",
                    TradeTime = "2023-10-12T19:37:36.353126314Z",
                    TradeType = "FILL",
                    UserId = "3d7ca25b-6e00-523e-a248-2f6c2cc1de06"
                },
                new Fill
                {
                    Commission = "0.116911819008",
                    EntryId = "04803194c479efb21a538772da831ae66c4b439682bb8490d79191f002a53d82",
                    LiquidityIndicator = "TAKER",
                    OrderId = "0d720c1e-b4cc-42f6-8291-db7f2d612ed0",
                    Price = "26706.83",
                    ProductId = "BTC-USDC",
                    SequenceTimestamp = "2023-10-12T19:37:36.321874Z",
                    Side = "BUY",
                    Size = "14.613977376",
                    SizeInQuote = true,
                    TradeId = "1236c6cb-ea4c-4f4a-b933-b8e7befe3737",
                    TradeTime = "2023-10-12T19:37:36.319068Z",
                    TradeType = "FILL",
                    UserId = "3d7ca25b-6e00-523e-a248-2f6c2cc1de06"
                }
            };
        }


        public static List<Product> GetMockProductList()
        {
            return new List<Product>
            {
                new Product
                {
                    Alias = "BTC-USD",
                    BaseCurrencyId = "BTC",
                    BaseDisplaySymbol = "BTC",
                    BaseIncrement = "0.00000001",
                    BaseMaxSize = "2500",
                    BaseMinSize = "0.000016",
                    BaseName = "Bitcoin",
                    ProductId = "BTC-USDC",
                    ProductType = "SPOT",
                    QuoteCurrencyId = "USDC",
                    QuoteDisplaySymbol = "USD",
                    Price = "26737.06",
                    PriceIncrement = "0.01",
                    PricePercentageChange24h = "0.33661871194745",
                    Status = "online",
                    Volume24h = "7014.23935867",
                    VolumePercentageChange24h = "-24.28361707804761"
                },
                new Product
                {
                    Alias = "",
                    BaseCurrencyId = "BTC",
                    BaseDisplaySymbol = "BTC",
                    BaseIncrement = "0.00000001",
                    BaseMaxSize = "2500",
                    BaseMinSize = "0.000016",
                    BaseName = "Bitcoin",
                    ProductId = "BTC-USD",
                    ProductType = "SPOT",
                    QuoteCurrencyId = "USD",
                    QuoteDisplaySymbol = "USD",
                    Price = "26737.06",
                    PriceIncrement = "0.01",
                    PricePercentageChange24h = "0.33661871194745",
                    Status = "online",
                    Volume24h = "7014.23935867",
                    VolumePercentageChange24h = "-24.28361707804761"
                },
                new Product
                {
                    Alias = "",
                    BaseCurrencyId = "USDT",
                    BaseDisplaySymbol = "USDT",
                    BaseIncrement = "0.01",
                    BaseMaxSize = "9000000",
                    BaseMinSize = "0.99",
                    BaseName = "Tether",
                    ProductId = "USDT-USD",
                    ProductType = "SPOT",
                    QuoteCurrencyId = "USD",
                    QuoteDisplaySymbol = "USD",
                    Price = "0.99964",
                    PriceIncrement = "0.00001",
                    PricePercentageChange24h = "0.00600252105884",
                    Status = "online",
                    Volume24h = "110260585.8",
                    VolumePercentageChange24h = "4.00780660299781"
                },
                new Product
                {
                    Alias = "",
                    BaseCurrencyId = "ETH",
                    BaseDisplaySymbol = "ETH",
                    BaseIncrement = "0.00000001",
                    BaseMaxSize = "38000",
                    BaseMinSize = "0.00022",
                    BaseName = "Ethereum",
                    ProductId = "ETH-USD",
                    ProductType = "SPOT",
                    QuoteCurrencyId = "USD",
                    QuoteDisplaySymbol = "USD",
                    Price = "1543.02",
                    PriceIncrement = "0.01",
                    PricePercentageChange24h = "0.84834383414813",
                    Status = "online",
                    Volume24h = "51137.30003547",
                    VolumePercentageChange24h = "-26.39121154319378"
                }
            };
        }



        public static MarketTrades GetMockMarketTrades()
        {
            return new MarketTrades
            {
                BestAsk = "26733.25",
                BestBid = "26732.59",
                Trades = new List<Trade>
                {
                    new Trade
                    {
                        Price = "26733.25",
                        ProductId = "BTC-USD",
                        Side = "SELL",
                        Size = "0.00349882",
                        Time = "2023-10-13T19:42:40.741859Z",
                        TradeId = "568233524"
                    }
                }
            };
        }




        public static List<Candle> GetMockCandleData()
        {
            return new List<Candle>
            {
                new Candle
                {
                    StartUnix = "1693612800",
                    Low = "25790.1",
                    High = "25803.89",
                    Open = "25795.61",
                    Close = "25791.41",
                    Volume = "10.62437035"
                },
                new Candle
                {
                    StartUnix = "1693612500",
                    Low = "25790.35",
                    High = "25811.33",
                    Open = "25811.02",
                    Close = "25794.88",
                    Volume = "16.92922277"
                },
            };
        }



        public static List<ProductBook> GetMockProductBookData()
        {
            return new List<ProductBook>
            {
                new ProductBook
                {
                    ProductId = "BTC-USDC",
                    Bids = new List<Offer>
                    {
                        new Offer { Price = "28986.64", Size = "0.07265273" },
                        new Offer { Price = "28986.55", Size = "0.07965558" },
                        new Offer { Price = "28986.48", Size = "0.04941081" }
                    },
                    Asks = new List<Offer>
                    {
                        new Offer { Price = "28995.27", Size = "0.07549657" },
                        new Offer { Price = "28995.47", Size = "0.02583124" },
                        new Offer { Price = "28996.03", Size = "0.004" }
                    },
                    Time = DateTime.Parse("2023-10-16T13:38:24.174440Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
                },
                new ProductBook
                {
                    ProductId = "ETH-USDC",
                    Bids = new List<Offer>
                    {
                        new Offer { Price = "28995.27", Size = "0.07549657" },
                        new Offer { Price = "28995.47", Size = "0.02583124" },
                        new Offer { Price = "28996.03", Size = "0.004" }
                    },
                    Asks = new List<Offer>
                    {
                        new Offer { Price = "28995.27", Size = "0.07549657" },
                        new Offer { Price = "28995.47", Size = "0.02583124" },
                        new Offer { Price = "28996.03", Size = "0.004" }
                    },
                    Time = DateTime.Parse("2023-10-16T13:38:24.174440Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
                }
            };
        }


        /// <summary>
        /// Gets mock server time data for testing purposes.
        /// </summary>
        /// <returns>A mock ServerTime object.</returns>
        public static ServerTime GetMockServerTime()
        {
            return new ServerTime
            {
                Iso = "2023-11-28T00:00:22Z",
                EpochSeconds = "1701129622",
                EpochMillis = "1701129622115"
            };
        }



        public static EditOrderPreviewResult GetMockEditOrderPreviewResponse()
        {
            return new EditOrderPreviewResult
            {
                Slippage = "0.01", 
                OrderTotal = "10500", 
                CommissionTotal = "50", 
                QuoteSize = "10000", 
                BaseSize = "0.5", 
                BestBid = "10400", 
                BestAsk = "10600",
                AverageFilledPrice = "10500" 
            };
        }




    }
}
