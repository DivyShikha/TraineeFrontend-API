using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Models;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Auth
{
    public class RegisterPageModel : PageModel
    {
        private readonly AuthService _authService;

        public RegisterPageModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterModel Register { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var success = await _authService.RegisterAsync(Register);
            Message = success ? "Registration successful!" : "Registration failed!";

            return Page();
        }
    }
}
