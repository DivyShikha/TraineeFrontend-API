using System.Net.Http;
using System.Net.Http.Json;
using TraineeFrontend.Model;

namespace TraineeFrontend.Service
{
    public class TraineeService
    {
        private readonly HttpClient _httpClient;

        public TraineeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7240/api/");
        }

        // ✅ Get all trainees
        public async Task<List<Trainee>> GetTrainees()
        {
            return await _httpClient.GetFromJsonAsync<List<Trainee>>("Trainee");
        }

        // ✅ Get trainee by Id
        public async Task<Trainee?> GetTraineeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Trainee>($"Trainee/{id}");
        }

        // ✅ Add new trainee
        public async Task<Trainee?> AddTrainee(Trainee trainee)
        {
            var response = await _httpClient.PostAsJsonAsync("Trainee", trainee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Trainee>();
        }

        // ✅ Update trainee
        public async Task<bool> UpdateTrainee(int id, Trainee trainee)
        {
            var response = await _httpClient.PutAsJsonAsync($"Trainee/{id}", trainee);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete trainee
        public async Task<bool> DeleteTrainee(int id)
        {
            var response = await _httpClient.DeleteAsync($"Trainee/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
