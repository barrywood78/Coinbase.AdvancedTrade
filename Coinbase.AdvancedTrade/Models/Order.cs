using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models
{
    // Represents an order within the Coinbase AdvancedTrade system.
    public class Order
    {
        // The unique identifier for the order.
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        // The identifier for the product associated with the order.
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        // The user identifier who created the order.
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        // Configuration details for the order.
        [JsonProperty("order_configuration")]
        public OrderConfiguration OrderConfiguration { get; set; }

        // Indicates if the order is a buy or sell.
        [JsonProperty("side")]
        public string Side { get; set; }

        // The client's custom identifier for the order.
        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }

        // The current status of the order.
        [JsonProperty("status")]
        public string Status { get; set; }

        // How long the order will remain active.
        [JsonProperty("time_in_force")]
        public string TimeInForce { get; set; }

        // The timestamp when the order was created.
        [JsonProperty("created_time")]
        public DateTime? CreatedTime { get; set; }

        // The percentage of the order that has been completed.
        [JsonProperty("completion_percentage")]
        public string CompletionPercentage { get; set; }

        // The quantity of the product that has been filled in the order.
        [JsonProperty("filled_size")]
        public string FilledSize { get; set; }

        // The average price at which the order has been filled.
        [JsonProperty("average_filled_price")]
        public string AverageFilledPrice { get; set; }

        // The fee associated with the order.
        [JsonProperty("fee")]
        public string Fee { get; set; }

        // The number of times the order has been filled.
        [JsonProperty("number_of_fills")]
        public string NumberOfFills { get; set; }

        // The total value of the filled portions of the order.
        [JsonProperty("filled_value")]
        public string FilledValue { get; set; }

        // Indicates if the order is pending cancellation.
        [JsonProperty("pending_cancel")]
        public bool? PendingCancel { get; set; }

        // If true, the size of the order is specified in the quote currency.
        [JsonProperty("size_in_quote")]
        public bool? SizeInQuote { get; set; }

        // The total fees associated with the order.
        [JsonProperty("total_fees")]
        public string TotalFees { get; set; }

        // If true, the size of the order includes fees.
        [JsonProperty("size_inclusive_of_fees")]
        public bool? SizeInclusiveOfFees { get; set; }

        // The total value of the order after fees have been deducted.
        [JsonProperty("total_value_after_fees")]
        public string TotalValueAfterFees { get; set; }

        // The status of the order's trigger if applicable.
        [JsonProperty("trigger_status")]
        public string TriggerStatus { get; set; }

        // The type of order (e.g. market, limit, stop).
        [JsonProperty("order_type")]
        public string OrderType { get; set; }

        // Reason for order rejection, if applicable.
        [JsonProperty("reject_reason")]
        public string RejectReason { get; set; }

        // Indicates if the order has been settled.
        [JsonProperty("settled")]
        public bool? Settled { get; set; }

        // The type of product associated with the order.
        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        // A message providing more details about the rejection reason.
        [JsonProperty("reject_message")]
        public string RejectMessage { get; set; }

        // A message providing more details if the order was canceled.
        [JsonProperty("cancel_message")]
        public string CancelMessage { get; set; }

        // The source from which the order was placed (e.g. web, API).
        [JsonProperty("order_placement_source")]
        public string OrderPlacementSource { get; set; }

        // The amount of the order that is currently on hold.
        [JsonProperty("outstanding_hold_amount")]
        public string OutstandingHoldAmount { get; set; }

        // Indicates if the order is a liquidation order.
        [JsonProperty("is_liquidation")]
        public bool? IsLiquidation { get; set; }

        // An array of the latest 5 edits per order.
        [JsonProperty("edit_history")]
        public List<EditHistoryEntry> EditHistory { get; set; }
    }

    // Represents specific configurations for different types of orders.
    public class OrderConfiguration
    {
        // Configuration details for market-market IOC orders.
        [JsonProperty("market_market_ioc")]
        public MarketIoc MarketIoc { get; set; }

        // Configuration details for limit-limit GTC orders.
        [JsonProperty("limit_limit_gtc")]
        public LimitGtc LimitGtc { get; set; }

        // Configuration details for limit-limit GTD orders.
        [JsonProperty("limit_limit_gtd")]
        public LimitGtd LimitGtd { get; set; }

        // Configuration details for stop-limit-stop-limit GTC orders.
        [JsonProperty("stop_limit_stop_limit_gtc")]
        public StopLimitGtc StopLimitGtc { get; set; }

        // Configuration details for stop-limit-stop-limit GTD orders.
        [JsonProperty("stop_limit_stop_limit_gtd")]
        public StopLimitGtd StopLimitGtd { get; set; }
    }

    // Represents configuration details for market-market IOC orders.
    public class MarketIoc
    {
        // The size of the order in the quote currency.
        [JsonProperty("quote_size")]
        public string QuoteSize { get; set; }

        // The size of the order in the base currency.
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }
    }

    // Represents configuration details for limit-limit GTC orders.
    public class LimitGtc
    {
        // The size of the order in the base currency.
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }

        // The limit price for the order.
        [JsonProperty("limit_price")]
        public string LimitPrice { get; set; }

        // Indicates if the order can only be posted to the order book.
        [JsonProperty("post_only")]
        public bool? PostOnly { get; set; }
    }

    // Represents configuration details for limit-limit GTD orders.
    public class LimitGtd : LimitGtc
    {
        // The time when the order will expire.
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
    }

    // Represents configuration details for stop-limit-stop-limit GTC orders.
    public class StopLimitGtc
    {
        // The size of the order in the base currency.
        [JsonProperty("base_size")]
        public string BaseSize { get; set; }

        // The limit price for the order.
        [JsonProperty("limit_price")]
        public string LimitPrice { get; set; }

        // The stop price for the order.
        [JsonProperty("stop_price")]
        public string StopPrice { get; set; }

        // The direction in which the stop price is triggered (e.g. 'above', 'below').
        [JsonProperty("stop_direction")]
        public string StopDirection { get; set; }
    }

    // Represents configuration details for stop-limit-stop-limit GTD orders.
    public class StopLimitGtd : StopLimitGtc
    {
        // The time when the order will expire.
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
    }


    // Represents an edit history entry for an order.
    public class EditHistoryEntry
    {
        // The price associated with the edit.
        [JsonProperty("price")]
        public string Price { get; set; }

        // The size associated with the edit.
        [JsonProperty("size")]
        public string Size { get; set; }

        // The timestamp when the edit was accepted.
        [JsonProperty("replace_accept_timestamp")]
        public DateTime? ReplaceAcceptTimestamp { get; set; }
    }


}
