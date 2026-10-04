using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseScheduleSystem.Web.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync("CSASAuth");
            return RedirectToPage("/Login");
        }
    }
}
