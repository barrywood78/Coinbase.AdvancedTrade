using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Manages account-related activities for the Coinbase Advanced Trade API.
    /// </summary>
    public class AccountsManager : BaseManager, IAccountsManager
    {
        /// <summary>
        /// Initializes a new instance of the AccountsManager class.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator.</param>
        public AccountsManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

        /// <inheritdoc/>
        public async Task<List<Account>> ListAccountsAsync(int limit = 49, string cursor = null)
        {
            try
            {
                var parameters = new { limit, cursor };
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/accounts", ConvertToDictionary(parameters)) ?? new Dictionary<string, object>();

                return DeserializeJsonElement<List<Account>>(response, "accounts");
            }
            catch (Exception ex)
            {
                // Wrap and rethrow exceptions to provide more context.
                throw new InvalidOperationException("Failed to list accounts", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<Account> GetAccountAsync(string accountUuid)
        {
            // Check if the provided UUID is valid.
            if (string.IsNullOrEmpty(accountUuid))
            {
                throw new ArgumentException("Account UUID cannot be null or empty", nameof(accountUuid));
            }

            try
            {
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", $"/api/v3/brokerage/accounts/{accountUuid}") ?? new Dictionary<string, object>();

                return DeserializeJsonElement<Account>(response, "account");
            }
            catch (Exception ex)
            {
                // Wrap and rethrow exceptions to provide more context.
                throw new InvalidOperationException($"Failed to get account with UUID {accountUuid}", ex);
            }
        }
    }
}
