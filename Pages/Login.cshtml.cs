using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

// Alias to avoid name clash with CourseScheduleSystem.Web.Models.Claim
using SecurityClaim          = System.Security.Claims.Claim;
using SecurityClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using SecurityClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;
using SecurityClaimTypes      = System.Security.Claims.ClaimTypes;

namespace CourseScheduleSystem.Web.Pages
{
    public class LoginModel : PageModel
    {
        // -- Bound form fields ----------------------------------

        [BindProperty]
        public string Identifier { get; set; } = string.Empty;   // email OR reg. number

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        // -- Page state -----------------------------------------

        public string? ErrorMessage { get; set; }

        // -- GET -----------------------------------------------

        public void OnGet() { }

        // -- POST ----------------------------------------------

        public async Task<IActionResult> OnPostAsync()
        {
            // Basic field validation
            if (string.IsNullOrWhiteSpace(Identifier))
            {
                ErrorMessage = "Please enter your email or registration number.";
                return Page();
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter your password.";
                return Page();
            }

            // -- Find user by email or registration identifier ----

            var user = UserData.Users.FirstOrDefault(u =>
                u.Email.Equals(Identifier.Trim(), StringComparison.OrdinalIgnoreCase) ||
                u.Identifier.Equals(Identifier.Trim(), StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                ErrorMessage = "No account found with that email or registration number.";
                return Page();
            }

            if (!user.IsActive)
            {
                ErrorMessage = "This account has been deactivated. Contact the administrator.";
                return Page();
            }

            // -- Verify password -----------------------------------
            // NOTE: PasswordHash currently stores plain-text demo passwords.
            // Replace with BCrypt.Verify(Password, user.PasswordHash) when
            // connecting a real backend.

            if (!user.PasswordHash.Equals(Password, StringComparison.Ordinal))
            {
                ErrorMessage = "Incorrect password. Please try again.";
                return Page();
            }

            // -- Build claims and sign in --------------------------

            var claims = new List<SecurityClaim>
            {
                new SecurityClaim(SecurityClaimTypes.NameIdentifier, user.Id.ToString()),
                new SecurityClaim(SecurityClaimTypes.Name,           user.FullName),
                new SecurityClaim(SecurityClaimTypes.Email,          user.Email),
                new SecurityClaim(SecurityClaimTypes.Role,           user.Role.ToString()),
                new SecurityClaim("Department",                      user.Department),
                new SecurityClaim("Identifier",                      user.Identifier)
            };

            var identity  = new SecurityClaimsIdentity(claims, "CSASAuth");
            var principal = new SecurityClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CSASAuth", principal);

            // -- Redirect to the matching role dashboard -----------

            return user.Role switch
            {
                UserRole.Student             => RedirectToPage("/Student/Dashboard"),
                UserRole.ClassRepresentative => RedirectToPage("/CP/Dashboard"),
                UserRole.Lecturer            => RedirectToPage("/Lecturer/Dashboard"),
                UserRole.HOD                 => RedirectToPage("/HOD/Dashboard"),
                UserRole.Dean                => RedirectToPage("/Dean/Dashboard"),
                UserRole.DirectorOfQuality   => RedirectToPage("/Quality/Dashboard"),
                _                            => RedirectToPage("/Index")
            };
        }
    }
}
