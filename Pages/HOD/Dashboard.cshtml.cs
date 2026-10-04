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
        // ── Read-only data exposed to the view ─────────────────

        public User                CurrentUser         { get; private set; } = default!;
        public List<Course>        Courses             { get; private set; } = new();
        public List<SessionReport> AwaitingApproval    { get; private set; } = new();
        public List<User>          Lecturers           { get; private set; } = new();
        public List<Room>          Rooms               { get; private set; } = new();
        public List<Room>          AvailableRooms      { get; private set; } = new();

        // ── Summary stats ──────────────────────────────────────

        public int    TotalCourses      { get; private set; }
        public int    TotalLecturers    { get; private set; }
        public int    PendingApprovals  { get; private set; }
        public double DeptDeliveryRate  { get; private set; }

        // ── Status message shown after a POST ──────────────────

        [TempData] public string? StatusMessage  { get; set; }
        [TempData] public string? StatusType     { get; set; }   // "success" | "danger" | "warning"

        // ══════════════════════════════════════════════════════
        //  GET
        // ══════════════════════════════════════════════════════

        public void OnGet()
        {
            LoadData();
        }

        // ══════════════════════════════════════════════════════
        //  POST — Approve session report
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

            if (report.Status != SessionReportStatus.CPSignedOff)
            {
                StatusMessage = $"Report #{reportId} cannot be approved — current status: {report.Status}.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            report.Status       = SessionReportStatus.HODApproved;
            report.HODApprovedOn = DateTime.Now;
            report.HODNotes     = $"Approved by HOD on {DateTime.Now:dd MMM yyyy HH:mm}";

            StatusMessage = $"✓ Session report approved and forwarded to the Dean.";
            StatusType    = "success";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  POST — Reject session report
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
                                        ? "Rejected by HOD."
                                        : reason;

            StatusMessage = $"✗ Session report #{reportId} has been rejected and returned.";
            StatusType    = "danger";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  POST — Add new course
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostAddCourse(
            string code, string title, string lecturer,
            string scheduleTime, string venue, int scheduledHours,
            int year, int semester, string whatsappUrl)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(title))
            {
                StatusMessage = "Course code and title are required.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            // Check duplicate code
            if (CourseData.Courses.Any(c =>
                    c.Code.Equals(code.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                StatusMessage = $"A course with code \"{code.ToUpper()}\" already exists.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            // Check room conflict: same venue + same time slot already booked
            if (!string.IsNullOrWhiteSpace(venue) && !string.IsNullOrWhiteSpace(scheduleTime))
            {
                var conflict = CourseData.Courses.FirstOrDefault(c =>
                    c.Venue.Equals(venue.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    c.ScheduleTime.Equals(scheduleTime.Trim(), StringComparison.OrdinalIgnoreCase));

                if (conflict != null)
                {
                    StatusMessage = $"⚠ Room conflict: {venue} is already booked at {scheduleTime} for {conflict.Code} – {conflict.Title}.";
                    StatusType    = "warning";
                    return RedirectToPage();
                }
            }

            // Get HOD department
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var hod   = UserData.Users.FirstOrDefault(u => u.Email == email);

            var newCourse = new Course
            {
                Id               = CourseData.Courses.Count > 0
                                      ? CourseData.Courses.Max(c => c.Id) + 1
                                      : 1,
                Code             = code.Trim().ToUpper(),
                Title            = title.Trim(),
                Lecturer         = lecturer?.Trim() ?? string.Empty,
                ScheduleTime     = scheduleTime?.Trim() ?? string.Empty,
                Venue            = venue?.Trim() ?? string.Empty,
                WhatsappGroupUrl = whatsappUrl?.Trim() ?? string.Empty,
                JoinDeadline     = DateTime.Now.AddDays(14),
                ScheduledHours   = scheduledHours > 0 ? scheduledHours : 36,
                DeliveredHours   = 0,
                Department       = hod?.Department ?? string.Empty,
                Year             = year > 0 ? year : 1,
                Semester         = semester > 0 ? semester : 1
            };

            CourseData.Courses.Add(newCourse);

            // Also mark the room as booked for this slot
            if (!string.IsNullOrWhiteSpace(venue) && !string.IsNullOrWhiteSpace(scheduleTime))
            {
                var room = RoomData.Rooms.FirstOrDefault(r =>
                    r.FullName.Equals(venue.Trim(), StringComparison.OrdinalIgnoreCase));
                room?.BookedSlots.TryAdd(scheduleTime.Trim(), newCourse.Code);
            }

            StatusMessage = $"✓ Course {newCourse.Code} – {newCourse.Title} added successfully.";
            StatusType    = "success";
            return RedirectToPage();
        }

        // ══════════════════════════════════════════════════════
        //  POST — Assign room to an existing course
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostAssignRoom(int courseId, int roomId)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            var room   = RoomData.Rooms.FirstOrDefault(r => r.Id == roomId);

            if (course == null || room == null)
            {
                StatusMessage = "Course or room not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (!room.IsAvailable)
            {
                StatusMessage = $"Room {room.FullName} is not available for scheduling.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            // Check time conflict in this room
            if (!string.IsNullOrWhiteSpace(course.ScheduleTime) &&
                room.BookedSlots.ContainsKey(course.ScheduleTime))
            {
                var bookedBy = room.BookedSlots[course.ScheduleTime];
                StatusMessage = $"⚠ Conflict: {room.FullName} is already booked at {course.ScheduleTime} for {bookedBy}.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            // Free old room slot if course was in another room
            if (!string.IsNullOrWhiteSpace(course.Venue) &&
                !string.IsNullOrWhiteSpace(course.ScheduleTime))
            {
                var oldRoom = RoomData.Rooms.FirstOrDefault(r =>
                    r.FullName.Equals(course.Venue, StringComparison.OrdinalIgnoreCase));
                oldRoom?.BookedSlots.Remove(course.ScheduleTime);
            }

            // Assign
            course.Venue = room.FullName;
            if (!string.IsNullOrWhiteSpace(course.ScheduleTime))
                room.BookedSlots.TryAdd(course.ScheduleTime, course.Code);

            StatusMessage = $"✓ {course.Code} assigned to {room.FullName}.";
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
                ?? new User { FirstName = "Dr.", LastName = "Uwimana" };

            Courses = CourseData.Courses
                .Where(c => c.Department == CurrentUser.Department)
                .ToList();
            if (!Courses.Any()) Courses = CourseData.Courses.ToList();

            Lecturers = UserData.Users
                .Where(u => u.Role == UserRole.Lecturer && u.Department == CurrentUser.Department)
                .ToList();
            if (!Lecturers.Any())
                Lecturers = UserData.Users.Where(u => u.Role == UserRole.Lecturer).ToList();

            AwaitingApproval = SessionReportData.Reports
                .Where(r => r.Status == SessionReportStatus.CPSignedOff)
                .ToList();

            Rooms          = RoomData.Rooms.ToList();
            AvailableRooms = Rooms.Where(r => r.IsAvailable).ToList();

            TotalCourses     = Courses.Count;
            TotalLecturers   = Lecturers.Count;
            PendingApprovals = AwaitingApproval.Count;

            DeptDeliveryRate = Courses.Any()
                ? Math.Round(Courses.Average(c =>
                    c.ScheduledHours > 0
                        ? (double)c.DeliveredHours / c.ScheduledHours * 100
                        : 0), 1)
                : 0;
        }
    }
}
