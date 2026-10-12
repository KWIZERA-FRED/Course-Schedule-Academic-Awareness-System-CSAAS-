using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Timetable
{
    [Authorize]
    public class ReportModel : PageModel
    {
        public User          CurrentUser  { get; private set; } = default!;
        public List<Course>  Courses      { get; private set; } = new();
        public string        FilterBy     { get; private set; } = "all";
        public string        RoleLabel    { get; private set; } = string.Empty;
        public string        DashboardUrl { get; private set; } = "/";

        // Timetable grid: Day -> TimeSlot -> Course (for visual grid view)
        public Dictionary<string, Dictionary<string, Course?>> TimetableGrid { get; private set; } = new();

        public static readonly string[] Days      = { "Monday","Tuesday","Wednesday","Thursday","Friday" };
        public static readonly string[] TimeSlots = { "08:00 - 10:00","10:00 - 12:00","13:00 - 15:00","15:00 - 17:00" };

        public void OnGet(string filter = "all")
        {
            FilterBy = filter;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var roleStr    = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email || u.Identifier == identifier)
                ?? new User { FirstName = "User", LastName = "" };

            // Set dashboard URL by role
            DashboardUrl = roleStr switch
            {
                "ClassRepresentative" => "/CP/Dashboard",
                "Lecturer"            => "/Lecturer/Dashboard",
                "HOD"                 => "/HOD/Dashboard",
                "Dean"                => "/Dean/Dashboard",
                "DirectorOfQuality"   => "/Quality/Dashboard",
                _                     => "/Student/Dashboard"
            };

            RoleLabel = roleStr switch
            {
                "ClassRepresentative" => "Class Representative",
                "Lecturer"            => "Lecturer",
                "HOD"                 => "Head of Department",
                "Dean"                => "Dean",
                "DirectorOfQuality"   => "Director of Quality",
                _                     => "Student"
            };

            // Filter courses based on role and filter param
            var all = CourseData.Courses.ToList();

            Courses = (filter, roleStr) switch
            {
                ("mine", "Lecturer") => all.Where(c =>
                    c.Lecturer.Contains(CurrentUser.LastName, StringComparison.OrdinalIgnoreCase)).ToList(),
                ("mine", "Student") => all.Where(c =>
                {
                    var student = StudentData.Students.FirstOrDefault(s => s.RegistrationNumber == identifier);
                    return student != null && student.EnrolledCourseIds.Contains(c.Id);
                }).ToList(),
                ("dept", _) => all.Where(c =>
                    c.Department.Equals(CurrentUser.Department, StringComparison.OrdinalIgnoreCase)).ToList(),
                _ => all
            };

            Courses = Courses.OrderBy(c => c.ScheduleTime).ThenBy(c => c.Code).ToList();

            // Build timetable grid
            foreach (var day in Days)
            {
                TimetableGrid[day] = new Dictionary<string, Course?>();
                foreach (var slot in TimeSlots)
                {
                    var slotKey = $"{day[..3]} {slot}";
                    TimetableGrid[day][slot] = Courses.FirstOrDefault(c =>
                        c.ScheduleTime.Equals(slotKey, StringComparison.OrdinalIgnoreCase));
                }
            }
        }
    }
}
