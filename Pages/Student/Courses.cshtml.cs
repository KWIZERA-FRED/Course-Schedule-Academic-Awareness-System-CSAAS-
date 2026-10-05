using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class CoursesModel : PageModel
    {
        public User          CurrentUser         { get; private set; } = default!;
        public List<Course>  Enrolled            { get; private set; } = new();
        public List<Course>  Ongoing             { get; private set; } = new();
        public List<Course>  Completed           { get; private set; } = new();
        public List<Course>  NotStarted          { get; private set; } = new();
        public List<Course>  AllCourses          { get; private set; } = new();
        public int           UnreadNotifications { get; private set; }

        [TempData] public string? StatusMessage  { get; set; }
        [TempData] public string? StatusType     { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostEnroll(int courseId)
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var student    = StudentData.Students.FirstOrDefault(s => s.RegistrationNumber == identifier);

            if (student == null)
            {
                StatusMessage = "Your student record was not found. Contact the administrator.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (student.EnrolledCourseIds.Contains(courseId))
            {
                StatusMessage = $"You are already enrolled in {course.Code} – {course.Title}.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            student.EnrolledCourseIds.Add(courseId);

            StatusMessage = $"✓ Successfully enrolled in {course.Code} – {course.Title}.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUnenroll(int courseId)
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var student    = StudentData.Students.FirstOrDefault(s => s.RegistrationNumber == identifier);

            if (student == null)
            {
                StatusMessage = "Your student record was not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (!student.EnrolledCourseIds.Contains(courseId))
            {
                StatusMessage = $"You are not enrolled in {course.Code}.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            student.EnrolledCourseIds.Remove(courseId);

            StatusMessage = $"You have unenrolled from {course.Code} – {course.Title}.";
            StatusType    = "warning";
            return RedirectToPage();
        }

        private void LoadData()
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

            Ongoing    = Enrolled.Where(c => c.DeliveredHours > 0 && c.DeliveredHours < c.ScheduledHours).ToList();
            Completed  = Enrolled.Where(c => c.ScheduledHours > 0 && c.DeliveredHours >= c.ScheduledHours).ToList();
            NotStarted = Enrolled.Where(c => c.DeliveredHours == 0).ToList();

            UnreadNotifications = NotificationData.Notifications
                .Count(n => n.UserEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase) && !n.IsRead);
        }
    }
}
