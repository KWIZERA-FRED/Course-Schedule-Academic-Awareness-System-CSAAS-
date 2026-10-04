using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Quality
{
    [Authorize(Roles = "DirectorOfQuality")]
    public class DashboardModel : PageModel
    {
        // ── Data exposed to the view ───────────────────────────

        public User          CurrentUser          { get; private set; } = default!;
        public List<Course>  AllCourses           { get; private set; } = new();
        public List<Course>  FlaggedCourses       { get; private set; } = new();
        public List<User>    Deans                { get; private set; } = new();
        public List<User>    HODs                 { get; private set; } = new();
        public Dictionary<string, double> DeliveryByDepartment { get; private set; } = new();

        // ── Summary stats ──────────────────────────────────────

        public int    TotalCourses           { get; private set; }
        public int    BelowThresholdCount    { get; private set; }
        public double UniversityDeliveryRate { get; private set; }

        private const double FlagThreshold = 70.0;

        // ── Feedback after POST ────────────────────────────────

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        // ══════════════════════════════════════════════════════
        //  GET
        // ══════════════════════════════════════════════════════

        public void OnGet() => LoadData();

        // ══════════════════════════════════════════════════════
        //  POST — Escalate a flagged course to the Dean
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostEscalate(int courseId, string notes)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            // Find the Dean responsible for this course's department
            var dean = UserData.Users.FirstOrDefault(u => u.Role == UserRole.Dean);

            // Log an escalation notification (in-memory — a future DB would persist this)
            var escalation = new EscalationRecord
            {
                CourseCode  = course.Code,
                CourseTitle = course.Title,
                Department  = course.Department,
                Lecturer    = course.Lecturer,
                DeliveryPct = course.ScheduledHours > 0
                    ? Math.Round((double)course.DeliveredHours / course.ScheduledHours * 100, 1)
                    : 0,
                Notes       = string.IsNullOrWhiteSpace(notes)
                    ? $"Course delivery below {FlagThreshold}% threshold. Immediate action required."
                    : notes.Trim(),
                EscalatedBy = CurrentUser.FullName,
                EscalatedTo = dean?.FullName ?? "Dean",
                EscalatedOn = DateTime.Now
            };

            EscalationData.Escalations.Add(escalation);

            StatusMessage = $"✓ {course.Code} escalated to {escalation.EscalatedTo}. They will be notified to take action.";
            StatusType    = "success";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  Private helpers
        // ══════════════════════════════════════════════════════

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "Dr.", LastName = "Ingabire" };

            AllCourses = CourseData.Courses.ToList();
            FlaggedCourses = AllCourses
                .Where(c => c.ScheduledHours > 0 &&
                            (double)c.DeliveredHours / c.ScheduledHours * 100 < FlagThreshold)
                .OrderBy(c => c.DeliveredHours).ToList();

            Deans = UserData.Users.Where(u => u.Role == UserRole.Dean).ToList();
            HODs  = UserData.Users.Where(u => u.Role == UserRole.HOD).ToList();

            DeliveryByDepartment = AllCourses
                .GroupBy(c => c.Department)
                .ToDictionary(g => g.Key,
                    g => g.Average(c => c.ScheduledHours > 0
                        ? Math.Round((double)c.DeliveredHours / c.ScheduledHours * 100, 1) : 0));

            TotalCourses           = AllCourses.Count;
            BelowThresholdCount    = FlaggedCourses.Count;
            UniversityDeliveryRate = AllCourses.Any()
                ? Math.Round(AllCourses.Average(c => c.ScheduledHours > 0
                    ? (double)c.DeliveredHours / c.ScheduledHours * 100 : 0), 1) : 0;
        }
    }
}
