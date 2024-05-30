using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using Coinbase.AdvancedTrade.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Manages order-related activities for the Coinbase Advanced Trade API.
    /// </summary>
    public class OrdersManager : BaseManager, IOrdersManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersManager"/> class.
        /// </summary>
        /// <param name="authenticator">The authenticator for Coinbase API requests.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided authenticator is null.</exception>
        public OrdersManager(CoinbaseAuthenticator authenticator) : base(authenticator)
        {
            if (authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator), "Authenticator cannot be null.");
            }
        }


        /// <inheritdoc/>
        public async Task<List<Order>> ListOrdersAsync(
            string productId = null,
            OrderStatus[] orderStatus = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            OrderType? orderType = null,
            OrderSide? orderSide = null)
        {
            // Guard against invalid OrderStatus combinations
            ValidateOrderStatus(orderStatus);

            // Use utility methods for conversion
            string[] orderStatusStrings = UtilityHelper.EnumToStringArray(orderStatus);
            string startDateString = UtilityHelper.FormatDateToISO8601(startDate);
            string endDateString = UtilityHelper.FormatDateToISO8601(endDate);
            string orderTypeString = orderType?.ToString();
            string orderSideString = orderSide?.ToString();

            // Create an anonymous object with the parameters
            var paramsObj = new
            {
                product_id = productId,
                order_status = orderStatusStrings,
                start_date = startDateString,
                end_date = endDateString,
                order_type = orderTypeString,
                order_side = orderSideString
            };

            try
            {
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/orders/historical/batch", UtilityHelper.ConvertToDictionary(paramsObj))
                               ?? new Dictionary<string, object>();

                return UtilityHelper.DeserializeJsonElement<List<Order>>(response, "orders");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to list orders", ex);
            }
        }

        private static void ValidateOrderStatus(OrderStatus[] orderStatus)
        {
            if (orderStatus != null && orderStatus.Contains(OrderStatus.OPEN) && orderStatus.Length > 1)
            {
                throw new ArgumentException("Cannot pair OPEN orders with other order types.");
            }
        }


        /// <inheritdoc/>
        public async Task<List<Fill>> ListFillsAsync(
            string orderId = null,
            string productId = null,
            DateTime? startSequenceTimestamp = null,
            DateTime? endSequenceTimestamp = null)
        {
            // Convert DateTime to the desired ISO8601 format
            string startSequenceTimestampString = UtilityHelper.FormatDateToISO8601(startSequenceTimestamp);
            string endSequenceTimestampString = UtilityHelper.FormatDateToISO8601(endSequenceTimestamp);

            // Prepare request parameters using anonymous type
            var paramsObj = new
            {
                order_id = orderId,
                product_id = productId,
                start_sequence_timestamp = startSequenceTimestampString,
                end_sequence_timestamp = endSequenceTimestampString
            };

            try
            {
                // Send authenticated request to the API and obtain response
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", "/api/v3/brokerage/orders/historical/fills", UtilityHelper.ConvertToDictionary(paramsObj))
                               ?? new Dictionary<string, object>();

                // Deserialize response to obtain fills
                return UtilityHelper.DeserializeJsonElement<List<Fill>>(response, "fills");
            }
            catch (Exception ex)
            {
                // Rethrow exception with additional context
                throw new InvalidOperationException("Failed to list fills", ex);
            }
        }



        /// <inheritdoc/>
        public async Task<Order> GetOrderAsync(string orderId)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentException("Order ID cannot be null, empty, or consist only of white-space characters.", nameof(orderId));
            }

            try
            {
                // Construct the API endpoint using the provided order ID
                string endpoint = $"/api/v3/brokerage/orders/historical/{orderId}";

                // Send authenticated request to the API and obtain response
                var response = await _authenticator.SendAuthenticatedRequestAsync("GET", endpoint)
                               ?? new Dictionary<string, object>();

                // Deserialize response to obtain the order details
                return UtilityHelper.DeserializeJsonElement<Order>(response, "order");
            }
            catch (Exception ex)
            {
                // Rethrow exception with additional context
                throw new InvalidOperationException("Failed to get the order", ex);
            }
        }



        /// <inheritdoc/>
        public async Task<List<CancelOrderResult>> CancelOrdersAsync(string[] orderIds)
        {
            // Validate the input parameter
            if (orderIds == null || orderIds.Length == 0)
            {
                throw new ArgumentException("Order IDs array cannot be null or empty.", nameof(orderIds));
            }

            try
            {
                // Set up the request body with the provided order IDs
                var requestBody = new { order_ids = orderIds };

                // Send authenticated request to the API to cancel the orders and obtain response
                var response = await _authenticator.SendAuthenticatedRequestAsync("POST", "/api/v3/brokerage/orders/batch_cancel", null, requestBody)
                               ?? new Dictionary<string, object>();

                // Deserialize the response to obtain the cancel order results
                return UtilityHelper.DeserializeJsonElement<List<CancelOrderResult>>(response, "results");
            }
            catch (Exception ex)
            {
                // Rethrow exception with additional context
                throw new InvalidOperationException("Failed to cancel orders", ex);
            }
        }




        /// <summary>
        /// Creates an order based on the provided configurations.
        /// </summary>
        /// <param name="productId">The ID of the product for the order.</param>
        /// <param name="side">Specifies whether to buy or sell.</param>
        /// <param name="orderConfiguration">Configuration details for the order.</param>
        /// <returns>Order ID upon successful order creation; otherwise, null.</returns>
        private async Task<string> CreateOrderAsync(string productId, OrderSide side, OrderConfiguration orderConfiguration)
        {
            // Validate the provided product ID
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Product ID cannot be null, empty, or consist only of white-space characters.", nameof(productId));
            }

            // Validate the order side
            if (side != OrderSide.BUY && side != OrderSide.SELL)
            {
                throw new ArgumentException("Invalid side value provided.", nameof(side));
            }


            // Ensure order configuration is provided
            if (orderConfiguration is null) 
            {
                throw new ArgumentNullException(nameof(orderConfiguration), "Order configuration cannot be null.");
            }

            try
            {
                // Generate a unique client order ID
                var clientOrderId = Guid.NewGuid().ToString();

                // Construct the order request payload
                var orderRequest = new
                {
                    client_order_id = clientOrderId,
                    product_id = productId,
                    side = side.ToString(),
                    order_configuration = orderConfiguration
                };

                // Send a POST request to create the order
                var response = await _authenticator.SendAuthenticatedRequestAsync("POST", "/api/v3/brokerage/orders", null, orderRequest) ?? new Dictionary<string, object>();

                // Check if we have a 'success_response' in the received response
                if (response.TryGetValue("success_response", out var successResponse))
                {
                    var successResponseStr = successResponse?.ToString();
                    if (!string.IsNullOrEmpty(successResponseStr))
                    {
                        var successResponseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(successResponseStr);

                        // If 'order_id' is present in the success response, return it
                        if (successResponseDict?.TryGetValue("order_id", out var orderId) == true)
                        {
                            return orderId;
                        }
                    }
                }

                // If there's an 'error_response', handle it
                else if (response.ContainsKey("error_response"))
                {
                    // Extract the error message from the response
                    var errorResponseObj = response["error_response"];

                    // Assuming errorResponseObj is a JsonElement
                    if (errorResponseObj is JObject errorResponseObject) // Check if the errorResponseObj is a JSON object
                    {
                        var errorResponseValue = errorResponseObject.ToString(); // Get the raw JSON text of the object

                        if (!string.IsNullOrEmpty(errorResponseValue))
                        {
                            // Deserialize the error response from the raw text
                            var errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResponseValue);

                            if (errorResponse != null)
                            {
                                // Construct an error message using the provided details
                                var error = errorResponse.ContainsKey("error") ? errorResponse["error"] : "Unknown Error";
                                var message = errorResponse.ContainsKey("message") ? errorResponse["message"] : "No Message";
                                var errorDetails = errorResponse.ContainsKey("error_details") ? errorResponse["error_details"] : "No Details";

                                throw new Exception($"Order creation failed. Error: {error}. Message: {message}. Details: {errorDetails}");
                            }
                        }
                    }

                    // If error response is not in the expected format or is empty, return null (or handle as needed)
                    return null;
                }


                // If we reach here, the order creation was not successful
                return null;
            }
            catch (Exception ex)
            {
                // If an exception occurred, wrap it with additional information and rethrow
                throw new InvalidOperationException($"Failed to create order: {ex.Message}", ex);
            }
        }




        /// <inheritdoc/>
        public async Task<string> CreateMarketOrderAsync(string productId, OrderSide side, string amount)
        {
            // Ensure the product ID is provided and not empty
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            // Determine the market order details based on the side (BUY or SELL)
            MarketIoc marketDetails;
            switch (side)
            {
                case OrderSide.BUY:
                    marketDetails = new MarketIoc { QuoteSize = amount }; // Buy orders use the QuoteSize
                    break;
                case OrderSide.SELL:
                    marketDetails = new MarketIoc { BaseSize = amount };  // Sell orders use the BaseSize
                    break;
                default:
                    throw new ArgumentException($"Invalid order side provided: {side}.");
            }


            // Create the order configuration using the determined market details
            var orderConfiguration = new OrderConfiguration
            {
                MarketIoc = marketDetails
            };

            // Call the underlying order creation method with the prepared configuration
            return await CreateOrderAsync(productId, side, orderConfiguration);
        }


        /// <inheritdoc/>
        public async Task<string> CreateLimitOrderGTCAsync(string productId, OrderSide side, string baseSize, string limitPrice, bool postOnly)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (string.IsNullOrEmpty(baseSize))
            {
                throw new ArgumentException("Base size cannot be null or empty.", nameof(baseSize));
            }

            if (string.IsNullOrEmpty(limitPrice))
            {
                throw new ArgumentException("Limit price cannot be null or empty.", nameof(limitPrice));
            }

            // Prepare the order configuration for a Limit Order with GTC (Good Till Cancel)
            // This defines the parameters of the limit order, such as the amount (baseSize),
            // the price at which it's willing to trade (limitPrice), and whether it should only
            // post this order to the order book (postOnly).
            var orderConfiguration = new OrderConfiguration
            {
                LimitGtc = new LimitGtc
                {
                    BaseSize = baseSize,
                    LimitPrice = limitPrice,
                    PostOnly = postOnly
                }
            };

            // Delegate the actual order creation to a more general-purpose method, 
            // passing in the prepared configuration.
            return await CreateOrderAsync(productId, side, orderConfiguration);
        }



        /// <inheritdoc/>
        public async Task<string> CreateLimitOrderGTDAsync(string productId, OrderSide side, string baseSize, string limitPrice, DateTime endTime, bool postOnly = true)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (string.IsNullOrEmpty(baseSize))
            {
                throw new ArgumentException("Base size cannot be null or empty.", nameof(baseSize));
            }

            if (string.IsNullOrEmpty(limitPrice))
            {
                throw new ArgumentException("Limit price cannot be null or empty.", nameof(limitPrice));
            }

            if (endTime <= DateTime.UtcNow)
            {
                throw new ArgumentException("End time should be in the future.", nameof(endTime));
            }

            // Construct the order configuration for a Limit Order with GTD (Good Till Date)
            // This sets the parameters like the size of the order (baseSize), the desired trade price (limitPrice),
            // the time until the order remains active (endTime), and if the order should only be posted to the order book (postOnly).
            var orderConfig = new OrderConfiguration
            {
                LimitGtd = new LimitGtd
                {
                    BaseSize = baseSize,
                    LimitPrice = limitPrice,
                    EndTime = endTime,
                    PostOnly = postOnly
                }
            };

            // Delegate the actual order creation to the general-purpose method with the constructed configuration
            return await CreateOrderAsync(productId, side, orderConfig);
        }



        /// <inheritdoc/>
        public async Task<string> CreateStopLimitOrderGTCAsync(string productId, OrderSide side, string baseSize, string limitPrice, string stopPrice)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (string.IsNullOrEmpty(baseSize))
            {
                throw new ArgumentException("Base size cannot be null or empty.", nameof(baseSize));
            }

            if (string.IsNullOrEmpty(limitPrice))
            {
                throw new ArgumentException("Limit price cannot be null or empty.", nameof(limitPrice));
            }

            if (string.IsNullOrEmpty(stopPrice))
            {
                throw new ArgumentException("Stop price cannot be null or empty.", nameof(stopPrice));
            }

            // Determine stop direction based on the side of the order (BUY or SELL)
            string stopDirection;
            switch (side)
            {
                case OrderSide.BUY:
                    stopDirection = "STOP_DIRECTION_STOP_UP";
                    break;
                case OrderSide.SELL:
                    stopDirection = "STOP_DIRECTION_STOP_DOWN";
                    break;
                default:
                    throw new ArgumentException($"Invalid order side provided: {side}.");
            }


            // Construct the order configuration for a Stop Limit Order with GTC (Good Till Cancel)
            var orderConfig = new OrderConfiguration
            {
                StopLimitGtc = new StopLimitGtc
                {
                    BaseSize = baseSize,
                    LimitPrice = limitPrice,
                    StopPrice = stopPrice,
                    StopDirection = stopDirection
                }
            };

            // Delegate the actual order creation to the general-purpose method with the constructed configuration
            return await CreateOrderAsync(productId, side, orderConfig);
        }



        /// <inheritdoc/>
        public async Task<string> CreateStopLimitOrderGTDAsync(string productId, OrderSide side, string baseSize, string limitPrice, string stopPrice, DateTime endTime)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (string.IsNullOrEmpty(baseSize))
            {
                throw new ArgumentException("Base size cannot be null or empty.", nameof(baseSize));
            }

            if (string.IsNullOrEmpty(limitPrice))
            {
                throw new ArgumentException("Limit price cannot be null or empty.", nameof(limitPrice));
            }

            if (string.IsNullOrEmpty(stopPrice))
            {
                throw new ArgumentException("Stop price cannot be null or empty.", nameof(stopPrice));
            }

            if (endTime <= DateTime.UtcNow)
            {
                throw new ArgumentException("End time should be in the future.", nameof(endTime));
            }

            // Determine stop direction based on the side of the order (BUY or SELL)
            string stopDirection;
            switch (side)
            {
                case OrderSide.BUY:
                    stopDirection = "STOP_DIRECTION_STOP_UP";
                    break;
                case OrderSide.SELL:
                    stopDirection = "STOP_DIRECTION_STOP_DOWN";
                    break;
                default:
                    throw new ArgumentException($"Invalid order side provided: {side}.");
            }


            // Construct the order configuration for a Stop Limit Order with GTD (Good Till Date)
            var orderConfig = new OrderConfiguration
            {
                StopLimitGtd = new StopLimitGtd
                {
                    BaseSize = baseSize,
                    LimitPrice = limitPrice,
                    StopPrice = stopPrice,
                    StopDirection = stopDirection,
                    EndTime = endTime
                }
            };

            // Delegate the actual order creation to the general-purpose method with the constructed configuration
            return await CreateOrderAsync(productId, side, orderConfig);
        }



        /// <inheritdoc/>
        public async Task<string> CreateSORLimitIOCOrderAsync(string productId, OrderSide side, string baseSize, string limitPrice)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (string.IsNullOrEmpty(baseSize))
            {
                throw new ArgumentException("Base size cannot be null or empty.", nameof(baseSize));
            }

            if (string.IsNullOrEmpty(limitPrice))
            {
                throw new ArgumentException("Limit price cannot be null or empty.", nameof(limitPrice));
            }

            // Prepare the order configuration for a SOR Limit IOC order
            var orderConfiguration = new OrderConfiguration
            {
                SorLimitIoc = new SorLimitIoc
                {
                    BaseSize = baseSize,
                    LimitPrice = limitPrice
                }
            };

            // Delegate the actual order creation to the general-purpose method with the prepared configuration
            return await CreateOrderAsync(productId, side, orderConfiguration);
        }



        /// <inheritdoc/>
        public async Task<bool> EditOrderAsync(string orderId, string price = null, string size = null)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            if (string.IsNullOrEmpty(price))
            {
                throw new ArgumentException("Price cannot be null or empty.", nameof(price));
            }

            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentException("Size cannot be null or empty.", nameof(size));
            }

            var requestBody = new
            {
                order_id = orderId,
                price,
                size
            };

            try
            {
                var response = await _authenticator.SendAuthenticatedRequestAsync("POST", "/api/v3/brokerage/orders/edit", null, requestBody) ?? new Dictionary<string, object>();
                var responseObject = UtilityHelper.DeserializeDictionary<Dictionary<string, JToken>>(response);

                if (responseObject != null && responseObject.TryGetValue("success", out var successValue) && successValue.ToObject<bool>())
                {
                    return true; // Operation was successful
                }

                // Start constructing the error message
                var errorMessage = "Failed to edit order.";

                if (responseObject?.TryGetValue("errors", out var errorsValue) == true && errorsValue is JArray errorsArray && errorsArray.Any())
                {
                    var errorDetails = errorsArray.FirstOrDefault();
                    if (errorDetails != null && errorDetails["edit_failure_reason"] != null)
                    {
                        errorMessage += $" Reason: {errorDetails["edit_failure_reason"]}";
                    }
                }

                throw new InvalidOperationException(errorMessage);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Failed to edit order due to an exception.", ex);
            }
        }


        /// <inheritdoc/>
        public async Task<EditOrderPreviewResult> EditOrderPreviewAsync(string orderId, string price, string size)
        {
            // Validation of input parameters
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));
            }

            if (string.IsNullOrEmpty(price))
            {
                throw new ArgumentException("Price cannot be null or empty.", nameof(price));
            }

            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentException("Size cannot be null or empty.", nameof(size));
            }

            var requestBody = new
            {
                order_id = orderId,
                price,
                size
            };

            try
            {
                var response = await _authenticator.SendAuthenticatedRequestAsync("POST", "/api/v3/brokerage/orders/edit_preview", null, requestBody) ?? new Dictionary<string, object>();
                var responseObject = UtilityHelper.DeserializeDictionary<Dictionary<string, JToken>>(response);

                // Check if there are errors
                if (responseObject != null && responseObject.TryGetValue("errors", out var errorsValue) && errorsValue is JArray errorsArray && errorsArray.Any())
                {
                    // Convert errorsValue to a JArray and check if it's not empty
                    var errorsList = errorsArray.ToList();

                    if (errorsList.Any())
                    {
                        var errorMessage = "Failed to preview order edit.";

                        // Iterate through error objects
                        foreach (var errorObj in errorsList)
                        {
                            if (errorObj is JObject errorObject)
                            {
                                // Check for specific properties within each error object
                                if (errorObject.TryGetValue("edit_failure_reason", out var editFailureReason))
                                {
                                    errorMessage += $" Edit Failure Reason: {editFailureReason.ToString()}.";
                                }

                                if (errorObject.TryGetValue("preview_failure_reason", out var previewFailureReason))
                                {
                                    errorMessage += $" Preview Failure Reason: {previewFailureReason.ToString()}.";
                                }
                            }
                        }

                        throw new InvalidOperationException(errorMessage);
                    }
                }

                // Assuming no errors or empty error array, populate the EditOrderPreviewResult from responseObject
                var result = new EditOrderPreviewResult
                {
                    Slippage = responseObject.TryGetValue("slippage", out var tempValue) ? tempValue.ToString() : string.Empty,
                    OrderTotal = responseObject.TryGetValue("order_total", out tempValue) ? tempValue.ToString() : string.Empty,
                    CommissionTotal = responseObject.TryGetValue("commission_total", out tempValue) ? tempValue.ToString() : string.Empty,
                    QuoteSize = responseObject.TryGetValue("quote_size", out tempValue) ? tempValue.ToString() : string.Empty,
                    BaseSize = responseObject.TryGetValue("base_size", out tempValue) ? tempValue.ToString() : string.Empty,
                    BestBid = responseObject.TryGetValue("best_bid", out tempValue) ? tempValue.ToString() : string.Empty,
                    BestAsk = responseObject.TryGetValue("best_ask", out tempValue) ? tempValue.ToString() : string.Empty,
                    AverageFilledPrice = responseObject.TryGetValue("average_filled_price", out tempValue) ? tempValue.ToString() : string.Empty
                };


                return result;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Failed to preview order edit due to an exception.", ex);
            }
        }



    }
}
