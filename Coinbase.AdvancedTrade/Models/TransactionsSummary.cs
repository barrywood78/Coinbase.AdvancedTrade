using Newtonsoft.Json;

namespace Coinbase.AdvancedTrade.Models
{
    /// <summary>
    /// Represents a tiered fee structure based on trade volume.
    /// </summary>
    public class FeeTier
    {
        /// <summary>
        /// Gets or sets the pricing tier identifier.
        /// </summary>
        [JsonProperty("pricing_tier")]
        public string PricingTier { get; set; }

        /// <summary>
        /// Gets or sets the starting USD value for this tier.
        /// </summary>
        [JsonProperty("usd_from")]
        public string UsdFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending USD value for this tier.
        /// </summary>
        [JsonProperty("usd_to")]
        public string UsdTo { get; set; }

        /// <summary>
        /// Gets or sets the fee rate for takers in this tier.
        /// </summary>
        [JsonProperty("taker_fee_rate")]
        public string TakerFeeRate { get; set; }

        /// <summary>
        /// Gets or sets the fee rate for makers in this tier.
        /// </summary>
        [JsonProperty("maker_fee_rate")]
        public string MakerFeeRate { get; set; }
    }

    /// <summary>
    /// Represents the rate at which margin is applied.
    /// </summary>
    public class MarginRate
    {
        /// <summary>
        /// Gets or sets the value of the margin rate.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// Represents the tax rate for goods and services.
    /// </summary>
    public class GoodsAndServicesTax
    {
        /// <summary>
        /// Gets or sets the tax rate value.
        /// </summary>
        [JsonProperty("rate")]
        public string Rate { get; set; }

        /// <summary>
        /// Gets or sets the type of tax applied (e.g., GST, VAT).
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents a summary of trading transactions.
    /// </summary>
    public class TransactionsSummary
    {
        /// <summary>
        /// Gets or sets the total trade volume.
        /// </summary>
        [JsonProperty("total_volume")]
        public double TotalVolume { get; set; }

        /// <summary>
        /// Gets or sets the total fees accumulated from trades.
        /// </summary>
        [JsonProperty("total_fees")]
        public double TotalFees { get; set; }

        /// <summary>
        /// Gets or sets the fee tier information for the trades.
        /// </summary>
        [JsonProperty("fee_tier")]
        public FeeTier FeeTier { get; set; }

        /// <summary>
        /// Gets or sets the margin rate applied to the trades.
        /// </summary>
        [JsonProperty("margin_rate")]
        public MarginRate MarginRate { get; set; }

        /// <summary>
        /// Gets or sets the goods and services tax information.
        /// </summary>
        [JsonProperty("goods_and_services_tax")]
        public GoodsAndServicesTax GoodsAndServicesTax { get; set; }

        /// <summary>
        /// Gets or sets the trade volume specific to Advanced Trade.
        /// </summary>
        [JsonProperty("advanced_trade_only_volume")]
        public double AdvancedTradeOnlyVolume { get; set; }

        /// <summary>
        /// Gets or sets the total fees specific to Advanced Trade.
        /// </summary>
        [JsonProperty("advanced_trade_only_fees")]
        public double AdvancedTradeOnlyFees { get; set; }

        /// <summary>
        /// Gets or sets the trade volume specific to Coinbase Pro.
        /// </summary>
        [JsonProperty("coinbase_pro_volume")]
        public double CoinbaseProVolume { get; set; }

        /// <summary>
        /// Gets or sets the total fees specific to Coinbase Pro.
        /// </summary>
        [JsonProperty("coinbase_pro_fees")]
        public double CoinbaseProFees { get; set; }

        /// <summary>
        /// Gets or sets the low value for a given period.
        /// </summary>
        [JsonProperty("low")]
        public double Low { get; set; }
    }
}
