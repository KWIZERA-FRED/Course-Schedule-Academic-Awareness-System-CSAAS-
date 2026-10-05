using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class GroupsModel : PageModel
    {
        public User          CurrentUser  { get; private set; } = default!;
        public List<Course>  OpenGroups   { get; private set; } = new();
        public List<Course>  ClosedGroups { get; private set; } = new();
        public List<Course>  NoGroup      { get; private set; } = new();

        public void OnGet()
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u =>
                u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "Student", LastName = "" };

            var student = StudentData.Students.FirstOrDefault(s =>
                s.RegistrationNumber == identifier);

            var courses = student != null
                ? CourseData.Courses.Where(c => student.EnrolledCourseIds.Contains(c.Id)).ToList()
                : CourseData.Courses.ToList();

            OpenGroups   = courses.Where(c => !string.IsNullOrEmpty(c.WhatsappGroupUrl) && c.JoinDeadline >= DateTime.Now).ToList();
            ClosedGroups = courses.Where(c => !string.IsNullOrEmpty(c.WhatsappGroupUrl) && c.JoinDeadline < DateTime.Now).ToList();
            NoGroup      = courses.Where(c => string.IsNullOrEmpty(c.WhatsappGroupUrl)).ToList();
        }
    }
}
