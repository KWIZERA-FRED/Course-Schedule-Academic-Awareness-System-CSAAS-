using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.HOD
{
    [Authorize(Roles = "HOD")]
    public class DashboardModel : PageModel
    {
        public User                  CurrentUser        { get; private set; } = default!;
        public List<Course>          Courses            { get; private set; } = new();
        public List<SessionReport>   AwaitingApproval   { get; private set; } = new();
        public List<User>            Lecturers          { get; private set; } = new();
        public List<Room>            Rooms              { get; private set; } = new();
        public List<Room>            AvailableRooms     { get; private set; } = new();
        public List<UmurongoIssue>   Issues             { get; private set; } = new();

        public int    TotalCourses      { get; private set; }
        public int    TotalLecturers    { get; private set; }
        public int    PendingApprovals  { get; private set; }
        public double DeptDeliveryRate  { get; private set; }
        public int    OpenIssues        { get; private set; }

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostApproveSession(int reportId)
        {
            var report = SessionReportData.Reports.FirstOrDefault(r => r.Id == reportId);
            if (report == null) { StatusMessage = "Session report not found."; StatusType = "danger"; return RedirectToPage(); }
            if (report.Status != SessionReportStatus.CPSignedOff) { StatusMessage = $"Report #{reportId} cannot be approved — status: {report.Status}."; StatusType = "warning"; return RedirectToPage(); }
            report.Status        = SessionReportStatus.HODApproved;
            report.HODApprovedOn = DateTime.Now;
            report.HODNotes      = $"Approved by HOD on {DateTime.Now:dd MMM yyyy HH:mm}";
            StatusMessage = "✓ Session report approved and forwarded to the Dean.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostRejectSession(int reportId, string reason)
        {
            var report = SessionReportData.Reports.FirstOrDefault(r => r.Id == reportId);
            if (report == null) { StatusMessage = "Session report not found."; StatusType = "danger"; return RedirectToPage(); }
            report.Status          = SessionReportStatus.Rejected;
            report.RejectionReason = string.IsNullOrWhiteSpace(reason) ? "Rejected by HOD." : reason;
            StatusMessage = $"✗ Session report #{reportId} rejected and returned.";
            StatusType    = "danger";
            return RedirectToPage();
        }

        public IActionResult OnPostAddCourse(string code, string title, string lecturer,
            string scheduleTime, string venue, int scheduledHours, int year, int semester, string whatsappUrl)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(title)) { StatusMessage = "Course code and title are required."; StatusType = "danger"; return RedirectToPage(); }
            if (CourseData.Courses.Any(c => c.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase))) { StatusMessage = $"Course \"{code.ToUpper()}\" already exists."; StatusType = "warning"; return RedirectToPage(); }
            if (!string.IsNullOrWhiteSpace(venue) && !string.IsNullOrWhiteSpace(scheduleTime))
            {
                var conflict = CourseData.Courses.FirstOrDefault(c => c.Venue.Equals(venue.Trim(), StringComparison.OrdinalIgnoreCase) && c.ScheduleTime.Equals(scheduleTime.Trim(), StringComparison.OrdinalIgnoreCase));
                if (conflict != null) { StatusMessage = $"⚠ Room conflict: {venue} already booked at {scheduleTime} for {conflict.Code}."; StatusType = "warning"; return RedirectToPage(); }
            }
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var hod   = UserData.Users.FirstOrDefault(u => u.Email == email);
            var newCourse = new Course
            {
                Id = CourseData.Courses.Count > 0 ? CourseData.Courses.Max(c => c.Id) + 1 : 1,
                Code = code.Trim().ToUpper(), Title = title.Trim(),
                Lecturer = lecturer?.Trim() ?? string.Empty, ScheduleTime = scheduleTime?.Trim() ?? string.Empty,
                Venue = venue?.Trim() ?? string.Empty, WhatsappGroupUrl = string.Empty,
                JoinDeadline = DateTime.Now.AddDays(14), ScheduledHours = scheduledHours > 0 ? scheduledHours : 36,
                DeliveredHours = 0, Department = hod?.Department ?? string.Empty,
                Year = year > 0 ? year : 1, Semester = semester > 0 ? semester : 1
            };
            CourseData.Courses.Add(newCourse);
            if (!string.IsNullOrWhiteSpace(venue) && !string.IsNullOrWhiteSpace(scheduleTime))
                RoomData.Rooms.FirstOrDefault(r => r.FullName.Equals(venue.Trim(), StringComparison.OrdinalIgnoreCase))?.BookedSlots.TryAdd(scheduleTime.Trim(), newCourse.Code);
            StatusMessage = $"✓ Course {newCourse.Code} – {newCourse.Title} added.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostAssignRoom(int courseId, int roomId)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            var room   = RoomData.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (course == null || room == null) { StatusMessage = "Course or room not found."; StatusType = "danger"; return RedirectToPage(); }
            if (!room.IsAvailable) { StatusMessage = $"Room {room.FullName} is not available."; StatusType = "warning"; return RedirectToPage(); }
            if (!string.IsNullOrWhiteSpace(course.ScheduleTime) && room.BookedSlots.ContainsKey(course.ScheduleTime)) { StatusMessage = $"⚠ Conflict: {room.FullName} already booked at {course.ScheduleTime}."; StatusType = "warning"; return RedirectToPage(); }
            if (!string.IsNullOrWhiteSpace(course.Venue) && !string.IsNullOrWhiteSpace(course.ScheduleTime))
                RoomData.Rooms.FirstOrDefault(r => r.FullName.Equals(course.Venue, StringComparison.OrdinalIgnoreCase))?.BookedSlots.Remove(course.ScheduleTime);
            course.Venue = room.FullName;
            if (!string.IsNullOrWhiteSpace(course.ScheduleTime)) room.BookedSlots.TryAdd(course.ScheduleTime, course.Code);
            StatusMessage = $"✓ {course.Code} assigned to {room.FullName}.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostAddLecturer(string firstName, string lastName,
            string email, string staffId, string title)
        {
            LoadData();
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
            {
                StatusMessage = "First name, last name and email are required.";
                StatusType    = "danger";
                return RedirectToPage();
            }
            if (UserData.Users.Any(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                StatusMessage = $"A user with email {email} already exists.";
                StatusType    = "warning";
                return RedirectToPage();
            }
            var hodEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var hod      = UserData.Users.FirstOrDefault(u => u.Email == hodEmail);
            var newUser  = new User
            {
                Id         = UserData.Users.Count > 0 ? UserData.Users.Max(u => u.Id) + 1 : 1,
                FirstName  = firstName.Trim(),
                LastName   = lastName.Trim(),
                Email      = email.Trim().ToLower(),
                Identifier = string.IsNullOrWhiteSpace(staffId) ? $"STAFF/{UserData.Users.Count + 1:000}" : staffId.Trim(),
                Role       = UserRole.Lecturer,
                Department = hod?.Department ?? string.Empty,
                IsActive   = true,
                PasswordHash = "lecturer123"
            };
            UserData.Users.Add(newUser);
            StatusMessage = $"✓ {title} {newUser.FullName} added as a Lecturer.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUpdateIssue(int issueId, string notes, string status)
        {
            var issue = UmurongoIssueData.Issues.FirstOrDefault(i => i.Id == issueId);
            if (issue == null) { StatusMessage = "Issue not found."; StatusType = "danger"; return RedirectToPage(); }
            if (!string.IsNullOrWhiteSpace(notes)) issue.HODNotes = notes.Trim();
            issue.Status = status switch
            {
                "InProgress" => IssueStatus.InProgress,
                "Resolved"   => IssueStatus.Resolved,
                "Escalated"  => IssueStatus.Escalated,
                _            => IssueStatus.Open
            };
            if (issue.Status == IssueStatus.Resolved) issue.ResolvedOn = DateTime.Now;
            if (issue.Status == IssueStatus.Escalated) issue.EscalatedToDean = true;
            StatusMessage = issue.Status == IssueStatus.Escalated
                ? $"⬆ Issue #{issueId} escalated to the Dean."
                : $"✓ Issue #{issueId} updated to {issue.Status}.";
            StatusType = issue.Status == IssueStatus.Escalated ? "warning" : "success";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email) ?? new User { FirstName = "Dr.", LastName = "Uwimana" };
            Courses = CourseData.Courses.Where(c => c.Department == CurrentUser.Department).ToList();
            if (!Courses.Any()) Courses = CourseData.Courses.ToList();
            Lecturers = UserData.Users.Where(u => u.Role == UserRole.Lecturer && u.Department == CurrentUser.Department).ToList();
            if (!Lecturers.Any()) Lecturers = UserData.Users.Where(u => u.Role == UserRole.Lecturer).ToList();
            AwaitingApproval = SessionReportData.Reports.Where(r => r.Status == SessionReportStatus.CPSignedOff).ToList();
            Rooms          = RoomData.Rooms.ToList();
            AvailableRooms = Rooms.Where(r => r.IsAvailable).ToList();
            Issues         = UmurongoIssueData.Issues.OrderByDescending(i => i.ReportedOn).ToList();
            TotalCourses     = Courses.Count;
            TotalLecturers   = Lecturers.Count;
            PendingApprovals = AwaitingApproval.Count;
            OpenIssues       = Issues.Count(i => i.Status == IssueStatus.Open || i.Status == IssueStatus.InProgress);
            DeptDeliveryRate = Courses.Any() ? Math.Round(Courses.Average(c => c.ScheduledHours > 0 ? (double)c.DeliveredHours / c.ScheduledHours * 100 : 0), 1) : 0;
        }
    }
}
