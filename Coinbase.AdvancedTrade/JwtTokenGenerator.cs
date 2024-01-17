using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using System.IO;


/*
 * This class is responsible for generating JSON Web Tokens (JWTs) for authenticating with the Coinbase Cloud API.
 * It utilizes the Bouncy Castle library and the Jose library due to the following reasons:
 *
 * 1. Bouncy Castle Library:
 *    - .NET Standard 2.0 does not natively support parsing PEM-formatted keys.
 *    - Bouncy Castle is used to read and process PEM-formatted private keys, converting them into a format
 *      that can be used with .NET's cryptographic libraries.
 *
 * 2. Jose Library:
 *    - The Jose library is used for encoding and signing the JWT in a standard-compliant manner.
 *    - .NET Standard 2.0's built-in capabilities for JWT handling are limited, particularly for ECC-based JWTs.
 *
 * These libraries are essential to provide the necessary cryptographic functionality that is not natively available in .NET Standard 2.0.
 */


/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWTs) for authenticating with the Coinbase Cloud API.
/// This utility class supports creating tokens with both REST and WebSocket service identifiers.
/// </summary>
public static class JwtTokenGenerator
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for authenticating requests with Cloud Trading API keys.
    /// The JWT can be used in the Authorization header of the HTTP request.
    /// </summary>
    /// <param name="key">The API key provided by Coinbase for Cloud API Trading.</param>
    /// <param name="secret">The API secret provided by Coinbase for Cloud API Trading.</param>
    /// <param name="service">The service identifier for the API (e.g., 'public_websocket_api' or 'retail_rest_api_proxy').</param>
    /// <param name="method">Optional. The HTTP method being used for the request (e.g., 'GET', 'POST'). Not required for WebSocket authentication.</param>
    /// <param name="path">Optional. The path of the API endpoint being accessed. Not required for WebSocket authentication.</param>
    /// <returns>A signed JWT in string format for use in the Authorization header.</returns>
    /// <exception cref="Exception">Throws an exception if there is an issue in generating the JWT.</exception>
    public static string GenerateJwt(string key, string secret, string service, string method = null, string path = null)
    {
        try
        {
            ECDsa privateKey = null;
            string modifiedSecret = secret.Replace("\\n", "\n");

            using (var reader = new StringReader(modifiedSecret))
            {
                var pemReader = new PemReader(reader);
                var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                var privateKeyParameters = (ECPrivateKeyParameters)keyPair.Private;
                var publicKeyParameters = (ECPublicKeyParameters)keyPair.Public;

                // Convert the private key 'D' value to a byte array
                var d = privateKeyParameters.D.ToByteArrayUnsigned();

                // Get the public key's elliptic curve point 'Q'
                var q = publicKeyParameters.Q;

                // Convert the X and Y coordinates of point 'Q' to byte arrays
                // These represent the public key components
                var x = q.AffineXCoord.GetEncoded();
                var y = q.AffineYCoord.GetEncoded();

                // Create a new ECDsa object with the specified ECParameters
                // The ECParameters include the curve details, private key 'D', and public key point 'Q'
                privateKey = ECDsa.Create(new ECParameters
                {
                    Curve = ECCurve.NamedCurves.nistP256, // Specify the elliptic curve used
                    D = d,                                // Set the private key component
                    Q = new ECPoint { X = x, Y = y }      // Set the public key components
                });
            }


            string request_host = "api.coinbase.com";
            string request_path = path != null && path.Contains("?") ? path.Substring(0, path.IndexOf('?')) : path;

            var payload = new Dictionary<string, object>
            {
                { "sub", key },
                { "iss", "coinbase-cloud" },
                { "nbf", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
                { "exp", DateTimeOffset.UtcNow.AddSeconds(60).ToUnixTimeSeconds() },
                { "aud", new string[] { service } }
            };

            if (method != null && request_path != null)
            {
                payload.Add("uri", $"{method} {request_host}{request_path}");
            }

            var extraHeaders = new Dictionary<string, object>
            {
                { "kid", key },
                { "nonce", GenerateNonce() },
                { "typ", "JWT" }
            };

            var token = JWT.Encode(payload, privateKey, JwsAlgorithm.ES256, extraHeaders: extraHeaders);
            privateKey?.Dispose();

            return token;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Generates a unique nonce value.
    /// This method generates a 32-byte random value which is sufficiently large to ensure uniqueness and security.
    /// </summary>
    /// <returns>A nonce value as a 64-character lowercase hexadecimal string.</returns>
#pragma warning disable SYSLIB0023 // Disable the obsolete RNGCryptoServiceProvider warning
    private static string GenerateNonce()
    {
        var randomBytes = new byte[32];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        return BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
    }
#pragma warning restore SYSLIB0023 // Restore the warning

}
