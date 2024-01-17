# Coinbase API Wrapper for Advanced Trade

This project provides a C# wrapper for the [Coinbase Advanced Trade API](https://docs.cloud.coinbase.com/advanced-trade-api), facilitating interactions with various advanced trading functionalities on Coinbase. 

It now supports both legacy API keys and the new Cloud API Trading Keys, which utilize JWT for authentication. For more details on Cloud API Trading Keys, see [Coinbase's documentation](https://docs.cloud.coinbase.com/advanced-trade-api/docs/rest-api-auth#cloud-api-trading-keys).

For Cloud API Trading Keys, the `key_name` and `key_secret` are expected to follow this structure as per Coinbase:

- key: `"organizations/{org_id}/apiKeys/{key_id}"`
- secret: `"-----BEGIN EC PRIVATE KEY-----\nYOUR PRIVATE KEY\n-----END EC PRIVATE KEY-----\n"`



**[View the Changelog](#changelog)** - See the detailed log of changes including updates, enhancements, and important notes.


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
Your gateway to the vast functionalities of the Coinbase API, supporting both legacy API keys and new Cloud API Trading Keys.
- **Constructor**: `public CoinbaseClient(string apiKey, string apiSecret, ApiKeyType apiKeyType = ApiKeyType.Legacy)`
  - Initializes managers for accounts, products, orders, fees, and WebSocket connections using the provided API credentials. The `apiKeyType` parameter allows for the selection of either `Legacy` or `CloudTrading` keys.

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
Fine-tuned enumerations like `OrderStatus`, `OrderType`, and others, ensuring clarity and type precision in operations.

## Additional Information
- **Models**: Detailed blueprints for entities such as `Account`, `Product`, `Order`, and `Candle`. These lay the groundwork for robust data representation when communicating with the Coinbase API.

# Changelog

## v1.2 - 2024-JAN-17
- **Added Support for Cloud API Trading Keys**: The wrapper now supports the new Cloud API Trading Keys that utilize JWT for authentication. This enhances the flexibility in API key usage and provides an updated authentication mechanism.
- **Updated `CoinbaseClient` Constructor**: The `CoinbaseClient` constructor has been updated to include the `apiKeyType` parameter, allowing users to specify the type of API key (`Legacy` or `CloudTrading`).
- **IntelliSense**: Added comprehensive IntelliSense to methods and parameters.

## v1.1 - 2024-JAN-08
- **Changed Target Frameworks**: Changed project's target frameworks to .NET Standard 2.0, .NET 8, and .NET Framework 4.8

## 2024-JAN-07 Update
- **Fixed Stop Loss Order Creation Issue**: CoinbaseClient.Orders.CreateStopLimitOrderGTCAsync( ) and CoinbaseClient.Orders.CreateStopLimitOrderGTDAsync( ) were not creating Stop Loss orders correctly. This has been fixed.
- **Order Creation Exception Handling**:   Better exception handling for Order Creation. An exception is thrown with Coinbase response details instead of just returning null.


## 2024-JAN-02 Update

### Added
- **Get UNIX Time** (Coinbase: 2023-DEC-06): Implemented feature (GetCoinbaseServerTimeAsync) to retrieve the current UNIX time from the Coinbase Advanced Trading API.
- **Edit Order and Edit Order Preview** (Coinbase: 2023-NOV-13): Users can now edit and preview edits to orders, limited to certain conditions (IOrdersManager).

### Notes on Non-Implemented Features
- **Portfolios Feature (Coinbase: 2023-DEC-11)**: Currently in beta by Coinbase and not implemented in this wrapper. Intended for future updates.
- **Converts Feature (Coinbase: 2023-NOV-27)**: Not implemented due to regional restrictions affecting testing capabilities.
