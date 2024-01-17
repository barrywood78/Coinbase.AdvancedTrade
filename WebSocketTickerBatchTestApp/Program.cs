using Coinbase.AdvancedTrade;
using Coinbase.AdvancedTrade.Enums;

bool _isCleanupDone = false;

// Coinbase Legacy API Keys
//var apiKey = Environment.GetEnvironmentVariable("COINBASE_API_KEY", EnvironmentVariableTarget.User)
//             ?? throw new InvalidOperationException("API Key not found");
//var apiSecret = Environment.GetEnvironmentVariable("COINBASE_API_SECRET", EnvironmentVariableTarget.User)
//               ?? throw new InvalidOperationException("API Secret not found");
//var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

// Coinbase Cloud Trading Keys
var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret, ApiKeyType.CloudTrading);

WebSocketManager? webSocketManager = coinbaseClient.WebSocket;

AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;  // Prevent the process from terminating immediately
    await CleanupAsync(webSocketManager);
};

webSocketManager!.TickerBatchMessageReceived += (sender, tickerData) =>
{
    Console.WriteLine($"Received ticker batch data at {DateTime.UtcNow}");
};

webSocketManager.MessageReceived += (sender, e) =>
{
    Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
};

try
{
    Console.WriteLine("Connecting to the WebSocket...");
    await webSocketManager.ConnectAsync();

    Console.WriteLine("Subscribing to ticker batch...");
    await webSocketManager.SubscribeAsync(new[] { "BTC-USDC" }, ChannelType.TickerBatch);

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

async Task CleanupAsync(WebSocketManager? webSocketManager)
{
    if (_isCleanupDone) return;  // Return immediately if cleanup has been done

    Console.WriteLine("Unsubscribing from ticker batch...");
    await webSocketManager!.UnsubscribeAsync(new[] { "BTC-USDC" }, ChannelType.TickerBatch);

    Console.WriteLine("Disconnecting...");
    await webSocketManager.DisconnectAsync();

    _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
}
