using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Model;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Trainee
{
    public class CreateModel : PageModel
    {
        private readonly TraineeService _service;

        public CreateModel(TraineeService service) // DI will inject the service
        {
            _service = service;
        }

        [BindProperty]
        public Model.Trainee Trainee { get; set; }

        public void OnGet()
        {
            // Just renders the empty form
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // re-render with validation errors
            }

            await _service.AddTrainee(Trainee);
            return RedirectToPage("TraineeList");
        }
    }
}
