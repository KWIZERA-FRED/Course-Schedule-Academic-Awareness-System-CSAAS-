using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Student
{
    [Authorize(Roles = "Student")]
    public class DashboardModel : PageModel
    {

        public User          CurrentUser  { get; private set; } = default!;
        public List<Course>  Courses      { get; private set; } = new();
        public List<Mark>    Marks        { get; private set; } = new();
        public List<Claim>   Claims       { get; private set; } = new();

        public double AverageMark      { get; private set; }
        public int    ActiveClaimCount { get; private set; }
        public int    UnreadNotifications { get; private set; }

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostSubmitClaim(
            string courseCode, string assessmentLabel,
            string lecturerName, string reason)
        {
            LoadData();

            if (string.IsNullOrWhiteSpace(reason))
            {
                StatusMessage = "Please describe the reason for your claim.";
                StatusType    = "warning";
                return RedirectToPage();
            }
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var student    = StudentData.Students.FirstOrDefault(s =>
                s.RegistrationNumber == identifier);

            var mark = MarkData.Marks.FirstOrDefault(m =>
                m.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase) &&
                m.AssessmentLabel.Equals(assessmentLabel, StringComparison.OrdinalIgnoreCase) &&
                (student == null || m.StudentId == student.Id));
            if (mark != null)
            {
                var existing = ClaimData.Claims.FirstOrDefault(c =>
                    c.MarkId == mark.Id &&
                    c.Status == ClaimStatus.UnderReview);
                if (existing != null)
                {
                    StatusMessage = $"You already have an open claim for {courseCode} {assessmentLabel}.";
                    StatusType    = "warning";
                    return RedirectToPage();
                }
            }

            var newClaim = new Claim
            {
                Id                        = ClaimData.Claims.Count > 0
                                              ? ClaimData.Claims.Max(c => c.Id) + 1 : 1,
                MarkId                    = mark?.Id ?? 0,
                StudentId                 = student?.Id ?? 0,
                StudentFullName           = CurrentUser.FullName,
                StudentRegistrationNumber = identifier,
                CourseCode                = courseCode,
                CourseTitle               = mark?.CourseTitle ?? courseCode,
                AssessmentLabel           = assessmentLabel,
                LecturerName              = lecturerName,
                Reason                    = reason.Trim(),
                Status                    = ClaimStatus.UnderReview,
                RaisedOn                  = DateTime.Now
            };

            ClaimData.Claims.Add(newClaim);

            StatusMessage = $"✓ Claim submitted for {courseCode} {assessmentLabel}. The lecturer will respond within 3 working days.";
            StatusType    = "success";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u =>
                u.Identifier == identifier || u.Email == email)
                ?? new User { FirstName = "Student", LastName = "" };

            var enrolledStudent = StudentData.Students.FirstOrDefault(s =>
                s.RegistrationNumber == identifier);

            if (enrolledStudent != null)
            {
                Courses = CourseData.Courses
                    .Where(c => enrolledStudent.EnrolledCourseIds.Contains(c.Id))
                    .ToList();
                Marks   = MarkData.Marks
                    .Where(m => m.StudentRegistrationNumber == enrolledStudent.RegistrationNumber
                             && m.Status == MarkStatus.Published)
                    .ToList();
                Claims  = ClaimData.Claims.Where(c => c.StudentId == enrolledStudent.Id).ToList();
            }
            else
            {
                Courses = CourseData.Courses.ToList();
                Marks   = MarkData.Marks.Where(m => m.StudentId == 1 && m.Status == MarkStatus.Published).ToList();
                Claims  = ClaimData.Claims.Where(c => c.StudentId == 1).ToList();
            }

            AverageMark          = Marks.Count > 0 ? Math.Round(Marks.Average(m => m.Percentage), 1) : 0;
            ActiveClaimCount     = Claims.Count(c => c.Status == ClaimStatus.UnderReview);
            UnreadNotifications  = NotificationData.Notifications
                .Count(n => n.UserEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase) && !n.IsRead);
        }
    }
}
