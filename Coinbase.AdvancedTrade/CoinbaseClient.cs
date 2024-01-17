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
        /// Gets the common manager, responsible for common-related operations.
        /// </summary>
        public ICommonManager Common { get; }

        /// <summary>
        /// Gets the WebSocket manager, responsible for managing WebSocket connections.
        /// </summary>
        public WebSocketManager WebSocket { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbaseClient"/> class for interacting with the Coinbase API.
        /// </summary>
        /// <param name="apiKey">The API key for authentication with Coinbase.</param>
        /// <param name="apiSecret">The API secret for authentication with Coinbase.</param>
        /// <param name="apiKeyType">Specifies the type of API key used. This can be either a Legacy key or a Cloud Trading key.
        ///     The Legacy key type uses HMACSHA256 signatures for authentication. The Cloud Trading key type uses JWT (JSON Web Token) for authentication.
        ///     The default is set to Legacy for backward compatibility.</param>
        public CoinbaseClient(string apiKey, string apiSecret, ApiKeyType apiKeyType = ApiKeyType.Legacy)
        {
            // Create an instance of CoinbaseAuthenticator with the provided credentials and API key type
            var authenticator = new CoinbaseAuthenticator(apiKey, apiSecret, apiKeyType);

            // Initialize various service managers with the authenticator
            Accounts = new AccountsManager(authenticator);
            Products = new ProductsManager(authenticator);
            Orders = new OrdersManager(authenticator);
            Fees = new FeesManager(authenticator);
            Common = new CommonManager(authenticator);

            // Initialize WebSocketManager for real-time data feed
            WebSocket = new WebSocketManager("wss://advanced-trade-ws.coinbase.com", apiKey, apiSecret, apiKeyType);
        }



    }
}
