using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Provides details about the trading session status in Coinbase's Futures Commission Merchant (FCM) environment.
    /// </summary>
    public class FcmTradingSessionDetails
    {
        /// <summary>
        /// Gets or sets a value indicating whether the FCM trading session is currently open.
        /// </summary>
        [JsonPropertyName("is_session_open")]
        public bool IsSessionOpen { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the FCM trading session opens.
        /// </summary>
        [JsonPropertyName("open_time")]
        public string? OpenTime { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the FCM trading session closes.
        /// </summary>
        [JsonPropertyName("close_time")]
        public string? CloseTime { get; set; }
    }

    /// <summary>
    /// Represents product-specific details within the Coinbase trading environment.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        [JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the current price of the product.
        /// </summary>
        [JsonPropertyName("price")]
        public string? Price { get; set; }

        /// <summary>
        /// Gets or sets the percentage change in price over the last 24 hours.
        /// </summary>
        [JsonPropertyName("price_percentage_change_24h")]
        public string? PricePercentageChange24h { get; set; }

        /// <summary>
        /// Gets or sets the trading volume of the product over the last 24 hours.
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public string? Volume24h { get; set; }

        /// <summary>
        /// Gets or sets the percentage change in volume over the last 24 hours.
        /// </summary>
        [JsonPropertyName("volume_percentage_change_24h")]
        public string? VolumePercentageChange24h { get; set; }

        /// <summary>
        /// Gets or sets the smallest allowable increment in the base currency for this product.
        /// </summary>
        [JsonPropertyName("base_increment")]
        public string? BaseIncrement { get; set; }

        /// <summary>
        /// Gets or sets the smallest allowable increment in the quote currency for this product.
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public string? QuoteIncrement { get; set; }

        /// <summary>
        /// Gets or sets the minimum order size in the quote currency for this product.
        /// </summary>
        [JsonPropertyName("quote_min_size")]
        public string? QuoteMinSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum order size in the quote currency for this product.
        /// </summary>
        [JsonPropertyName("quote_max_size")]
        public string? QuoteMaxSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum order size in the base currency for this product.
        /// </summary>
        [JsonPropertyName("base_min_size")]
        public string? BaseMinSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum order size in the base currency for this product.
        /// </summary>
        [JsonPropertyName("base_max_size")]
        public string? BaseMaxSize { get; set; }

        /// <summary>
        /// Gets or sets the human-readable name of the base currency.
        /// </summary>
        [JsonPropertyName("base_name")]
        public string? BaseName { get; set; }

        /// <summary>
        /// Gets or sets the human-readable name of the quote currency.
        /// </summary>
        [JsonPropertyName("quote_name")]
        public string? QuoteName { get; set; }

        /// <summary>
        /// Gets or sets the current status of the product (e.g., active, delisted).
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the product (e.g., spot, futures).
        /// </summary>
        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the quote currency.
        /// </summary>
        [JsonPropertyName("quote_currency_id")]
        public string? QuoteCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the base currency.
        /// </summary>
        [JsonPropertyName("base_currency_id")]
        public string? BaseCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the details related to the FCM trading session for this product.
        /// </summary>
        [JsonPropertyName("fcm_trading_session_details")]
        public FcmTradingSessionDetails? FcmTradingSessionDetails { get; set; }

        /// <summary>
        /// Gets or sets the midpoint price between the best bid and best ask.
        /// </summary>
        [JsonPropertyName("mid_market_price")]
        public string? MidMarketPrice { get; set; }

        /// <summary>
        /// Gets or sets an alternate name for the product.
        /// </summary>
        [JsonPropertyName("alias")]
        public string? Alias { get; set; }

        /// <summary>
        /// Gets or sets a list of all aliases for this product.
        /// </summary>
        [JsonPropertyName("alias_to")]
        public List<string>? AliasTo { get; set; }

        /// <summary>
        /// Gets or sets the display symbol for the base currency.
        /// </summary>
        [JsonPropertyName("base_display_symbol")]
        public string? BaseDisplaySymbol { get; set; }

        /// <summary>
        /// Gets or sets the display symbol for the quote currency.
        /// </summary>
        [JsonPropertyName("quote_display_symbol")]
        public string? QuoteDisplaySymbol { get; set; }

        /// <summary>
        /// Gets or sets the allowable price increment for placing orders.
        /// </summary>
        [JsonPropertyName("price_increment")]
        public string? PriceIncrement { get; set; }
    }
}
