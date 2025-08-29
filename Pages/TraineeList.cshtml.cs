using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Model;

namespace TraineeFrontend.Pages
{
    public class TraineeListModel : PageModel

    {
        private readonly TraineeService _service;
        public TraineeListModel(TraineeService service)
        {
            _service = service;
        }
        public List<Trainee> TraineeList { get; set; } = new();
        public async Task OnGetAsync()
        {
            TraineeList = await _service.GetTrainees();
        }

        // Delete trainee
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteTrainee(id);
            return RedirectToPage();
        }
    }
}