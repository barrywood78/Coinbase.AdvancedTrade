namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Provides access to various functionalities of the Coinbase API.
    /// </summary>
    public interface ICoinbaseClient
    {
        /// <summary>
        /// Gets the accounts manager, responsible for account-related operations.
        /// </summary>
        IAccountsManager Accounts { get; }

        /// <summary>
        /// Gets the products manager, responsible for product-related operations.
        /// </summary>
        IProductsManager Products { get; }

        /// <summary>
        /// Gets the orders manager, responsible for order-related operations.
        /// </summary>
        IOrdersManager Orders { get; }

        /// <summary>
        /// Gets the fees manager, responsible for fee-related operations.
        /// </summary>
        IFeesManager Fees { get; }

        /// <summary>
        /// Gets the public manager, responsible for public-related operations.
        /// </summary>
        IPublicManager Public { get; }

        /// <summary>
        /// Gets the WebSocket manager, responsible for managing WebSocket connections.
        /// </summary>
        WebSocketManager WebSocket { get; }
    }
}