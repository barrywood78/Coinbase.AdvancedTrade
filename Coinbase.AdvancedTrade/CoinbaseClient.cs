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
        /// Initializes a new instance of the <see cref="CoinbaseClient"/> class for real-world usage.
        /// </summary>
        /// <param name="apiKey">The API key for authentication.</param>
        /// <param name="apiSecret">The API secret for authentication.</param>
        public CoinbaseClient(string apiKey, string apiSecret)
        {
            var authenticator = new CoinbaseAuthenticator(apiKey, apiSecret);
            Accounts = new AccountsManager(authenticator);
            Products = new ProductsManager(authenticator);
            Orders = new OrdersManager(authenticator);
            Fees = new FeesManager(authenticator);
            Common = new CommonManager(authenticator);
            WebSocket = new WebSocketManager("wss://advanced-trade-ws.coinbase.com", apiKey, apiSecret);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbaseClient"/> class for testing purposes.
        /// </summary>
        /// <param name="accounts">The mock or stub accounts manager for testing.</param>
        /// <param name="products">The mock or stub products manager for testing.</param>
        /// <param name="orders">The mock or stub orders manager for testing.</param>
        /// <param name="fees">The mock or stub fees manager for testing.</param>
        public CoinbaseClient(IAccountsManager accounts, IProductsManager products, IOrdersManager orders, IFeesManager fees, ICommonManager common)
        {
            Accounts = accounts;
            Products = products;
            Orders = orders;
            Fees = fees;
            Common = common;
        }
    }
}
