using Coinbase.AdvancedTrade;
using Coinbase.AdvancedTrade.Enums;
using System;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    private static bool _isCleanupDone = false;
    private static CoinbaseClient? coinbaseClient = null;
    private static WebSocketManager? webSocketManager = null;
    private static string? apiKey;
    private static string? apiSecret;
    private static ApiKeyType _apiKeyType;

    static async Task Main(string[] args)
    {
        // Coinbase Developer Platform Keys
        apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
                 ?? throw new InvalidOperationException("API Key not found");
        apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
                   ?? throw new InvalidOperationException("API Secret not found");
        _apiKeyType = ApiKeyType.CoinbaseDeveloperPlatform;


        // Coinbase Legacy Keys
        //apiKey = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_KEY", EnvironmentVariableTarget.User)
        //         ?? throw new InvalidOperationException("API Key not found");
        //apiSecret = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_SECRET", EnvironmentVariableTarget.User)
        //           ?? throw new InvalidOperationException("API Secret not found");
        //var apiKeyType = ApiKeyType.Legacy;


        // Initialize the Coinbase client and WebSocket manager
        coinbaseClient = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret, apiKeyType: _apiKeyType);

        webSocketManager = coinbaseClient.WebSocket;

        // Handle process exit and Ctrl+C events to ensure cleanup
        AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync();
        Console.CancelKeyPress += async (s, e) =>
        {
            e.Cancel = true;  // Prevent the process from terminating immediately
            await CleanupAsync();
        };

        // Subscribe to WebSocket events
        SubscribeToWebSocketEvents(webSocketManager);

        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Connecting to the WebSocket...");
            await webSocketManager.ConnectAsync();
            LogWebSocketStatus();

            // Start tasks to monitor and handle WebSocket connection and status
            var statusTask = LogConnectionStatusAndSubscriptionsAsync();
            var monitorTask = MonitorAndReconnectAsync();
            var forceDisconnectTask = ForceDisconnectAsync();

            // Subscribe to necessary channels
            await SubscribeToChannelsAsync();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            if (!_isCleanupDone)
            {
                await CleanupAsync();
            }
        }
    }

    /// <summary>
    /// Performs cleanup actions like unsubscribing and disconnecting WebSocket.
    /// </summary>
    private static async Task CleanupAsync()
    {
        if (_isCleanupDone) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Unsubscribing from channels...");
        await webSocketManager!.UnsubscribeAsync(["BTC-USDC"], ChannelType.Heartbeats);
        await webSocketManager.UnsubscribeAsync(["BTC-USDC"], ChannelType.Candles);
        await webSocketManager.DisconnectAsync();
        Console.ResetColor();

        webSocketManager.Dispose();
        _isCleanupDone = true;
    }

    /// <summary>
    /// Subscribes to WebSocket events like heartbeat and candle messages.
    /// </summary>
    private static void SubscribeToWebSocketEvents(WebSocketManager webSocketManager)
    {
        webSocketManager.HeartbeatMessageReceived += (sender, heartbeatData) =>
        {
            Console.WriteLine($"Received heartbeat at {DateTime.UtcNow}");
        };

        webSocketManager.CandleMessageReceived += (sender, candleData) =>
        {
            Console.WriteLine($"Received candle data at {DateTime.UtcNow}");
        };

        webSocketManager.MessageReceived += (sender, e) =>
        {
            Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
        };
    }

    /// <summary>
    /// Logs WebSocket connection status and subscriptions every 5 seconds.
    /// </summary>
    private static async Task LogConnectionStatusAndSubscriptionsAsync()
    {
        while (true)
        {
            LogWebSocketStatus();
            await Task.Delay(5000);
        }
    }

    /// <summary>
    /// Monitors the WebSocket connection and attempts to reconnect if disconnected.
    /// </summary>
    private static async Task MonitorAndReconnectAsync()
    {
        while (true)
        {
            // Wait for 5 seconds before checking the WebSocket state again
            await Task.Delay(5000);

            // Check if the WebSocket connection is not open
            if (webSocketManager!.WebSocketState != WebSocketState.Open)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WebSocket disconnected. Attempting to reconnect...");
                Console.ResetColor();

                try
                {
                    // Attempt to reconnect and resubscribe to channels
                    await ReconnectAndResubscribeAsync();
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during the reconnection process
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred during reconnection and resubscription: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }


    /// <summary>
    /// Forces a disconnect every 30 seconds to test reconnection logic.
    /// </summary>
    private static async Task ForceDisconnectAsync()
    {
        while (true)
        {
            // Wait for 30 seconds before forcing a disconnect
            await Task.Delay(30000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Forcing disconnect...");

            // Forcefully disconnect the WebSocket
            await webSocketManager!.DisconnectAsync();

            Console.WriteLine("Forced disconnect complete.");
            Console.ResetColor();
        }
    }


    /// <summary>
    /// Reconnects to the WebSocket and resubscribes to necessary channels.
    /// </summary>
    private static async Task ReconnectAndResubscribeAsync()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Reconnecting to the WebSocket...");

        // Dispose of the current WebSocketManager instance to ensure a fresh connection
        webSocketManager!.Dispose();

        // Create a new WebSocketManager instance using the Coinbase client
        webSocketManager = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret, apiKeyType: _apiKeyType).WebSocket;

        // Re-subscribe to WebSocket events with the new WebSocketManager instance
        SubscribeToWebSocketEvents(webSocketManager);

        // Connect to the WebSocket server
        await webSocketManager.ConnectAsync();

        // Log the current status of the WebSocket connection
        LogWebSocketStatus();

        // Resubscribe to the necessary channels after reconnecting
        await SubscribeToChannelsAsync();
    }


    /// <summary>
    /// Subscribes to required channels like Heartbeats and Candles.
    /// </summary>
    private static async Task SubscribeToChannelsAsync()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Subscribing to channels...");

        await webSocketManager!.SubscribeAsync(["BTC-USDC"], ChannelType.Heartbeats);
        await webSocketManager.SubscribeAsync(["BTC-USDC"], ChannelType.Candles);

        LogWebSocketStatus();
        Console.ResetColor();
    }

    /// <summary>
    /// Logs the current WebSocket status and subscriptions.
    /// </summary>
    private static void LogWebSocketStatus()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"WebSocket connection state: {webSocketManager!.WebSocketState}");
        Console.WriteLine("Current subscriptions: " + string.Join(", ", webSocketManager.Subscriptions));
        Console.ResetColor();
    }
}