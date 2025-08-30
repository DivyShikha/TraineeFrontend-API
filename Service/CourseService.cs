using System.Text.Json;
using TraineeFrontend.Model;

namespace TraineeFrontend.Service
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7240/api/");
        }
        public async Task<List<Course>> GetCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("Course")
            ?? new List<Course>();

        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"Course/{id}");
        }

        public async Task<Course> AddCourse(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync("Course", course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Course>();
        }
        public async Task<bool> UpdateCourse(int id, Course course)
        {
            var response = await _httpClient.PutAsJsonAsync($"Course/{id}", course);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var response = await _httpClient.DeleteAsync($"Course/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
