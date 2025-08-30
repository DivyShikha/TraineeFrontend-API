using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Course
{
    public class CreateModel : PageModel
    {
        private readonly CourseService _service;

        public CreateModel(CourseService service)
        {
            _service = service;
        }

        [BindProperty]
        public Model.Course Course { get; set; } = new(0, "", 0);
        public void OnGet()
        {
            //renders Create Form
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _service.AddCourse(Course);
            return RedirectToPage("CourseList");
        }
    }
}
