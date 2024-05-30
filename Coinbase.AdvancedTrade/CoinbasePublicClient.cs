using Coinbase.AdvancedTrade.ExchangeManagers;
using Coinbase.AdvancedTrade.Interfaces;

namespace Coinbase.AdvancedTrade
{
    /// <summary>
    /// Provides access to various public functionalities of the Coinbase API without requiring authentication.
    /// </summary>
    public sealed class CoinbasePublicClient
    {
        /// <summary>
        /// Gets the public manager, responsible for public-related operations such as retrieving server time.
        /// </summary>
        public IPublicManager Public { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbasePublicClient"/> class for interacting with the public Coinbase API without authentication.
        /// </summary>
        public CoinbasePublicClient()
        {
            Public = new PublicManager();
        }
    }
}
