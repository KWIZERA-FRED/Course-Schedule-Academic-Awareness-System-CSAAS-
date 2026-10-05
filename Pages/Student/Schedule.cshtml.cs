using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class ScheduleModel : PageModel
    {
        public User                CurrentUser  { get; private set; } = default!;
        public List<Course>        Courses      { get; private set; } = new();
        public List<Course>        AllCourses   { get; private set; } = new();
        public List<SessionReport> Sessions     { get; private set; } = new();

        public void OnGet()
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u =>
                u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "Student", LastName = "" };

            var student = StudentData.Students.FirstOrDefault(s =>
                s.RegistrationNumber == identifier);

            Courses = student != null
                ? CourseData.Courses.Where(c => student.EnrolledCourseIds.Contains(c.Id)).ToList()
                : CourseData.Courses.ToList();

            AllCourses = CourseData.Courses.ToList();

            var courseCodes = Courses.Select(c => c.Code).ToHashSet();
            Sessions = SessionReportData.Reports
                .Where(r => courseCodes.Contains(r.CourseCode)
                         && r.Status >= SessionReportStatus.HODApproved)
                .OrderByDescending(r => r.SessionDate)
                .ToList();
        }
    }
}
