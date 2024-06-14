using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.ExchangeManagers;
using Coinbase.AdvancedTrade.Interfaces;

namespace Coinbase.AdvancedTrade
{
    /// <summary>
    /// Provides access to various functionalities of the Coinbase API using OAuth2 authentication.
    /// </summary>
    public sealed class CoinbaseOauth2Client
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
        /// Initializes a new instance of the <see cref="CoinbaseOauth2Client"/> class for interacting with the Coinbase API using OAuth2 authentication.
        /// </summary>
        /// <param name="oAuth2AccessToken">The OAuth2 access token for authentication with Coinbase.</param>
        public CoinbaseOauth2Client(string oAuth2AccessToken)
        {
            // Create an instance of CoinbaseAuthenticator with the provided OAuth2 access token
            var authenticator = new CoinbaseAuthenticator(oAuth2AccessToken);

            // Initialize various service managers with the authenticator
            Accounts = new AccountsManager(authenticator);
            Products = new ProductsManager(authenticator);
            Orders = new OrdersManager(authenticator);
            Fees = new FeesManager(authenticator);
            Public = new PublicManager(authenticator);
        }
    }
}