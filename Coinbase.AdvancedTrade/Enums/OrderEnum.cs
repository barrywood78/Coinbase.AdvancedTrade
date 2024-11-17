namespace Coinbase.AdvancedTrade.Enums
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order is open.
        /// </summary>
        OPEN,

        /// <summary>
        /// The order is cancelled.
        /// </summary>
        CANCELLED,

        /// <summary>
        /// The order is expired.
        /// </summary>
        EXPIRED
    }

    /// <summary>
    /// Represents the type of an order.
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// A market order.
        /// </summary>
        MARKET,

        /// <summary>
        /// A limit order.
        /// </summary>
        LIMIT,

        /// <summary>
        /// A stop order.
        /// </summary>
        STOP,

        /// <summary>
        /// A stop limit order.
        /// </summary>
        STOP_LIMIT,

        /// <summary>
        /// An unknown order type.
        /// </summary>
        UNKNOWN_ORDER_TYPE
    }

    /// <summary>
    /// Represents the side of an order (buy or sell).
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// A buy order.
        /// </summary>
        BUY,

        /// <summary>
        /// A sell order.
        /// </summary>
        SELL
    }

    /// <summary>
    /// Options for sort_by parameter for List Orders (Default is Creation Time)
    /// </summary>
    public enum ListOrdersSortBy
    {
        /// <summary>
        /// Sort by Limit Price
        /// </summary>
        LIMIT_PRICE,

        /// <summary>
        /// Sort by Last Fill Time
        /// </summary>
        LAST_FILL_TIME
    }

    /// <summary>
    /// Options for sort_by parameter for List Fills (Default is Creation Time)
    /// </summary>
    public enum ListFillsSortBy
    {
        /// <summary>
        /// Sort by Price
        /// </summary>
        PRICE,

        /// <summary>
        /// Sort by Trade Time
        /// </summary>
        TRADE_TIME
    }
}
