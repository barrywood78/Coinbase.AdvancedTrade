namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Represents the primary interface for interacting with the Coinbase API client.
    /// </summary>
    public interface ICoinbaseClient
    {
        /// <summary>
        /// Gets the manager for account-related operations.
        /// </summary>
        IAccountsManager Accounts { get; }

        /// <summary>
        /// Gets the manager for product-related operations.
        /// </summary>
        IProductsManager Products { get; }

        /// <summary>
        /// Gets the manager for order-related operations.
        /// </summary>
        IOrdersManager Orders { get; }

        /// <summary>
        /// Gets the manager for fee-related operations.
        /// </summary>
        IFeesManager Fees { get; }
    }
}
