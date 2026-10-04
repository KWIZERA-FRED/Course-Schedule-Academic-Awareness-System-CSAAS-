using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Dean
{
    [Authorize(Roles = "Dean")]
    public class DashboardModel : PageModel
    {
        // ── Data exposed to the view ───────────────────────────

        public User                CurrentUser           { get; private set; } = default!;
        public List<Course>        AllCourses            { get; private set; } = new();
        public List<SessionReport> SessionReports        { get; private set; } = new();
        public List<SessionReport> AwaitingDeanApproval  { get; private set; } = new();
        public List<User>          HODs                  { get; private set; } = new();
        public Dictionary<string, double> DeliveryByDepartment { get; private set; } = new();

        // ── Summary stats ──────────────────────────────────────

        public int    TotalDepartments      { get; private set; }
        public int    TotalCourses          { get; private set; }
        public int    PendingFinalApprovals { get; private set; }
        public double FacultyDeliveryRate   { get; private set; }

        // ── Feedback after POST ────────────────────────────────

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        // ══════════════════════════════════════════════════════
        //  GET
        // ══════════════════════════════════════════════════════

        public void OnGet() => LoadData();

        // ══════════════════════════════════════════════════════
        //  POST — Final approve a session report
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostApproveSession(int reportId)
        {
            var report = SessionReportData.Reports.FirstOrDefault(r => r.Id == reportId);

            if (report == null)
            {
                StatusMessage = "Session report not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (report.Status != SessionReportStatus.HODApproved)
            {
                StatusMessage = $"Report #{reportId} is not at HOD-approved stage.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            report.Status        = SessionReportStatus.DeanApproved;
            report.DeanApprovedOn = DateTime.Now;
            report.DeanNotes     = $"Final approval by Dean on {DateTime.Now:dd MMM yyyy HH:mm}";

            StatusMessage = $"✓ Session report for {report.CourseCode} approved. Fully verified.";
            StatusType    = "success";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  POST — Reject / return a session report
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostRejectSession(int reportId, string reason)
        {
            var report = SessionReportData.Reports.FirstOrDefault(r => r.Id == reportId);

            if (report == null)
            {
                StatusMessage = "Session report not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            report.Status          = SessionReportStatus.Rejected;
            report.RejectionReason = string.IsNullOrWhiteSpace(reason)
                                        ? "Returned by Dean for revision."
                                        : reason;

            StatusMessage = $"✗ Report for {report.CourseCode} returned to HOD for revision.";
            StatusType    = "danger";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  Private helpers
        // ══════════════════════════════════════════════════════

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "Prof.", LastName = "Ndayisaba" };

            AllCourses    = CourseData.Courses.ToList();
            HODs          = UserData.Users.Where(u => u.Role == UserRole.HOD).ToList();
            SessionReports = SessionReportData.Reports.ToList();

            AwaitingDeanApproval = SessionReports
                .Where(r => r.Status == SessionReportStatus.HODApproved).ToList();

            DeliveryByDepartment = AllCourses
                .GroupBy(c => c.Department)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(c => c.ScheduledHours > 0
                        ? Math.Round((double)c.DeliveredHours / c.ScheduledHours * 100, 1) : 0));

            TotalDepartments      = DeliveryByDepartment.Count;
            TotalCourses          = AllCourses.Count;
            PendingFinalApprovals = AwaitingDeanApproval.Count;
            FacultyDeliveryRate   = AllCourses.Any()
                ? Math.Round(AllCourses.Average(c => c.ScheduledHours > 0
                    ? (double)c.DeliveredHours / c.ScheduledHours * 100 : 0), 1) : 0;
        }
    }
}
