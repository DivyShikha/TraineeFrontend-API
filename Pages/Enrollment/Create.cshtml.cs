using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TraineeFrontend.Model;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Enrolment
{
    public class CreateModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;
        private readonly TraineeService _traineeService;
        private readonly CourseService _courseService;

        public CreateModel(EnrolmentService enrolmentService, TraineeService traineeService, CourseService courseService)
        {
            _enrolmentService = enrolmentService;
            _traineeService = traineeService;
            _courseService = courseService;
        }

        [BindProperty]
        public Model.Enrolment Enrolment { get; set; } = new(0, "", 0, 0, null, null);

        public SelectList Trainees { get; set; } = default!;
        public SelectList Courses { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var trainees = await _traineeService.GetTrainees();
            var courses = await _courseService.GetCourses();

            Trainees = new SelectList(trainees, "Id", "Name");
            Courses = new SelectList(courses, "CourseID", "Title");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var trainees = await _traineeService.GetTrainees();
                var courses = await _courseService.GetCourses();

                Trainees = new SelectList(trainees, "Id", "Name");
                Courses = new SelectList(courses, "CourseID", "Title");

                return Page();
            }

            var success = await _enrolmentService.AddEnrolmentAsync(Enrolment);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to create enrolment.");
                return Page();
            }

            return RedirectToPage("EnrolmentList");
        }
    }
}
