namespace Coinbase.AdvancedTrade.Enums
{
    /// <summary>
    /// Represents different types of channels for data streams.
    /// </summary>
    public enum ChannelType
    {
        /// <summary>
        /// Channel for candle data.
        /// </summary>
        Candles,

        /// <summary>
        /// Channel for heartbeat signals.
        /// </summary>
        Heartbeats,

        /// <summary>
        /// Channel for market trades.
        /// </summary>
        MarketTrades,

        /// <summary>
        /// Channel for status updates.
        /// </summary>
        Status,

        /// <summary>
        /// Channel for ticker information.
        /// </summary>
        Ticker,

        /// <summary>
        /// Channel for batch ticker information.
        /// </summary>
        TickerBatch,

        /// <summary>
        /// Channel for level 2 order book data.
        /// </summary>
        Level2,

        /// <summary>
        /// Channel for user-specific data.
        /// </summary>
        User
    }

    /// <summary>
    /// Represents the state of a WebSocket connection.
    /// </summary>
    public enum WebSocketState
    {
        /// <summary>
        /// No state set.
        /// </summary>
        None,

        /// <summary>
        /// The WebSocket connection is in the process of connecting.
        /// </summary>
        Connecting,

        /// <summary>
        /// The WebSocket connection is open and ready for communication.
        /// </summary>
        Open,

        /// <summary>
        /// A close frame has been sent to the WebSocket server.
        /// </summary>
        CloseSent,

        /// <summary>
        /// A close frame has been received from the WebSocket server.
        /// </summary>
        CloseReceived,

        /// <summary>
        /// The WebSocket connection is closed.
        /// </summary>
        Closed,

        /// <summary>
        /// The WebSocket connection was aborted due to an error.
        /// </summary>
        Aborted
    }
}
