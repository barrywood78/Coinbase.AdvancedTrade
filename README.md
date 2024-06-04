# Coinbase API Wrapper for Advanced Trade

This project provides a C# wrapper for the [Coinbase Advanced Trade API](https://docs.cloud.coinbase.com/advanced-trade-api), facilitating interactions with various advanced trading functionalities on Coinbase. 

The wrapper supports both Coinbase Developer Platform (CDP) Keys and Legacy keys. The new Coinbase Developer Platform (CDP) Keys utilize JWT for authentication. The legacy API keys option has been restored but is deprecated and will be removed in a future release following Coinbase's update and the removal of the ability to create and edit legacy API keys effective June 10, 2024. However, Coinbase will continue to allow existing legacy keys to work for some time. For more details on Coinbase Developer Platform (CDP) Keys, see [Coinbase's documentation](https://docs.cloud.coinbase.com/advanced-trade-api/docs/rest-api-auth#cloud-api-trading-keys).

For Coinbase Developer Platform (CDP) Keys, the `key_name` and `key_secret` are expected to follow this structure as per Coinbase:

- key: `"organizations/{org_id}/apiKeys/{key_id}"`
- secret: `"-----BEGIN EC PRIVATE KEY-----\nYOUR PRIVATE KEY\n-----END EC PRIVATE KEY-----\n"`

**[View the Changelog](#changelog)** - See the detailed log of changes including updates, enhancements, and important notes.

## Overview
The wrapper is organized into various namespaces, each serving a distinct purpose, covering the spectrum from data models to actual API interactions.

## Namespaces

### `Coinbase.AdvancedTrade`
The root namespace and foundation for the entire API wrapper.

### `Coinbase.AdvancedTrade.ExchangeManagers`
Central hub for managers responsible for orchestrating different types of operations with the Coinbase Advanced Trade API, including WebSocket connections.

### `Coinbase.AdvancedTrade.Interfaces`
Blueprints that outline the structure and contract for the managers, ensuring a cohesive and standardized approach to API interactions.

### `Coinbase.AdvancedTrade.Models`
Collection of models capturing the essence of various entities and data structures pivotal for a seamless interaction with the Coinbase API.

## Classes

### `CoinbaseClient`
Your gateway to the vast functionalities of the Coinbase API, supporting the new Coinbase Developer Platform (CDP) Keys.
- **Constructor**: Initializes managers for accounts, products, orders, fees, and WebSocket connections using the provided API credentials. It also includes an optional parameter to specify the WebSocket buffer size, with a default value of 5 MB.

### `CoinbasePublicClient`
Access public API endpoints without the need for authentication.
- **Methods**:
  - `ListPublicProductsAsync`, `GetPublicProductAsync`, `GetPublicProductBookAsync`, `GetPublicMarketTradesAsync`, `GetPublicProductCandlesAsync`, and more.

### `CoinbaseAuthenticator`
A sentinel that ensures every API request is authenticated.
- **Methods**:
  - `SendAuthenticatedRequest`: Directs an authenticated request to the destined path.
  - `SendAuthenticatedRequestAsync`: Asynchronously channels an authenticated request to the designated path.

## Interfaces

### `IAccountsManager`
- **Methods**:
  - `ListAccountsAsync`: Unveils a list of accounts.
  - `GetAccountAsync`: Zeros in on a specific account using its UUID.

### `IFeesManager`
- **Methods**:
  - `GetTransactionsSummaryAsync`: Condenses transactions within a chosen date range into a summary.

### `IProductsManager`
Entrusted with product-centric functionalities.
- **Methods**:
  - `ListProductsAsync`, `GetProductCandlesAsync`, and more.

### `IOrdersManager`
A maestro orchestrating order-related functionalities.
- **Methods**:
  - `ListOrdersAsync`, `CreateMarketOrderAsync`, `CreateSORLimitIOCOrderAsync`, and more.

### `IPublicManager`
Interface for managing public endpoints of Coinbase Advanced Trade API.
- **Methods**:
  - `GetCoinbaseServerTimeAsync`: Asynchronously retrieves the current server time from Coinbase.
  - `ListPublicProductsAsync`: Asynchronously retrieves a list of public products from Coinbase.
  - `GetPublicProductAsync`: Asynchronously retrieves details for a specific public product by product ID.
  - `GetPublicProductBookAsync`: Asynchronously retrieves the order book for a specific public product.
  - `GetPublicMarketTradesAsync`: Asynchronously retrieves market trades for a specific public product.
  - `GetPublicProductCandlesAsync`: Asynchronously retrieves candlestick data for a specific public product.

### `WebSocketManager`
Specialized in managing WebSocket communications for real-time updates from the Coinbase Advanced Trade API.
- **Methods**:
  - `ConnectAsync`, `SubscribeAsync`, and more.
- **Events**:
  - Alerts for diverse message types, from `CandleMessageReceived` to `UserMessageReceived`.
- **Properties**:
  - `WebSocketState`: Get the current connection state.
  - `Subscriptions`: Get the list of active subscriptions.

## Enums
Fine-tuned enumerations like `OrderStatus`, `OrderType`, and others, ensuring clarity and type precision in operations.
- `ApiKeyType`: Enum with values `Legacy` and `CloudTrading`, representing the two types of API keys supported by the wrapper.

## Additional Information
- **Models**: Detailed blueprints for entities such as `Account`, `Product`, `Order`, and `Candle`. These lay the groundwork for robust data representation when communicating with the Coinbase API.

## Testing
The `Coinbase API Wrapper` is accompanied by a suite of tests, ensuring its reliability and functionality.
- **Test Coverage**: Tests cover key functionalities including (but not limited to) authentication, order management, and WebSocket communication.

# Changelog

## v1.3.1 - 2024-JUN-04
- **Reverted CoinbaseClient Constructor to Include Legacy API Key Type**: The `CoinbaseClient` constructor again includes the `ApiKeyType` of `Legacy`, but `CoinbaseDeveloperPlatform` is now the default. **This may be a breaking change and can be fixed by supplying the constuctor with an ApiKeyType = Legacy**.


## v1.3 - 2024-MAY-30
- **Removed Legacy API Keys**: The wrapper now only supports Coinbase Developer Platform (CDP) API keys due to Coinbase removing legacy keys after an extended timeline, effective June 10, 2024. **This is a breaking change**
- **Updated NuGet Packages**: Addressed vulnerabilities by updating various NuGet packages.
- **WebSocket Buffer Option**: Added a new optional parameter in the client constructor to specify buffer size, with a default of 5MB.
- **Renamed CommonManager to PublicManager**: The `CommonManager` has been renamed to `PublicManager`. **This is a breaking change**
- **Limit IOC Orders**: Added support for Limit IOC orders within the new `CreateSORLimitIOCOrderAsync` function in `OrdersManager`.
- **New CoinbasePublicClient**: Added to access public API endpoints without the need for authentication. Public API endpoints are still also available through authenticated `CoinbaseClient`.
- **Public Market Data Endpoints**: Added support for new public market data endpoints released by Coinbase on 2024-APR-09, including methods to list public products, get product details, get order books, market trades, and candle data.
- **WebSocket Enhancements**: Added `WebSocketState` and `Subscriptions` properties to `WebSocketManager` for retrieving the current connection state and active subscriptions.
- **WebSocket Reconnection Demo**: Added `WebSocketReconnectionTest` application to Github Project to demonstrate reconnection logic using `WebSocketState` from `WebSocketManager`.

## v1.2 - 2024-JAN-17
- **Added Support for Coinbase Developer Platform (CDP) Keys**: The wrapper now supports the new Coinbase Developer Platform (CDP) Keys that utilize JWT for authentication. This enhances the flexibility in API key usage and provides an updated authentication mechanism.
- **Updated `CoinbaseClient` Constructor**: The `CoinbaseClient` constructor has been updated to include the `apiKeyType` parameter, allowing users to specify the type of API key (`Legacy` or `CloudTrading`).
- **IntelliSense**: Added comprehensive IntelliSense to methods and parameters.

## v1.1 - 2024-JAN-08
- **Changed Target Frameworks**: Changed project's target frameworks to .NET Standard 2.0, .NET 8, and .NET Framework 4.8.

## 2024-JAN-07 Update
- **Fixed Stop Loss Order Creation Issue**: `CoinbaseClient.Orders.CreateStopLimitOrderGTCAsync()` and `CoinbaseClient.Orders.CreateStopLimitOrderGTDAsync()` were not creating Stop Loss orders correctly. This has been fixed.
- **Order Creation Exception Handling**: Better exception handling for order creation. An exception is thrown with Coinbase response details instead of just returning null.

## 2024-JAN-02 Update

### Added
- **Get UNIX Time** (Coinbase: 2023-DEC-06): Implemented feature (`GetCoinbaseServerTimeAsync`) to retrieve the current UNIX time from the Coinbase Advanced Trading API.
- **Edit Order and Edit Order Preview** (Coinbase: 2023-NOV-13): Users can now edit and preview edits to orders, limited to certain conditions (`IOrdersManager`).

### Notes on Non-Implemented Features
- **Portfolios Feature (Coinbase: 2023-DEC-11)**: Currently in beta by Coinbase and not implemented in this wrapper. Intended for future updates.
- **Converts Feature (Coinbase: 2023-NOV-27)**: Not implemented due to regional restrictions affecting testing capabilities.
