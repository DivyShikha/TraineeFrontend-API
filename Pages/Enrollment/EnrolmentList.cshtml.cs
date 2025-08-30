using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Service;
using TraineeFrontend.Model;

namespace TraineeFrontend.Pages.Enrolment
{
    public class EnrolmentListModel : PageModel
    {
        private readonly EnrolmentService _enrolmentService;
        private readonly TraineeService _traineeService;
        private readonly CourseService _courseService;

        public EnrolmentListModel(
            EnrolmentService enrolmentService,
            TraineeService traineeService,
            CourseService courseService)
        {
            _enrolmentService = enrolmentService;
            _traineeService = traineeService;
            _courseService = courseService;
        }

        public List<Model.Enrolment> Enrolments { get; set; } = new();

        // Lookup dictionaries
        public Dictionary<int, string> CourseNames { get; set; } = new();
        public Dictionary<int, string> TraineeNames { get; set; } = new();

        public async Task OnGetAsync()
        {
            Enrolments = await _enrolmentService.GetEnrolmentsAsync();
            var trainees = await _traineeService.GetTrainees();
            var courses = await _courseService.GetCourses();

            CourseNames = courses.ToDictionary(c => c.CourseID, c => c.Title);
            TraineeNames = trainees.ToDictionary(t => t.Id, t => t.Name);
        }
    }
}
