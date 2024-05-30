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
}
