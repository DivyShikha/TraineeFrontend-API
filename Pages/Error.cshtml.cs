using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TraineeFrontend.Pages
{
    public class ErrorModel : PageModel
    {
        public string Message { get; set; } = "An unexpected error occurred.";

        public void OnGet(string? msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Message = msg;
            }
        }
    }
}
