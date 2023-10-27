#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Provides base functionalities for interacting with Coinbase API.
    /// </summary>
    public abstract class BaseManager
    {
        // Authenticator instance for Coinbase API authentication.
        protected readonly CoinbaseAuthenticator _authenticator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseManager"/> class.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if authenticator is null.</exception>
        protected BaseManager(CoinbaseAuthenticator authenticator)
        {
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        }

        /// <summary>
        /// Deserializes a JsonElement from a given dictionary based on a specified key.
        /// </summary>
        /// <param name="response">The dictionary containing the response.</param>
        /// <param name="key">The key to retrieve the JsonElement.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown if deserialization fails.</exception>
        protected static T? DeserializeJsonElement<T>(Dictionary<string, object> response, string key)
        {
            try
            {
                if (response.TryGetValue(key, out object? valueObj) && valueObj is JsonElement element)
                {
                    return JsonSerializer.Deserialize<T>(element.GetRawText());
                }
                return default;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize JSON element", ex);
            }
        }

        /// <summary>
        /// Extracts a double value from a dictionary based on a specified key.
        /// </summary>
        /// <param name="response">The dictionary containing the response.</param>
        /// <param name="key">The key to retrieve the double value.</param>
        /// <returns>The extracted double value, or null if extraction fails.</returns>
        /// <exception cref="InvalidOperationException">Thrown if extraction fails.</exception>
        protected static double? ExtractDoubleValue(Dictionary<string, object> response, string key)
        {
            try
            {
                if (response.TryGetValue(key, out object? valueObj) && valueObj is JsonElement element && element.ValueKind == JsonValueKind.Number)
                {
                    return element.GetDouble();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to extract double value", ex);
            }
        }

        /// <summary>
        /// Converts an object's properties to a dictionary of string keys and values.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A dictionary representation of the object's properties.</returns>
        protected static Dictionary<string, string> ConvertToDictionary(object obj)
        {
            return obj.GetType().GetProperties()
                .Where(prop => prop.GetValue(obj) is { } value)
                .ToDictionary(
                    prop => prop.Name,
                    prop => prop.GetValue(obj) switch
                    {
                        Array array => string.Join(',', array.Cast<object>()),
                        IList nonGenericList => string.Join(',', nonGenericList.Cast<object>()),
                        var other => other?.ToString() ?? string.Empty
                    }
                );
        }

        /// <summary>
        /// Converts an array of enums to an array of strings.
        /// </summary>
        /// <param name="enums">The array of enums.</param>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <returns>An array of strings.</returns>
        protected static string[]? EnumToStringArray<TEnum>(TEnum[]? enums) where TEnum : struct
        {
            return enums?.Select(e => e.ToString()!).ToArray();
        }

        /// <summary>
        /// Formats a DateTime instance to the ISO 8601 format.
        /// </summary>
        /// <param name="dateTime">The DateTime instance to format.</param>
        /// <returns>The ISO 8601 formatted string.</returns>
        protected static string? FormatDateToISO8601(DateTime? dateTime)
        {
            return dateTime?.ToUniversalTime().ToString("o");
        }
    }
}
