using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestAPP.Models;

namespace TestAPP.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://ezl-test.vercel.app/");
        }

        public async Task<UserGuildsResponse> GetGuildsAsync()
        {
            var guilds = await _httpClient.GetAsync("userGuilds");
            guilds.EnsureSuccessStatusCode();
            var content = await guilds.Content.ReadFromJsonAsync<UserGuildsResponse>();
            return content;
        }

        public async Task<GuildsListResponse> GetGuildsListAsync()
        {
            try
            {
                var guilds = await _httpClient.GetAsync("allGuilds");

                if (!guilds.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"API Error: {guilds.StatusCode}");
                    return null; // Return null in case of error
                }

                var content = await guilds.Content.ReadFromJsonAsync<GuildsListResponse>();
                return content;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<GuildsListResponse> GetGuildsByLocationAsync(string location)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"allGuilds?location={Uri.EscapeDataString(location)}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching guilds: {response.StatusCode}");
                    return null;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GuildsListResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetGuildsByLocationAsync: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> CreateGuildAsync(object newGuild)
        {
            string jsonContent = JsonSerializer.Serialize(newGuild);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("addGuild", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }




    }

}
