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
            if (!ModelState.IsValid)
                return Page();

            var success = await _authService.RegisterAsync(Register);

            if (success)
            {
                // ? redirect to Login page after successful registration
                return RedirectToPage("/Auth/Login");
            }

            // if failed, stay on same page and show error message
            Message = "Registration failed!";
            return Page();
        }
    }
}
