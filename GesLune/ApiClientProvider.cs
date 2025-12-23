using DotNetEnv;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace GesLune
{
    public static class ApiClientProvider
    {
        private static HttpClient _client;
        public static string BaseUrl { get; private set; }

        static ApiClientProvider()
        {
            // Try to load from appsettings.json first
            BaseUrl = LoadBaseUrlFromAppsettings();
            
            // Fallback to environment variable if appsettings.json is not found
            if (string.IsNullOrEmpty(BaseUrl))
            {
                Env.Load();
                BaseUrl = Environment.GetEnvironmentVariable("API_URL");
            }

            if (string.IsNullOrEmpty(BaseUrl))
            {
                throw new InvalidOperationException("API_URL not configured in appsettings.json or environment variables");
            }

            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
        }

        private static string LoadBaseUrlFromAppsettings()
        {
            try
            {
                var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                if (!File.Exists(appSettingsPath))
                {
                    return null;
                }

                var json = File.ReadAllText(appSettingsPath);
                using var doc = System.Text.Json.JsonDocument.Parse(json);
                var root = doc.RootElement;
                
                if (root.TryGetProperty("ApiSettings", out var apiSettings) &&
                    apiSettings.TryGetProperty("BaseUrl", out var baseUrl))
                {
                    return baseUrl.GetString();
                }
            }
            catch
            {
                // If there's any error reading appsettings.json, fall back to environment variable
            }

            return null;
        }

        public static HttpClient Client => _client;
    }
}
