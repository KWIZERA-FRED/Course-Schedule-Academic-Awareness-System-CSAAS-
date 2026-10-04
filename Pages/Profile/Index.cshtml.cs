using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public User CurrentUser { get; private set; } = default!;

        [BindProperty] public string FirstName   { get; set; } = string.Empty;
        [BindProperty] public string LastName    { get; set; } = string.Empty;
        [BindProperty] public string Email       { get; set; } = string.Empty;
        [BindProperty] public string PhoneNumber { get; set; } = string.Empty;
        [BindProperty] public string NewPassword { get; set; } = string.Empty;
        [BindProperty] public string ConfirmPassword { get; set; } = string.Empty;

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public string DashboardUrl => CurrentUser.Role switch
        {
            UserRole.ClassRepresentative => "/CP/Dashboard",
            UserRole.Lecturer            => "/Lecturer/Dashboard",
            UserRole.HOD                 => "/HOD/Dashboard",
            UserRole.Dean                => "/Dean/Dashboard",
            UserRole.DirectorOfQuality   => "/Quality/Dashboard",
            _                            => "/Student/Dashboard"
        };

        public void OnGet()
        {
            LoadUser();
            FirstName   = CurrentUser.FirstName;
            LastName    = CurrentUser.LastName;
            Email       = CurrentUser.Email;
            PhoneNumber = CurrentUser.PhoneNumber;
        }

        public IActionResult OnPostSaveProfile()
        {
            LoadUser();

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                StatusMessage = "First name and last name are required.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            var emailTaken = UserData.Users.Any(u =>
                u.Id != CurrentUser.Id &&
                u.Email.Equals(Email.Trim(), StringComparison.OrdinalIgnoreCase));

            if (emailTaken)
            {
                StatusMessage = "That email address is already used by another account.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            CurrentUser.FirstName   = FirstName.Trim();
            CurrentUser.LastName    = LastName.Trim();
            CurrentUser.Email       = Email.Trim().ToLower();
            CurrentUser.PhoneNumber = PhoneNumber?.Trim() ?? string.Empty;

            StatusMessage = "✓ Profile updated successfully.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostChangePassword()
        {
            LoadUser();

            if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length < 6)
            {
                StatusMessage = "Password must be at least 6 characters.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            if (NewPassword != ConfirmPassword)
            {
                StatusMessage = "Passwords do not match.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            CurrentUser.PasswordHash = NewPassword;

            StatusMessage = "✓ Password changed. Use your new password next time you log in.";
            StatusType    = "success";
            return RedirectToPage();
        }

        private void LoadUser()
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u =>
                u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "User", LastName = "" };
        }
    }
}
