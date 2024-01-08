using Newtonsoft.Json;
using System;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents an account within the Coinbase system.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the account.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the currency code for the account.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the available balance within the account.
        /// </summary>
        [JsonProperty("available_balance")]
        public Balance AvailableBalance { get; set; }

        /// <summary>
        /// Indicates if this account is the default one for the user.
        /// </summary>
        [JsonProperty("default")]
        public bool Default { get; set; }

        /// <summary>
        /// Indicates if this account is active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was deleted, if applicable.
        /// </summary>
        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Indicates if this account is ready for transactions.
        /// </summary>
        [JsonProperty("ready")]
        public bool Ready { get; set; }

        /// <summary>
        /// Gets or sets the funds on hold within the account.
        /// </summary>
        [JsonProperty("hold")]
        public Balance Hold { get; set; }
    }

    /// <summary>
    /// Represents a balance value and its associated currency.
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Gets or sets the value of the balance.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }  // Consider converting to Decimal if working with financial data

        /// <summary>
        /// Gets or sets the currency code associated with the balance.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
