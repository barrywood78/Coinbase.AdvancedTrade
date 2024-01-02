using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models
{
    // Represents an order within the Coinbase AdvancedTrade system.
    public class Order
    {
        // The unique identifier for the order.
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        // The identifier for the product associated with the order.
        [JsonPropertyName("product_id")]
        public string? ProductId { get; set; }

        // The user identifier who created the order.
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        // Configuration details for the order.
        [JsonPropertyName("order_configuration")]
        public OrderConfiguration? OrderConfiguration { get; set; }

        // Indicates if the order is a buy or sell.
        [JsonPropertyName("side")]
        public string? Side { get; set; }

        // The client's custom identifier for the order.
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }

        // The current status of the order.
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        // How long the order will remain active.
        [JsonPropertyName("time_in_force")]
        public string? TimeInForce { get; set; }

        // The timestamp when the order was created.
        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        // The percentage of the order that has been completed.
        [JsonPropertyName("completion_percentage")]
        public string? CompletionPercentage { get; set; }

        // The quantity of the product that has been filled in the order.
        [JsonPropertyName("filled_size")]
        public string? FilledSize { get; set; }

        // The average price at which the order has been filled.
        [JsonPropertyName("average_filled_price")]
        public string? AverageFilledPrice { get; set; }

        // The fee associated with the order.
        [JsonPropertyName("fee")]
        public string? Fee { get; set; }

        // The number of times the order has been filled.
        [JsonPropertyName("number_of_fills")]
        public string? NumberOfFills { get; set; }

        // The total value of the filled portions of the order.
        [JsonPropertyName("filled_value")]
        public string? FilledValue { get; set; }

        // Indicates if the order is pending cancellation.
        [JsonPropertyName("pending_cancel")]
        public bool? PendingCancel { get; set; }

        // If true, the size of the order is specified in the quote currency.
        [JsonPropertyName("size_in_quote")]
        public bool? SizeInQuote { get; set; }

        // The total fees associated with the order.
        [JsonPropertyName("total_fees")]
        public string? TotalFees { get; set; }

        // If true, the size of the order includes fees.
        [JsonPropertyName("size_inclusive_of_fees")]
        public bool? SizeInclusiveOfFees { get; set; }

        // The total value of the order after fees have been deducted.
        [JsonPropertyName("total_value_after_fees")]
        public string? TotalValueAfterFees { get; set; }

        // The status of the order's trigger if applicable.
        [JsonPropertyName("trigger_status")]
        public string? TriggerStatus { get; set; }

        // The type of order (e.g. market, limit, stop).
        [JsonPropertyName("order_type")]
        public string? OrderType { get; set; }

        // Reason for order rejection, if applicable.
        [JsonPropertyName("reject_reason")]
        public string? RejectReason { get; set; }

        // Indicates if the order has been settled.
        [JsonPropertyName("settled")]
        public bool? Settled { get; set; }

        // The type of product associated with the order.
        [JsonPropertyName("product_type")]
        public string? ProductType { get; set; }

        // A message providing more details about the rejection reason.
        [JsonPropertyName("reject_message")]
        public string? RejectMessage { get; set; }

        // A message providing more details if the order was canceled.
        [JsonPropertyName("cancel_message")]
        public string? CancelMessage { get; set; }

        // The source from which the order was placed (e.g. web, API).
        [JsonPropertyName("order_placement_source")]
        public string? OrderPlacementSource { get; set; }

        // The amount of the order that is currently on hold.
        [JsonPropertyName("outstanding_hold_amount")]
        public string? OutstandingHoldAmount { get; set; }

        // Indicates if the order is a liquidation order.
        [JsonPropertyName("is_liquidation")]
        public bool? IsLiquidation { get; set; }

        // An array of the latest 5 edits per order.
        [JsonPropertyName("edit_history")]
        public List<EditHistoryEntry>? EditHistory { get; set; }
    }

    // Represents specific configurations for different types of orders.
    public class OrderConfiguration
    {
        // Configuration details for market-market IOC orders.
        [JsonPropertyName("market_market_ioc")]
        public MarketIoc? MarketIoc { get; set; }

        // Configuration details for limit-limit GTC orders.
        [JsonPropertyName("limit_limit_gtc")]
        public LimitGtc? LimitGtc { get; set; }

        // Configuration details for limit-limit GTD orders.
        [JsonPropertyName("limit_limit_gtd")]
        public LimitGtd? LimitGtd { get; set; }

        // Configuration details for stop-limit-stop-limit GTC orders.
        [JsonPropertyName("stop_limit_stop_limit_gtc")]
        public StopLimitGtc? StopLimitGtc { get; set; }

        // Configuration details for stop-limit-stop-limit GTD orders.
        [JsonPropertyName("stop_limit_stop_limit_gtd")]
        public StopLimitGtd? StopLimitGtd { get; set; }
    }

    // Represents configuration details for market-market IOC orders.
    public class MarketIoc
    {
        // The size of the order in the quote currency.
        [JsonPropertyName("quote_size")]
        public string? QuoteSize { get; set; }

        // The size of the order in the base currency.
        [JsonPropertyName("base_size")]
        public string? BaseSize { get; set; }
    }

    // Represents configuration details for limit-limit GTC orders.
    public class LimitGtc
    {
        // The size of the order in the base currency.
        [JsonPropertyName("base_size")]
        public string? BaseSize { get; set; }

        // The limit price for the order.
        [JsonPropertyName("limit_price")]
        public string? LimitPrice { get; set; }

        // Indicates if the order can only be posted to the order book.
        [JsonPropertyName("post_only")]
        public bool? PostOnly { get; set; }
    }

    // Represents configuration details for limit-limit GTD orders.
    public class LimitGtd : LimitGtc
    {
        // The time when the order will expire.
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }

    // Represents configuration details for stop-limit-stop-limit GTC orders.
    public class StopLimitGtc
    {
        // The size of the order in the base currency.
        [JsonPropertyName("base_size")]
        public string? BaseSize { get; set; }

        // The limit price for the order.
        [JsonPropertyName("limit_price")]
        public string? LimitPrice { get; set; }

        // The stop price for the order.
        [JsonPropertyName("stop_price")]
        public string? StopPrice { get; set; }

        // The direction in which the stop price is triggered (e.g. 'above', 'below').
        [JsonPropertyName("stop_direction")]
        public string? StopDirection { get; set; }
    }

    // Represents configuration details for stop-limit-stop-limit GTD orders.
    public class StopLimitGtd : StopLimitGtc
    {
        // The time when the order will expire.
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }


    // Represents an edit history entry for an order.
    public class EditHistoryEntry
    {
        // The price associated with the edit.
        [JsonPropertyName("price")]
        public string? Price { get; set; }

        // The size associated with the edit.
        [JsonPropertyName("size")]
        public string? Size { get; set; }

        // The timestamp when the edit was accepted.
        [JsonPropertyName("replace_accept_timestamp")]
        public DateTime? ReplaceAcceptTimestamp { get; set; }
    }


}
