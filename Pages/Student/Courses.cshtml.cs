using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class CoursesModel : PageModel
    {
        public User          CurrentUser    { get; private set; } = default!;
        public List<Course>  Enrolled       { get; private set; } = new();
        public List<Course>  Ongoing        { get; private set; } = new();
        public List<Course>  Completed      { get; private set; } = new();
        public List<Course>  NotStarted     { get; private set; } = new();
        public List<Course>  AllCourses     { get; private set; } = new();
        public int           UnreadNotifications { get; private set; }

        public void OnGet()
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u =>
                u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "Student", LastName = "" };

            var student = StudentData.Students.FirstOrDefault(s =>
                s.RegistrationNumber == identifier);

            Enrolled = student != null
                ? CourseData.Courses.Where(c => student.EnrolledCourseIds.Contains(c.Id)).ToList()
                : CourseData.Courses.ToList();

            AllCourses = CourseData.Courses.OrderBy(c => c.Department).ThenBy(c => c.Code).ToList();

            // Classify enrolled courses by delivery status
            Ongoing    = Enrolled.Where(c => c.DeliveredHours > 0 && c.DeliveredHours < c.ScheduledHours).ToList();
            Completed  = Enrolled.Where(c => c.ScheduledHours > 0 && c.DeliveredHours >= c.ScheduledHours).ToList();
            NotStarted = Enrolled.Where(c => c.DeliveredHours == 0).ToList();

            UnreadNotifications = NotificationData.Notifications
                .Count(n => n.UserEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase) && !n.IsRead);
        }
    }
}
