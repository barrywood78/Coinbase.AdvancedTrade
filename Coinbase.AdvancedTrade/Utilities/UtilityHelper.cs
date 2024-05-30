using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Coinbase.AdvancedTrade.Utilities
{
    /// <summary>
    /// Provides various utility functions for data manipulation and conversion.
    /// </summary>
    public static class UtilityHelper
    {
        /// <summary>
        /// Deserializes an object from a given dictionary based on a specified key.
        /// </summary>
        /// <param name="response">The dictionary containing the response.</param>
        /// <param name="key">The key to retrieve the object.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown if deserialization fails.</exception>
        public static T DeserializeJsonElement<T>(Dictionary<string, object> response, string key)
        {
            try
            {
                if (response.TryGetValue(key, out object valueObj) && valueObj != null)
                {
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
        public static T DeserializeDictionary<T>(Dictionary<string, object> response)
        {
            try
            {
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
        public static double? ExtractDoubleValue(Dictionary<string, object> response, string key)
        {
            try
            {
                if (response.TryGetValue(key, out object valueObj))
                {
                    if (valueObj is double doubleValue)
                    {
                        return doubleValue;
                    }
                    else if (valueObj is string stringValue && double.TryParse(stringValue, out doubleValue))
                    {
                        return doubleValue;
                    }
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
        public static Dictionary<string, string> ConvertToDictionary(object obj)
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
        public static string[] EnumToStringArray<TEnum>(TEnum[] enums) where TEnum : struct
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
        public static string FormatDateToISO8601(DateTime? dateTime)
        {
            return dateTime?.ToUniversalTime().ToString("o");
        }
    }
}
