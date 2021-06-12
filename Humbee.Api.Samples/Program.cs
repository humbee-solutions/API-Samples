using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Humbee.Api.Samples.Models;

namespace Humbee.Api.Samples
{
    public class Program
    {
        private const string ApiBaseUrl = "https://cloud.humbee.de/api/";

        public static async Task Main(string[] args)
        {
            var userEmail = args[1];
            var userPassword = args[2];
            var clientId = args[3];
            var clientSecret = args[4];
            var tenantId = args[5];

            Console.WriteLine("Try to get token from humbee...");

            var httpClient = await GetHumbeeApiHttpClientAsync(userEmail, userPassword, clientId, clientSecret, tenantId);

            Console.WriteLine("Token received - now issuing some API requests...");

            await ShowMyProfileAsync(httpClient);
        }

        private static async Task ShowMyProfileAsync(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("/profile");
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }

        private static async Task<HttpClient> GetHumbeeApiHttpClientAsync(string userName, string userPassword, string clientId, string clientSecret, string tenantId)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            var parameters = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", userName},
                {"password", userPassword},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"tenant_id", tenantId}
            };

            var content = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync("/oauth/token", content);

            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            var tokenResponseModel = JsonSerializer.Deserialize<TokenModel>(responseText);

            httpClient.DefaultRequestHeaders.Add("Bearer", tokenResponseModel?.AccessToken);

            return httpClient;
        }
    }
}
