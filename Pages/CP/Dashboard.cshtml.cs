using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.CP
{
    [Authorize(Roles = "ClassRepresentative")]
    public class DashboardModel : PageModel
    {

        public User                CurrentUser      { get; private set; } = default!;
        public List<Course>        Courses          { get; private set; } = new();
        public List<SessionReport> SessionReports   { get; private set; } = new();
        public List<SessionReport> PendingSignOffs  { get; private set; } = new();

        public int TotalSignedOff    { get; private set; }
        public int ActiveGroupCount  { get; private set; }
        public int OpenGroupCount    { get; private set; }
        public int ClosedGroupCount  { get; private set; }
        public List<User> AllCPs     { get; private set; } = new();

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostSignOff(int reportId, string notes)
        {
            var report = SessionReportData.Reports.FirstOrDefault(r => r.Id == reportId);

            if (report == null)
            {
                StatusMessage = "Session report not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (report.Status != SessionReportStatus.Submitted)
            {
                StatusMessage = $"Report #{reportId} is not in Submitted status — cannot sign off.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            report.Status        = SessionReportStatus.CPSignedOff;
            report.CPSignedOffOn = DateTime.Now;
            report.CPNotes       = string.IsNullOrWhiteSpace(notes)
                                    ? $"Signed off by CP on {DateTime.Now:dd MMM yyyy HH:mm}"
                                    : notes;

            StatusMessage = $"✓ Session report signed off and forwarded to the HOD.";
            StatusType    = "success";
            return RedirectToPage();
        }

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
                                        ? "Rejected by Class Representative."
                                        : reason;

            StatusMessage = $"✗ Session report #{reportId} rejected and returned to lecturer.";
            StatusType    = "danger";
            return RedirectToPage();
        }

        public IActionResult OnPostCreateGroup(int courseId, string groupUrl, int deadlineDays)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (string.IsNullOrWhiteSpace(groupUrl))
            {
                StatusMessage = "Please provide a valid WhatsApp group link.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            course.WhatsappGroupUrl = groupUrl.Trim();
            course.JoinDeadline     = DateTime.Now.AddDays(deadlineDays > 0 ? deadlineDays : 14);

            StatusMessage = $"✓ WhatsApp group link set for {course.Code} – {course.Title}. Deadline: {course.JoinDeadline:dd MMM yyyy}.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUpdateGroup(int courseId, string groupUrl, int deadlineDays)
        {
            return OnPostCreateGroup(courseId, groupUrl, deadlineDays);
        }

        public IActionResult OnPostCloseGroup(int courseId)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            course.JoinDeadline = DateTime.Now.AddDays(-1);   // expire immediately

            StatusMessage = $"✓ WhatsApp group for {course.Code} has been closed. Students can no longer join.";
            StatusType    = "success";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                ?? new User { FirstName = "Class Rep", LastName = "" };

            Courses         = CourseData.Courses.ToList();
            SessionReports  = SessionReportData.Reports.ToList();

            PendingSignOffs = SessionReports
                .Where(r => r.Status == SessionReportStatus.Submitted)
                .ToList();

            TotalSignedOff  = SessionReports.Count(r => r.Status >= SessionReportStatus.CPSignedOff);
            ActiveGroupCount = Courses.Count(c => !string.IsNullOrEmpty(c.WhatsappGroupUrl));
            OpenGroupCount   = Courses.Count(c => !string.IsNullOrEmpty(c.WhatsappGroupUrl) && c.JoinDeadline >= DateTime.Now);
            ClosedGroupCount = Courses.Count(c => !string.IsNullOrEmpty(c.WhatsappGroupUrl) && c.JoinDeadline < DateTime.Now);

            AllCPs = UserData.Users
                .Where(u => u.Role == UserRole.ClassRepresentative &&
                            u.Department == CurrentUser.Department)
                .OrderBy(u => u.Identifier)
                .ToList();
        }
    }
}
