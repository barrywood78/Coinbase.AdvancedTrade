
using System;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade;
using Coinbase.AdvancedTrade.Enums;

namespace Framework._4._8.TestApp
{
    class Program
    {
        static CoinbaseClient coinbaseClient;
        static bool _isCleanupDone = false;

        static async Task Main(string[] args)
        {
            // Coinbase Legacy API Keys
            //var apiKey = Environment.GetEnvironmentVariable("COINBASE_API_KEY", EnvironmentVariableTarget.User)
            //             ?? throw new InvalidOperationException("API Key not found");
            //var apiSecret = Environment.GetEnvironmentVariable("COINBASE_API_SECRET", EnvironmentVariableTarget.User)
            //               ?? throw new InvalidOperationException("API Secret not found");
            //coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

            // Coinbase Cloud Trading Keys
            var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
                         ?? throw new InvalidOperationException("API Key not found");
            var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
                           ?? throw new InvalidOperationException("API Secret not found");
            coinbaseClient = new CoinbaseClient(apiKey, apiSecret, ApiKeyType.CloudTrading);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(".NET Framework 4.8 Coinbase.AdvancedTrade Test Application");
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. REST API Tests");
                Console.WriteLine("2. WebSocket Tests");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await RunRestApiTests();
                        break;
                    case "2":
                        await RunWebSocketTests();
                        break;
                    case "0":
                        return; // Exit the application
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static async Task RunRestApiTests()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Fetching product details
            try
            {
                var product = await coinbaseClient?.Products.GetProductAsync("BTC-USDC");

                if (product != null)
                {
                    Console.WriteLine("\nProducts.GetProductAsync('BTC-USDC')");
                    Console.WriteLine($" - Product ID: {product.ProductId}");
                    Console.WriteLine($" - Price: {product.Price}");
                    Console.WriteLine($" - Volume 24h: {product.Volume24h}");
                    Console.WriteLine("\n\n");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve product details from Products.GetProductAsync('BTC-USDC').");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product details: {ex.Message}");
            }

            // Creating a Buy Market Order
            try
            {
                var buyOrderNum = await coinbaseClient.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.BUY, "1");
                if (buyOrderNum != null)
                {
                    Console.WriteLine("\nCreateMarketOrderAsync(\"BTC-USDC\", OrderSide.BUY, \"1\")");
                    Console.WriteLine($" - Order ID: {buyOrderNum}");
                }
                else
                {
                    Console.WriteLine("Failed to create buy market order.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating buy market order: {ex.Message}");
            }

            // Creating a Sell Market Order
            try
            {
                var sellOrderNum = await coinbaseClient.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.SELL, "0.000035");
                if (sellOrderNum != null)
                {
                    Console.WriteLine("\nCreateMarketOrderAsync(\"BTC-USDC\", OrderSide.SELL, \"0.000035\")");
                    Console.WriteLine($" - Order ID: {sellOrderNum}");
                }
                else
                {
                    Console.WriteLine("Failed to create sell market order.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sell market order: {ex.Message}");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }


        static async Task RunWebSocketTests()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var webSocketManager = coinbaseClient.WebSocket;
            AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
            Console.CancelKeyPress += async (s, e) =>
            {
                e.Cancel = true; // Prevent the process from terminating immediately
                await CleanupAsync(webSocketManager);
            };

            webSocketManager.CandleMessageReceived += (sender, candleData) =>
            {
                Console.WriteLine($"Received new candle data at {DateTime.UtcNow}");
            };

            webSocketManager.MessageReceived += (sender, e) =>
            {
                Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
            };

            try
            {
                Console.WriteLine("Connecting to the WebSocket...");
                await webSocketManager.ConnectAsync();

                Console.WriteLine("Subscribing to candles...");
                await webSocketManager.SubscribeAsync(new[] { "BTC-USDC" }, ChannelType.Candles);

                Console.WriteLine("Press any key to unsubscribe and exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                if (!_isCleanupDone)
                {
                    await CleanupAsync(webSocketManager);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        static async Task CleanupAsync(WebSocketManager webSocketManager)
        {
            if (_isCleanupDone) return;  // Return immediately if cleanup has been done

            Console.WriteLine("Unsubscribing...");
            await webSocketManager.UnsubscribeAsync(new[] { "BTC-USDC" }, ChannelType.Candles);

            Console.WriteLine("Disconnecting...");
            await webSocketManager.DisconnectAsync();

            _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
        }


        




    }
}
