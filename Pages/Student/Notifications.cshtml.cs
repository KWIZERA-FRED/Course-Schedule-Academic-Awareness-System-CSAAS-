using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class NotificationsModel : PageModel
    {
        public User                  CurrentUser   { get; private set; } = default!;
        public List<Notification>    All           { get; private set; } = new();
        public int                   UnreadCount   { get; private set; }

        public void OnGet()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var id    = User.FindFirst("Identifier")?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email || u.Identifier == id)
                          ?? new User { FirstName = "Student", LastName = "" };

            All = NotificationData.Notifications
                .Where(n => n.UserEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            UnreadCount = All.Count(n => !n.IsRead);
        }
        public IActionResult OnPostMarkRead(int id)
        {
            var n = NotificationData.Notifications.FirstOrDefault(x => x.Id == id);
            if (n != null) n.IsRead = true;
            return RedirectToPage();
        }
        public IActionResult OnPostMarkAllRead()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            foreach (var n in NotificationData.Notifications
                .Where(x => x.UserEmail.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                n.IsRead = true;
            }
            return RedirectToPage();
        }
    }
}
