using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Provides base functionalities for interacting with Coinbase API.
    /// </summary>
    public abstract class BaseManager
    {
        /// <summary>
        /// Authenticator instance for Coinbase API authentication.
        /// </summary>
        protected readonly CoinbaseAuthenticator _authenticator;

        /// <summary>
        /// REST client for making API requests.
        /// </summary>
        protected readonly RestClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseManager"/> class.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator instance.</param>
        /// <param name="baseUrl">The base URL for the REST client.</param>
        protected BaseManager(CoinbaseAuthenticator authenticator, string baseUrl = "https://api.coinbase.com")
        {
            _authenticator = authenticator;
            _client = new RestClient(baseUrl);
        }
        
    }
}
