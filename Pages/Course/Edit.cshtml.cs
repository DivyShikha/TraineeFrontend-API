using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Model;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Course
{
    public class EditModel : PageModel
    {
        private readonly CourseService _service;

        [BindProperty]
        public Model.Course Course { get; set; }  

        public EditModel(CourseService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _service.GetCourseById(id);
            if (Course == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // re-render form with errors
            }

            var success = await _service.UpdateCourse(Course.CourseID, Course);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to update course. Please try again.");
                return Page();
            }

            return RedirectToPage("CourseList");
        }
    }
}
