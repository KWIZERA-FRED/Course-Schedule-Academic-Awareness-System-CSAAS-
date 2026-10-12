using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.HOD
{
    [Authorize(Roles = "HOD")]
    public class TimetableHistoryModel : PageModel
    {
        public User                     CurrentUser { get; private set; } = default!;
        public List<TimetableRevision>  Revisions   { get; private set; } = new();
        public List<Course>             Courses     { get; private set; } = new();

        // Summary stats
        public int TotalChanges   { get; private set; }
        public int TodayChanges   { get; private set; }
        public int ThisWeekChanges{ get; private set; }

        public void OnGet()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                ?? new User { FirstName = "Dr.", LastName = "Uwimana" };

            Courses   = CourseData.Courses
                .Where(c => c.Department == CurrentUser.Department)
                .ToList();
            if (!Courses.Any()) Courses = CourseData.Courses.ToList();

            // Get revisions for courses in this department
            var deptCodes = Courses.Select(c => c.Code).ToHashSet();
            Revisions = TimetableRevisionData.Revisions
                .Where(r => deptCodes.Contains(r.CourseCode))
                .OrderByDescending(r => r.ChangedOn)
                .ToList();

            // If no real revisions yet, add sample history so the page isn't empty
            if (!Revisions.Any())
            {
                foreach (var c in Courses.Take(3))
                {
                    TimetableRevisionData.Record(c.Id, c.Code, c.Title,
                        "Created", "", $"{c.ScheduleTime} @ {c.Venue}",
                        CurrentUser.FullName, "HOD", "Initial course setup");
                }
                Revisions = TimetableRevisionData.Revisions
                    .OrderByDescending(r => r.ChangedOn).ToList();
            }

            TotalChanges    = Revisions.Count;
            TodayChanges    = Revisions.Count(r => r.ChangedOn.Date == DateTime.Today);
            ThisWeekChanges = Revisions.Count(r => r.ChangedOn >= DateTime.Now.AddDays(-7));
        }
    }
}
