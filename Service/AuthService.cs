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
            _httpClient.BaseAddress = new Uri("https://localhost:7240/"); // backend root
        }

        // ✅ Register → calls /auth/register
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", model);
            return response.IsSuccessStatusCode;
        }

        // ✅ Login → calls /auth/login
        public async Task<bool> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login failed: {response.StatusCode} - {error}");
            }

            // 🍪 Cookie is automatically stored in HttpClient’s CookieContainer
            return response.IsSuccessStatusCode;
        }

        // ✅ Logout → calls /auth/logout
        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("auth/logout", null);
        }
    }
}
