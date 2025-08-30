using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Model;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Trainee
{
    public class EditModel : PageModel
    {
        private readonly TraineeService _service;

        public EditModel(TraineeService service)
        {
            _service = service;
        }

        [BindProperty]
        public Model.Trainee Trainee { get; set; }

        // Load trainee details for editing
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Trainee = await _service.GetTraineeById(id);

            if (Trainee == null)
            {
                return NotFound(); // trainee doesn’t exist
            }

            return Page(); // load edit form with trainee data
        }

        // Save trainee after edit
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // re-render form with errors
            }

            var success = await _service.UpdateTrainee(Trainee.Id, Trainee);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to update trainee. Please try again.");
                return Page();
            }

            return RedirectToPage("TraineeList");
        }

    }
}
