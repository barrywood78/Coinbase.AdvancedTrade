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
        /// <exception cref="ArgumentNullException">Thrown when either <paramref name="apiKey"/> or <paramref name="apiSecret"/> is null.</exception>
        public CoinbaseAuthenticator(string apiKey, string apiSecret)
        {
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
            var headers = CreateJwtHeaders(method, path);

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
    }
}
