using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TraineeFrontend.Service;

namespace TraineeFrontend.Pages.Auth
{
    public class LogoutPageModel : PageModel
    {
        private readonly AuthService _authService;

        public LogoutPageModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ✅ Tell backend to clear cookie
            await _authService.LogoutAsync();

            // ✅ Clear session in frontend
            HttpContext.Session.Clear();

            // Redirect back to login page
            return RedirectToPage("/Auth/Login");
        }
    }
}
