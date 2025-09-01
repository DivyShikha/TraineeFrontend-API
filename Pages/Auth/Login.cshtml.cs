using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Models;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Auth
{
    public class LoginPageModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginPageModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginModel Login { get; set; } = new();

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var success = await _authService.LoginAsync(Login);

            if (success)
            {
                // ? Redirect to Home page
                return RedirectToPage("/Index");
            }

            Message = "Invalid login attempt.";
            return Page();
        }


    }
}
