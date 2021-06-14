using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Humbee.Api.Samples
{
    public class ProfileSample
    {
        public static async Task ShowProfileAsync(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("/api/profile");
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine("Your current profile:");
            Console.WriteLine(responseText);
        }
    }
}