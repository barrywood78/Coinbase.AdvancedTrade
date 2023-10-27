namespace Coinbase.AdvancedTrade.Enums
{
    public enum OrderStatus
    {
        OPEN,
        CANCELLED,
        EXPIRED
    }

    public enum OrderType
    {
        MARKET,
        LIMIT,
        STOP,
        STOP_LIMIT,
        UNKNOWN_ORDER_TYPE
    }

    public enum OrderSide
    {
        BUY,
        SELL
    }

}
