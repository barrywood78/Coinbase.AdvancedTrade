using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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
        /// Deserializes an object from a given dictionary based on a specified key.
        /// </summary>
        /// <param name="response">The dictionary containing the response.</param>
        /// <param name="key">The key to retrieve the object.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown if deserialization fails.</exception>
        protected static T DeserializeJsonElement<T>(Dictionary<string, object> response, string key)
        {
            try
            {
                if (response.TryGetValue(key, out object valueObj) && valueObj != null)
                {
                    // Assuming the object is already a string representation of JSON
                    // Alternatively, you might need to convert it to a string depending on what the object actually is
                    string json = valueObj.ToString();
                    return JsonConvert.DeserializeObject<T>(json);
                }
                return default(T);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize JSON element", ex);
            }
        }



        /// <summary>
        /// Deserializes a dictionary into an object of specified type.
        /// </summary>
        /// <param name="response">The dictionary representing the JSON object structure.</param>
        /// <typeparam name="T">The type of object to deserialize into.</typeparam>
        /// <returns>The deserialized object of type T, or default if deserialization fails.</returns>
        /// <exception cref="InvalidOperationException">Thrown if deserialization fails due to invalid operation.</exception>
        /// <remarks>
        /// This method converts the provided dictionary into a JSON string and then attempts to deserialize it into the specified type.
        /// It's useful for reconstructing objects when the response is already parsed into a dictionary form.
        /// If the dictionary does not represent a JSON structure compatible with type T, deserialization will fail.
        /// </remarks>
        protected static T DeserializeDictionary<T>(Dictionary<string, object> response)
        {
            try
            {
                // Convert the response dictionary back to JSON string and then deserialize it.
                string jsonString = JsonConvert.SerializeObject(response);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize dictionary", ex);
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
                if (response.TryGetValue(key, out object valueObj))
                {
                    if (valueObj is double doubleValue)
                    {
                        // Directly cast if it's already a double
                        return doubleValue;
                    }
                    else if (valueObj is string stringValue && double.TryParse(stringValue, out doubleValue))
                    {
                        // Try to parse if it's a string representation of a number
                        return doubleValue;
                    }
                    // Add additional conversion logic here if necessary
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
                .Where(prop => prop.GetValue(obj) != null) // Simplified null check
                .ToDictionary(
                    prop => prop.Name,
                    prop =>
                    {
                        var value = prop.GetValue(obj);
                        if (value is Array array) // Handle arrays
                        {
                            return string.Join(",", array.Cast<object>());
                        }
                        else if (value is IList && !(value is string)) // Handle non-generic lists
                        {
                            return string.Join(",", ((IList)value).Cast<object>());
                        }
                        else // Handle other types
                        {
                            return value?.ToString() ?? string.Empty;
                        }
                    }
                );
        }


        /// <summary>
        /// Converts an array of enums to an array of strings.
        /// </summary>
        /// <param name="enums">The array of enums.</param>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <returns>An array of strings.</returns>
        protected static string[] EnumToStringArray<TEnum>(TEnum[] enums) where TEnum : struct
        {
            if (enums == null)
                return null;

            return enums.Select(e => e.ToString()).ToArray();
        }


        /// <summary>
        /// Formats a DateTime instance to the ISO 8601 format.
        /// </summary>
        /// <param name="dateTime">The DateTime instance to format.</param>
        /// <returns>The ISO 8601 formatted string.</returns>
        protected static string FormatDateToISO8601(DateTime? dateTime)
        {
            // The 'o' or round-trip format specifier represents the time in ISO 8601 format
            return dateTime?.ToUniversalTime().ToString("o");
        }
    }
}
