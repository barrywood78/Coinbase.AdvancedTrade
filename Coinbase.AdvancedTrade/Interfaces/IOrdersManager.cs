using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Models;
using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTrade.Interfaces
{
    /// <summary>
    /// Provides asynchronous methods for managing and interacting with orders.
    /// </summary>
    public interface IOrdersManager
    {
        /// <summary>
        /// Asynchronously lists orders based on the provided criteria.
        /// </summary>
        /// <param name="productId">Optional product ID to filter the results.</param>
        /// <param name="orderStatus">Optional array of order statuses to filter the results.</param>
        /// <param name="startDate">Optional start date to filter the results.</param>
        /// <param name="endDate">Optional end date to filter the results.</param>
        /// <param name="orderType">Optional order type to filter the results.</param>
        /// <param name="orderSide">Optional order side to filter the results.</param>
        /// <returns>A task representing the operation. The task result contains a list of orders that match the given criteria.</returns>
        Task<List<Order>?> ListOrdersAsync(
            string? productId = null,
            OrderStatus[]? orderStatus = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            OrderType? orderType = null,
            OrderSide? orderSide = null
        );

        /// <summary>
        /// Asynchronously lists fills based on the provided criteria.
        /// </summary>
        /// <param name="orderId">Optional order ID to filter the results.</param>
        /// <param name="productId">Optional product ID to filter the results.</param>
        /// <param name="startSequenceTimestamp">Optional start timestamp to filter the results.</param>
        /// <param name="endSequenceTimestamp">Optional end timestamp to filter the results.</param>
        /// <returns>A task representing the operation. The task result contains a list of fills that match the given criteria.</returns>
        Task<List<Fill>?> ListFillsAsync(
            string? orderId = null,
            string? productId = null,
            DateTime? startSequenceTimestamp = null,
            DateTime? endSequenceTimestamp = null
        );

        /// <summary>
        /// Asynchronously retrieves a specific order by its ID.
        /// </summary>
        /// <param name="orderId">ID of the order to retrieve.</param>
        /// <returns>A task representing the operation. The task result contains the order with the given ID, or null if not found.</returns>
        Task<Order?> GetOrderAsync(string orderId);

        /// <summary>
        /// Asynchronously cancels a set of orders based on their IDs.
        /// </summary>
        /// <param name="orderIds">Array of order IDs to cancel.</param>
        /// <returns>A task representing the operation. The task result contains a list of results from the cancel operations.</returns>
        Task<List<CancelOrderResult>?> CancelOrdersAsync(string[] orderIds);

        /// <summary>
        /// Asynchronously creates a market order.
        /// </summary>
        /// <param name="productId">Product ID for which the order is being placed.</param>
        /// <param name="side">Side of the order (buy/sell).</param>
        /// <param name="size">Size of the order.</param>
        /// <returns>A task representing the operation. The task result contains the ID of the created order, or null if creation failed.</returns>
        Task<string?> CreateMarketOrderAsync(string productId, OrderSide side, string size);

        /// <summary>
        /// Asynchronously creates a limit order with good-till-canceled (GTC) duration.
        /// </summary>
        /// <param name="productId">Product ID for which the order is being placed.</param>
        /// <param name="side">Side of the order (buy/sell).</param>
        /// <param name="baseSize">Base size of the order.</param>
        /// <param name="limitPrice">Limit price for the order.</param>
        /// <param name="postOnly">Indicates if the order should only be posted.</param>
        /// <returns>A task representing the operation. The task result contains the ID of the created order, or null if creation failed.</returns>
        Task<string?> CreateLimitOrderGTCAsync(string productId, OrderSide side, string baseSize, string limitPrice, bool postOnly = true);

        /// <summary>
        /// Asynchronously creates a limit order with good-till-date (GTD) duration.
        /// </summary>
        /// <param name="productId">Product ID for which the order is being placed.</param>
        /// <param name="side">Side of the order (buy/sell).</param>
        /// <param name="baseSize">Base size of the order.</param>
        /// <param name="limitPrice">Limit price for the order.</param>
        /// <param name="endTime">Expiration time for the order.</param>
        /// <param name="postOnly">Indicates if the order should only be posted.</param>
        /// <returns>A task representing the operation. The task result contains the ID of the created order, or null if creation failed.</returns>
        Task<string?> CreateLimitOrderGTDAsync(string productId, OrderSide side, string baseSize, string limitPrice, DateTime endTime, bool postOnly = true);

        /// <summary>
        /// Asynchronously creates a stop limit order with good-till-canceled (GTC) duration.
        /// </summary>
        /// <param name="productId">Product ID for which the order is being placed.</param>
        /// <param name="side">Side of the order (buy/sell).</param>
        /// <param name="baseSize">Base size of the order.</param>
        /// <param name="limitPrice">Limit price for the order.</param>
        /// <param name="stopPrice">Stop price for the order.</param>
        /// <returns>A task representing the operation. The task result contains the ID of the created order, or null if creation failed.</returns>
        Task<string?> CreateStopLimitOrderGTCAsync(string productId, OrderSide side, string baseSize, string limitPrice, string stopPrice);

        /// <summary>
        /// Asynchronously creates a stop limit order with good-till-date (GTD) duration.
        /// </summary>
        /// <param name="productId">Product ID for which the order is being placed.</param>
        /// <param name="side">Side of the order (buy/sell).</param>
        /// <param name="baseSize">Base size of the order.</param>
        /// <param name="limitPrice">Limit price for the order.</param>
        /// <param name="stopPrice">Stop price for the order.</param>
        /// <param name="endTime">Expiration time for the order.</param>
        /// <returns>A task representing the operation. The task result contains the ID of the created order, or null if creation failed.</returns>
        Task<string?> CreateStopLimitOrderGTDAsync(string productId, OrderSide side, string baseSize, string limitPrice, string stopPrice, DateTime endTime);
    }
}
