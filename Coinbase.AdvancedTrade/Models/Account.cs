using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the account.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the currency code for the account.
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Gets or sets the available balance within the account.
        /// </summary>
        [JsonPropertyName("available_balance")]
        public Balance? AvailableBalance { get; set; }

        /// <summary>
        /// Indicates if this account is the default one for the user.
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }

        /// <summary>
        /// Indicates if this account is active.
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was last updated.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the account was deleted, if applicable.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Indicates if this account is ready for transactions.
        /// </summary>
        [JsonPropertyName("ready")]
        public bool Ready { get; set; }

        /// <summary>
        /// Gets or sets the funds on hold within the account.
        /// </summary>
        [JsonPropertyName("hold")]
        public Balance? Hold { get; set; }
    }

    /// <summary>
    /// Represents a balance value and its associated currency.
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Gets or sets the value of the balance.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }  // Consider converting to Decimal if working with financial data

        /// <summary>
        /// Gets or sets the currency code associated with the balance.
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }
}
