using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TraineeFrontend.Model;

namespace TraineeFrontend.Model
{
    public class TraineeService
    {
        private readonly HttpClient _httpClient;

        public TraineeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7240/api/");
        }

        public async Task<List<Trainee>> GetTrainees()
        {
            await using Stream stream = await _httpClient.GetStreamAsync("Trainee");
            var trainees = await JsonSerializer.DeserializeAsync<List<Trainee>>(stream);
            return trainees;
        }

        public async Task<Trainee> GetTraineeById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Trainee>($"Trainee/{id}");
        }

        public async Task<Trainee> AddTrainee(Trainee trainee)
        {
            var response = await _httpClient.PostAsJsonAsync("Trainee", trainee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Trainee>();
        }

        public async Task<bool> UpdateTrainee(int id, Trainee trainee)
        {
            var response = await _httpClient.PutAsJsonAsync($"Trainee/{id}", trainee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTrainee(int id)
        {
            var response = await _httpClient.DeleteAsync($"Trainee/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
