using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.ExchangeManagers;
using Coinbase.AdvancedTrade.Interfaces;

namespace Coinbase.AdvancedTrade
{
    /// <summary>
    /// Provides access to various functionalities of the Coinbase API.
    /// </summary>
    public sealed class CoinbaseClient
    {
        /// <summary>
        /// Gets the accounts manager, responsible for account-related operations.
        /// </summary>
        public IAccountsManager Accounts { get; }

        /// <summary>
        /// Gets the products manager, responsible for product-related operations.
        /// </summary>
        public IProductsManager Products { get; }

        /// <summary>
        /// Gets the orders manager, responsible for order-related operations.
        /// </summary>
        public IOrdersManager Orders { get; }

        /// <summary>
        /// Gets the fees manager, responsible for fee-related operations.
        /// </summary>
        public IFeesManager Fees { get; }

        /// <summary>
        /// Gets the public manager, responsible for public-related operations.
        /// </summary>
        public IPublicManager Public { get; }

        /// <summary>
        /// Gets the WebSocket manager, responsible for managing WebSocket connections.
        /// </summary>
        public WebSocketManager WebSocket { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbaseClient"/> class for interacting with the Coinbase API.
        /// </summary>
        /// <param name="apiKey">The API key for authentication with Coinbase.</param>
        /// <param name="apiSecret">The API secret for authentication with Coinbase.</param>
        /// <param name="websocketBufferSize">The buffer size for WebSocket messages in bytes (Default 5,242,880 bytes/ 5 MB).</param>
        public CoinbaseClient(string apiKey, string apiSecret, int websocketBufferSize = 5 * 1024 * 1024)
        {
            // Create an instance of CoinbaseAuthenticator with the provided credentials and API key type
            var authenticator = new CoinbaseAuthenticator(apiKey, apiSecret);

            // Initialize various service managers with the authenticator
            Accounts = new AccountsManager(authenticator);
            Products = new ProductsManager(authenticator);
            Orders = new OrdersManager(authenticator);
            Fees = new FeesManager(authenticator);
            Public = new PublicManager(authenticator);

            // Initialize WebSocketManager for real-time data feed
            WebSocket = new WebSocketManager("wss://advanced-trade-ws.coinbase.com", apiKey, apiSecret, websocketBufferSize);
        }

    }
}
