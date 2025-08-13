// Quantum Random Number Generator SDK for Unity
// Copyright 2025 Nicolas K. Perkins
// 
// This Unity SDK provides easy access to true quantum random numbers
// from NIST-certified quantum sources through our hosted API service.
//
// Get your API key at: https://nickinper.github.io/quantum-random-api/
// Documentation: https://github.com/nickinper/quantum-rng-unity-sdk

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace QuantumRNG
{
    /// <summary>
    /// Response structure from the Quantum RNG API
    /// </summary>
    [Serializable]
    public class QrngApiResponse
    {
        public bool success;
        public int count;
        public List<int> data;
        public QrngApiProof proof;
    }

    /// <summary>
    /// Proof structure showing the quantum randomness source
    /// </summary>
    [Serializable]
    public class QrngApiProof
    {
        public string source;
        public string pulseUri;
        public string timestamp;
    }

    /// <summary>
    /// Main Quantum Random Number Generator SDK
    /// Provides simple, static methods to fetch true quantum random numbers
    /// </summary>
    public static class QuantumRandom
    {
        // Production API endpoint - your live Render deployment
        private const string PRODUCTION_API_URL = "https://quantum-random-api.onrender.com/api/v1/random";
        
        private static string _apiKey;
        private static bool _isInitialized = false;

        /// <summary>
        /// Initialize the Quantum RNG SDK with your API key
        /// Get your API key at: https://your-quantum-api.com
        /// </summary>
        /// <param name="apiKey">Your developer API key</param>
        public static void Initialize(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                Debug.LogError("[QuantumRNG] API key cannot be empty. Get your key at: https://your-quantum-api.com");
                return;
            }

            _apiKey = apiKey;
            _isInitialized = true;
            
            Debug.Log($"[QuantumRNG] SDK initialized successfully");
        }

        /// <summary>
        /// Asynchronously fetches quantum random integers
        /// </summary>
        /// <param name="count">Number of random integers to generate (1-1000)</param>
        /// <param name="onSuccess">Callback invoked with the list of quantum random numbers</param>
        /// <param name="onError">Callback invoked with error message if request fails</param>
        /// <returns>Coroutine to be started with StartCoroutine</returns>
        public static IEnumerator GetIntegers(int count, Action<List<int>> onSuccess, Action<string> onError)
        {
            // Validation
            if (!_isInitialized)
            {
                onError?.Invoke("QuantumRNG SDK not initialized. Call QuantumRandom.Initialize(yourApiKey) first.");
                yield break;
            }

            if (count <= 0 || count > 1000)
            {
                onError?.Invoke("Count must be between 1 and 1000");
                yield break;
            }

            string requestUrl = $"{PRODUCTION_API_URL}?count={count}";
            
            using (UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl))
            {
                // Set timeout and headers
                webRequest.timeout = 30;
                webRequest.SetRequestHeader("x-api-key", _apiKey);
                webRequest.SetRequestHeader("User-Agent", $"QuantumRNG-Unity-SDK/1.0");

                // Send request
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        // Parse response
                        QrngApiResponse response = JsonUtility.FromJson<QrngApiResponse>(webRequest.downloadHandler.text);
                        
                        if (response.success && response.data != null)
                        {
                            Debug.Log($"[QuantumRNG] Successfully generated {response.data.Count} quantum random numbers from {response.proof?.source}");
                            onSuccess?.Invoke(response.data);
                        }
                        else
                        {
                            onError?.Invoke("API returned unsuccessful response. Check your API key and account status.");
                        }
                    }
                    catch (Exception e)
                    {
                        onError?.Invoke($"Failed to parse API response: {e.Message}");
                    }
                }
                else
                {
                    // Handle different error types
                    string errorMessage = GetErrorMessage(webRequest.error, webRequest.responseCode);
                    onError?.Invoke(errorMessage);
                }
            }
        }

        /// <summary>
        /// Generates a single quantum random integer between min and max (inclusive)
        /// </summary>
        /// <param name="min">Minimum value (inclusive)</param>
        /// <param name="max">Maximum value (inclusive)</param>
        /// <param name="onSuccess">Callback invoked with the random number</param>
        /// <param name="onError">Callback invoked with error message if request fails</param>
        /// <returns>Coroutine to be started with StartCoroutine</returns>
        public static IEnumerator GetIntegerInRange(int min, int max, Action<int> onSuccess, Action<string> onError)
        {
            if (min >= max)
            {
                onError?.Invoke("Min value must be less than max value");
                yield break;
            }

            yield return GetIntegers(1, 
                (numbers) => {
                    // Convert quantum number to specified range
                    int range = max - min + 1;
                    int result = min + (Math.Abs(numbers[0]) % range);
                    onSuccess?.Invoke(result);
                },
                onError
            );
        }

        /// <summary>
        /// Generates a quantum random float between 0.0 and 1.0
        /// </summary>
        /// <param name="onSuccess">Callback invoked with the random float</param>
        /// <param name="onError">Callback invoked with error message if request fails</param>
        /// <returns>Coroutine to be started with StartCoroutine</returns>
        public static IEnumerator GetFloat(Action<float> onSuccess, Action<string> onError)
        {
            yield return GetIntegers(1,
                (numbers) => {
                    // Convert integer to float between 0.0 and 1.0
                    uint unsignedValue = (uint)numbers[0];
                    float result = unsignedValue / (float)uint.MaxValue;
                    onSuccess?.Invoke(result);
                },
                onError
            );
        }

        /// <summary>
        /// Check if the SDK is properly initialized
        /// </summary>
        public static bool IsInitialized => _isInitialized;

        /// <summary>
        /// Get user-friendly error messages
        /// </summary>
        private static string GetErrorMessage(string error, long responseCode)
        {
            if (error.Contains("Cannot resolve destination host"))
            {
                return "Unable to connect to Quantum RNG service. Check your internet connection.";
            }
            else if (error.Contains("timeout"))
            {
                return "Request timed out. Please try again.";
            }
            else if (responseCode == 401)
            {
                return "Invalid API key. Get a valid key at: https://your-quantum-api.com";
            }
            else if (responseCode == 429)
            {
                return "Rate limit exceeded. Please wait before making another request.";
            }
            else if (responseCode >= 500)
            {
                return "Quantum RNG service is temporarily unavailable. Please try again later.";
            }
            else
            {
                return $"Network error: {error}";
            }
        }
    }
}