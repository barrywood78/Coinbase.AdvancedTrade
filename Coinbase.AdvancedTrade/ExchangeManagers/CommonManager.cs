using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Manages common activities, including server time retrieval, for the Coinbase Advanced Trade API.
    /// </summary>
    public class CommonManager : BaseManager, ICommonManager
    {
        /// <summary>
        /// Initializes a new instance of the CommonManager class.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator.</param>
        public CommonManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

        /// <inheritdoc/>
        public async Task<ServerTime> GetCoinbaseServerTimeAsync()
        {
            try
            {
                // Send an authenticated request to the Coinbase API to retrieve the server time.
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/time", null) ?? new Dictionary<string, object>();

                // Deserialize the response into a ServerTime object.
                return DeserializeDictionary<ServerTime>(response);
            }
            catch (Exception ex)
            {
                // Wrap and rethrow exceptions to provide more context.
                throw new InvalidOperationException("Failed to get server time", ex);
            }
        }
    }
}
