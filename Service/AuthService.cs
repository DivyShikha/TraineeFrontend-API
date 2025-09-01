using System.Net.Http.Json;
using TraineeFrontend.Models;

namespace TraineeFrontend.Service
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7240/");
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("register", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(LoginModel model, bool useCookies = true, bool useSessionCookies = false)
        {
            // Build query string
            var url = $"login?useCookies={useCookies.ToString().ToLower()}&useSessionCookies={useSessionCookies.ToString().ToLower()}";

            var response = await _httpClient.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed: {response.StatusCode} - {error}");
            }

            return response.IsSuccessStatusCode;
        }
    }
}
