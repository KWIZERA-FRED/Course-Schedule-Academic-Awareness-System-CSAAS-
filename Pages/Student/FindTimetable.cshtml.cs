using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class FindTimetableModel : PageModel
    {
        public User                    CurrentUser    { get; private set; } = default!;
        public List<AcademicTimetable> Results        { get; private set; } = new();
        public int                     UnreadNotifications { get; private set; }

        // Selected filter values
        public string  SelectedDept       { get; private set; } = string.Empty;
        public int     SelectedYear        { get; private set; }
        public int     SelectedSemester    { get; private set; }
        public string  SelectedAcademicYear{ get; private set; } = string.Empty;
        public bool    HasSearched         { get; private set; }

        public string[] Departments  => TimetableStore.Departments;
        public string[] AcademicYears => TimetableStore.AcademicYears.ToArray();

        public void OnGet(string? dept, int? year, int? semester, string? academicYear)
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser    = UserData.Users.FirstOrDefault(u => u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "Student", LastName = "" };

            UnreadNotifications = NotificationData.Notifications
                .Count(n => n.UserEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase) && !n.IsRead);

            // Pre-fill from student's own department
            SelectedDept        = dept         ?? CurrentUser.Department;
            SelectedYear        = year         ?? 0;
            SelectedSemester    = semester     ?? 0;
            SelectedAcademicYear= academicYear ?? string.Empty;

            HasSearched = !string.IsNullOrEmpty(dept) || year.HasValue || semester.HasValue;

            if (HasSearched || !string.IsNullOrEmpty(SelectedDept))
            {
                Results = TimetableStore.GetPublished(
                    string.IsNullOrEmpty(SelectedDept) ? null : SelectedDept,
                    SelectedYear > 0     ? SelectedYear     : null,
                    SelectedSemester > 0 ? SelectedSemester : null,
                    string.IsNullOrEmpty(SelectedAcademicYear) ? null : SelectedAcademicYear
                );
            }
        }
    }
}
