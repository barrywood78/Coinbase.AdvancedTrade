# Coinbase API Wrapper for Advanced Trade

This project provides a C# wrapper for the [Coinbase Advanced Trade API](https://docs.cloud.coinbase.com/advanced-trade-api), facilitating interactions with various advanced trading functionalities on Coinbase. It supports both the traditional legacy API keys and the new Cloud API Trading Keys that utilize JWT for authentication, offering flexibility in integrating with different API key types.

## Overview
The wrapper is organized into various namespaces, each serving a distinct purpose, covering the spectrum from data models to actual API interactions.

## Namespaces

### `Coinbase.AdvancedTrade`
The root namespace and foundation for the entire API wrapper.

### `Coinbase.AdvancedTrade.ExchangeManagers`
Central hub for managers responsible for orchestrating different types of operations with the Coinbase Advanced Trade API.

### `Coinbase.AdvancedTrade.Interfaces`
Blueprints that outline the structure and contract for the managers, ensuring a cohesive and standardized approach to API interactions.

### `Coinbase.AdvancedTrade.Models`
Collection of models capturing the essence of various entities and data structures pivotal for a seamless interaction with the Coinbase API.

## Classes

### `CoinbaseClient`
Your gateway to the vast functionalities of the Coinbase API, compatible with both legacy API keys and new Cloud API Trading Keys.
- **Constructor**: `public CoinbaseClient(string apiKey, string apiSecret, ApiKeyType apiKeyType = ApiKeyType.Legacy)`
  - Initializes managers for accounts, products, orders, fees, and WebSocket connections using the provided API credentials. The `apiKeyType` parameter determines the type of API key to use, with options being `Legacy` (default) or `CloudTrading` for the new JWT-based keys.

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
  - `ListOrdersAsync`, `CreateMarketOrderAsync`, and more.

### `WebSocketManager`
Specialized in managing WebSocket communications for real-time updates from the Coinbase Advanced Trade API.
- **Methods**:
  - `ConnectAsync`, `SubscribeAsync`, and more.
- **Events**:
  - Alerts for diverse message types, from `CandleMessageReceived` to `UserMessageReceived`.

## Enums
- Fine-tuned enumerations like `OrderStatus`, `OrderType`, and others, ensuring clarity and type precision in operations.
- `ApiKeyType`: Enum with values `Legacy` and `CloudTrading`, representing the two types of API keys supported by the wrapper.


## Additional Information
- **Models**: Detailed blueprints for entities such as `Account`, `Product`, `Order`, and `Candle`. These lay the groundwork for robust data representation when communicating with the Coinbase API.

## Testing

The `Coinbase API Wrapper` is accompanied by a suite of tests, ensuring its reliability and functionality.

- **Test Coverage**: Tests cover key functionalities including (but not limited to) authentication, order management, and WebSocket communication.
