using System;

namespace Coinbase.AdvancedTrade.Enums
{
    /// <summary>
    /// Specifies the type of API key used for authentication with the Coinbase API.
    /// </summary>
    public enum ApiKeyType
    {
        /// <summary>
        /// Legacy API key type, which is deprecated and will be removed in future versions.
        /// </summary>
        [Obsolete("Legacy API key type is deprecated and will be removed in future versions.")]
        Legacy,

        /// <summary>
        /// API key type for the Coinbase Developer Platform.
        /// </summary>
        CoinbaseDeveloperPlatform
    }
}
