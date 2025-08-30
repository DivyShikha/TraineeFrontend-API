using System.Net.Http.Json;
using TraineeFrontend.Model;

namespace TraineeFrontend.Service
{
    public class EnrolmentService
    {
        private readonly HttpClient _httpClient;

        public EnrolmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7240/api/");
        }

        // Get all enrolments
        public async Task<List<Enrolment>> GetEnrolmentsAsync()
        {
            var enrolments = await _httpClient.GetFromJsonAsync<List<Enrolment>>("Enrolment");
            return enrolments ?? new List<Enrolment>();
        }

        // Get single enrolment by id
        public async Task<Enrolment?> GetEnrolmentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Enrolment>($"Enrolment/{id}");
        }

        // Create enrolment
        public async Task<bool> AddEnrolmentAsync(Enrolment enrolment)
        {
            var response = await _httpClient.PostAsJsonAsync("Enrolment", enrolment);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"POST Enrolment failed: {response.StatusCode} - {error}");
            }

            return response.IsSuccessStatusCode;
        }

        // Update enrolment
        public async Task<bool> UpdateEnrolmentAsync(int id, Enrolment enrolment)
        {
            var response = await _httpClient.PutAsJsonAsync($"Enrolment/{id}", enrolment);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"PUT Enrolment failed: {response.StatusCode} - {error}");
            }

            return response.IsSuccessStatusCode;
        }

        // Delete enrolment
        public async Task<bool> DeleteEnrolmentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Enrolment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"DELETE Enrolment failed: {response.StatusCode} - {error}");
            }

            return response.IsSuccessStatusCode;
        }
    }
}
