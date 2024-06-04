using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Coinbase.AdvancedTrade.Enums;
using Newtonsoft.Json;
using RestSharp;

namespace Coinbase.AdvancedTrade
{
    /// <summary>
    /// Represents an authenticator for Coinbase API requests.
    /// This class is responsible for generating appropriate JWT headers and ensuring authenticated communication with the Coinbase API.
    /// </summary>
    public sealed class CoinbaseAuthenticator
    {
        private readonly RestClient _client;

        private ApiKeyType apiKeyType = ApiKeyType.CoinbaseDeveloperPlatform; // Default to CoinbaseDeveloperPlatform (CDP)

        /// <summary>
        /// Gets the API key used for Coinbase authentication.
        /// </summary>
        private string Key { get; }

        /// <summary>
        /// Gets the API secret used for Coinbase authentication.
        /// </summary>
        private string Secret { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinbaseAuthenticator"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for Coinbase authentication.</param>
        /// <param name="apiSecret">The API secret for Coinbase authentication.</param>
        /// <param name="apiKeyType">The type of API key, CoinbaseDeveloperPlatform or Legacy(depricated).</param>
        /// <exception cref="ArgumentNullException">Thrown when either <paramref name="apiKey"/> or <paramref name="apiSecret"/> is null.</exception>
        public CoinbaseAuthenticator(string apiKey, string apiSecret, ApiKeyType apiKeyType = ApiKeyType.CoinbaseDeveloperPlatform)
        {
            this.apiKeyType = apiKeyType;

            // Validate input arguments
            Key = apiKey ?? throw new ArgumentNullException(nameof(apiKey), "API key cannot be null.");
            Secret = apiSecret ?? throw new ArgumentNullException(nameof(apiSecret), "API secret cannot be null.");

            // Initialize the REST client with the base Coinbase API URL
            _client = new RestClient("https://api.coinbase.com");
        }

        /// <summary>
        /// Sends an authenticated asynchronous request to a specified path.
        /// </summary>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="path">The path for the request.</param>
        /// <param name="queryParams">Optional query parameters to append to the request.</param>
        /// <param name="bodyObj">Optional body object to be sent with the request.</param>
        /// <returns>A Task representing the asynchronous operation, which upon completion returns a dictionary containing the response, or null if the response content is empty or invalid.</returns>
        public async Task<Dictionary<string, object>> SendAuthenticatedRequestAsync(string method, string path, Dictionary<string, string> queryParams = null, object bodyObj = null)
        {
            // Validate the 'method' parameter for null or empty values
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("Method cannot be null or empty", nameof(method));
            }

            // Ensure the provided method is a valid HTTP method
            if (!Enum.GetNames(typeof(Method)).Any(e => e.Equals(method, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Invalid method type", nameof(method));
            }

            // Generate headers required for the authenticated request
            var headers = CreateHeaders(method, path, bodyObj);

            // Execute the request and return the result
            return await ExecuteRequestAsync(method, path, bodyObj, headers, queryParams);
        }


        /// <summary>
        /// Generates headers for Coinbase Developer Platform (CDP) API key authentication using JWT (JSON Web Token).
        /// This method creates a JWT token using the 'GenerateJwt' method, which includes
        /// various claims such as issuer, subject, audience, and the request details (method and path).
        /// The generated JWT is then included in the Authorization header as a Bearer token.
        /// </summary>
        /// <param name="method">The HTTP method being used for the request (e.g., 'GET', 'POST').</param>
        /// <param name="path">The path of the API endpoint being accessed.</param>
        /// <returns>A dictionary of headers with the Authorization header containing the JWT for authenticating the request using Coinbase Developer Platform (CDP) API keys.</returns>
        private Dictionary<string, string> CreateJwtHeaders(string method, string path)
        {
            string jwtToken = JwtTokenGenerator.GenerateJwt(Key, Secret, "retail_rest_api_proxy", method, path);
            return new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {jwtToken}" }
            };
        }

        /// <summary>
        /// Executes an asynchronous REST request using the provided parameters.
        /// </summary>
        /// <param name="method">The HTTP method (GET, POST, PUT, etc.).</param>
        /// <param name="path">The request path.</param>
        /// <param name="bodyObj">The request body object.</param>
        /// <param name="headers">Headers to be added to the request.</param>
        /// <param name="queryParams">Query parameters to be added to the request.</param>
        /// <returns>A Task representing the asynchronous operation, which upon completion returns a dictionary representation of the response content, or null if the content is empty or only consists of white-space characters.</returns>
        private async Task<Dictionary<string, object>> ExecuteRequestAsync(string method, string path, object bodyObj, Dictionary<string, string> headers, Dictionary<string, string> queryParams)
        {
            try
            {
                // Create a new request object with the specified method and path
                if (!Enum.TryParse<Method>(method, ignoreCase: true, out var httpMethod))
                {
                    throw new ArgumentException($"Invalid method '{method}'.", nameof(method));
                }

                var request = new RestRequest(path, httpMethod);

                // Add headers to the request
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }

                // Add query parameters if provided
                if (queryParams != null)
                {
                    foreach (var param in queryParams)
                    {
                        request.AddParameter(param.Key, param.Value);
                    }
                }

                // Serialize and add the body if provided
                if (bodyObj != null)
                {
                    request.AddJsonBody(JsonConvert.SerializeObject(bodyObj));
                }

                // Execute the request
                var response = await _client.ExecuteAsync(request); // Using the async version of Execute

                return HandleResponse(response);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while executing the request.", ex);
            }
        }

