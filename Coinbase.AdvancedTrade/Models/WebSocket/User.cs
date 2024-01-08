using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a user message from the Coinbase WebSocket API.
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// Gets or sets the channel for the user message.
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the client ID associated with the user message.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the user message was sent.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number for the user message.
        /// </summary>
        [JsonProperty("sequence_num")]
        public int SequenceNum { get; set; }

        /// <summary>
        /// Gets or sets the list of user events.
        /// </summary>
        [JsonProperty("events")]
        public List<UserEvent> Events { get; set; }
    }

    /// <summary>
    /// Represents an individual user event within a <see cref="UserMessage"/>.
    /// </summary>
    public class UserEvent
    {
        /// <summary>
        /// Gets or sets the type of the user event.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the list of user orders associated with the user event.
        /// </summary>
        [JsonProperty("orders")]
        public List<UserOrder> Orders { get; set; }
    }

    /// <summary>
    /// Represents details about a specific user order.
    /// </summary>
    public class UserOrder
    {
        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the client-specific order ID.
        /// </summary>
        [JsonProperty("client_order_id")]
        public string ClientOrderId { get; set; }

        /// <summary>
        /// Gets or sets the cumulative quantity of the order.
        /// </summary>
        [JsonProperty("cumulative_quantity")]
        public string CumulativeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the remaining quantity of the order.
        /// </summary>
        [JsonProperty("leaves_quantity")]
        public string LeavesQuantity { get; set; }

        /// <summary>
        /// Gets or sets the average price of the order.
        /// </summary>
        [JsonProperty("avg_price")]
        public string AvgPrice { get; set; }

        /// <summary>
        /// Gets or sets the total fees associated with the order.
        /// </summary>
        [JsonProperty("total_fees")]
        public string TotalFees { get; set; }

        /// <summary>
        /// Gets or sets the status of the order (e.g., "completed", "pending").
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product associated with the order.
        /// </summary>
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the order.
        /// </summary>
        [JsonProperty("creation_time")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the side of the order (e.g., "buy" or "sell").
        /// </summary>
        [JsonProperty("order_side")]
        public string OrderSide { get; set; }

        /// <summary>
        /// Gets or sets the type of the order (e.g., "limit", "market").
        /// </summary>
        [JsonProperty("order_type")]
        public string OrderType { get; set; }
    }
}
