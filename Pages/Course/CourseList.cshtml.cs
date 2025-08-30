using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Model;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Course
{
    public class CourseListModel : PageModel
    {
        private readonly CourseService _courseService;

        public CourseListModel(CourseService courseService)
        {
            _courseService = courseService;
        }

        public List<Model.Course> Courses { get; set; } = new();

        // GET: Load all courses
        public async Task OnGetAsync()
        {
            Courses = await _courseService.GetCourses();
        }

        //POST: Delete a course
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _courseService.DeleteCourse(id);
            if (!success)
            {
                // Could add ModelState.AddModelError here for UI message
            }
            return RedirectToPage();
        }
    }
}