        /// <summary>
        /// Parses and returns the response content as a dictionary.
        /// </summary>
        /// <param name="response">The response received from a REST request.</param>
        /// <returns>A dictionary representation of the response content, or null if the content is empty or only consists of white-space characters.</returns>
        private static Dictionary<string, object> HandleResponse(RestResponse response)
        {
            // Check if the response content is empty or just white-space
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return null;
            }

            // Deserialize the content into a dictionary
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
        }




        /// <summary>
        /// Creates headers for a request, with values appropriate to the type of API key being used.
        /// For legacy keys, it delegates to CreateLegacyHeaders method to include a signature based on the HMACSHA256 algorithm.
        /// For Cloud Trading keys, it includes a JWT (JSON Web Token) in the Authorization header.
        /// </summary>
        /// <param name="method">HTTP method being used (GET, POST, etc.)</param>
        /// <param name="path">API endpoint path.</param>
        /// <param name="bodyObj">Request body object, if any. Used in signature generation for legacy keys. Not used for JWT.</param>
        /// <returns>A dictionary containing headers for the request. The headers include authentication details appropriate to the API key type.</returns>
        private Dictionary<string, string> CreateHeaders(string method, string path, object bodyObj)
        {
            try
            {
                if (apiKeyType == ApiKeyType.CoinbaseDeveloperPlatform)
                {
                    // For JWT-based Cloud Trading keys, generate JWT headers
                    return CreateJwtHeaders(method, path);
                }
                else
                {
                    // For legacy keys, generate headers including the HMACSHA256 signature
                    return CreateLegacyHeaders(method, path, bodyObj);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Generates headers for legacy API key authentication.
        /// This method creates a signature using the HMACSHA256 algorithm based on a combination of
        /// the timestamp, HTTP method, request path, and the serialized request body (if present).
        /// The headers include the API key ('CB-ACCESS-KEY'), the generated signature ('CB-ACCESS-SIGN'),
        /// and the timestamp ('CB-ACCESS-TIMESTAMP') used in the signature.
        /// </summary>
        /// <param name="method">The HTTP method being used for the request (e.g., 'GET', 'POST').</param>
        /// <param name="path">The path of the API endpoint being accessed.</param>
        /// <param name="bodyObj">The request body object. This is serialized to JSON and included in the signature calculation. If null, it is omitted from the signature.</param>
        /// <returns>A dictionary of headers needed for authenticating the request using legacy API keys.</returns>
        [Obsolete("Legacy API key authentication is deprecated and will be removed in future versions.")]
        private Dictionary<string, string> CreateLegacyHeaders(string method, string path, object bodyObj)
        {
            // Serialize body object if present, otherwise set to null
            var body = bodyObj != null ? JsonConvert.SerializeObject(bodyObj) : null;
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var message = $"{timestamp}{method.ToUpper()}{path}{body}";
            var signature = GenerateSignature(message);

            return new Dictionary<string, string>
            {
                { "CB-ACCESS-KEY", Key },
                { "CB-ACCESS-SIGN", signature },
                { "CB-ACCESS-TIMESTAMP", timestamp }
            };
        }


        /// <summary>
        /// Generates a signature using HMACSHA256 for the provided message.
        /// </summary>
        /// <param name="message">The message for which the signature will be generated.</param>
        /// <returns>The generated signature in lowercase.</returns>
        [Obsolete("Legacy API key authentication is deprecated and will be removed in future versions.")]
        private string GenerateSignature(string message)
        {
            // Remove the query string from the message, if present
            message = RemoveQueryString(message);

            // Compute the signature for the refined message
            return ComputeHmacSignature(message);

            // Local function to remove the query string from the message
            string RemoveQueryString(string msg)
            {
                int queryStringIndex = msg.IndexOf('?');
                return queryStringIndex != -1 ? msg.Substring(0, queryStringIndex) : msg;
            }


            // Local function to compute the HMACSHA256 signature
            string ComputeHmacSignature(string msg)
            {
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret)))
                {
                    byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(msg));
                    return BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();
                }
            }
        }









    }
}
