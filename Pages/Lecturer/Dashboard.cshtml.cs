using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;
using StudentModel = CourseScheduleSystem.Web.Models.Student;

namespace CourseScheduleSystem.Web.Pages.Lecturer
{
    [Authorize(Roles = "Lecturer")]
    public class DashboardModel : PageModel
    {

        public User                    CurrentUser       { get; private set; } = default!;
        public List<Course>            Courses           { get; private set; } = new();
        public List<Mark>              Marks             { get; private set; } = new();
        public List<Mark>              DraftMarks        { get; private set; } = new();
        public List<Claim>             PendingClaims     { get; private set; } = new();
        public List<SessionReport>     SessionReports    { get; private set; } = new();
        public List<StudentModel>      Students          { get; private set; } = new();

        public int    TotalStudents       { get; private set; }
        public int    PendingSubmissions  { get; private set; }
        public double TotalDeliveredHours { get; private set; }

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostSubmitSessionReport(
            int courseId, string topicCovered, double durationHours)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                StatusMessage = "Course not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (string.IsNullOrWhiteSpace(topicCovered))
            {
                StatusMessage = "Please enter the topic covered in this session.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            var newReport = new SessionReport
            {
                Id          = SessionReportData.Reports.Count > 0
                                ? SessionReportData.Reports.Max(r => r.Id) + 1 : 1,
                CourseId    = course.Id,
                CourseCode  = course.Code,
                CourseTitle = course.Title,
                LecturerName= CurrentUser.FullName,
                SessionDate = DateTime.Now,
                DurationHours = durationHours > 0 ? durationHours : 2,
                TopicCovered = topicCovered.Trim(),
                Venue        = course.Venue,
                Status       = SessionReportStatus.Submitted,
                SubmittedOn  = DateTime.Now,
                ClassRepresentativeName = "Class Representative"
            };

            SessionReportData.Reports.Add(newReport);

            StatusMessage = $"✓ Session report for {course.Code} submitted. Awaiting CP sign-off.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostSaveMark(
            int courseId, string assessmentType, string studentRegNo,
            int maxScore, double score, string remarks, bool publishNow)
        {
            var course = CourseData.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null || string.IsNullOrWhiteSpace(studentRegNo))
            {
                StatusMessage = "Course or student registration number is missing.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            var student = StudentData.Students
                .FirstOrDefault(s => s.RegistrationNumber.Equals(
                    studentRegNo.Trim(), StringComparison.OrdinalIgnoreCase));

            var assessEnum = assessmentType switch
            {
                "CAT1"        => AssessmentType.CAT1,
                "CAT2"        => AssessmentType.CAT2,
                "Assignment1" => AssessmentType.Assignment1,
                "Assignment2" => AssessmentType.Assignment2,
                "Practical"   => AssessmentType.Practical,
                "MidSemester" => AssessmentType.MidSemester,
                "FinalExam"   => AssessmentType.FinalExam,
                _             => AssessmentType.CAT1
            };

            var newMark = new Mark
            {
                Id                        = MarkData.Marks.Count > 0
                                              ? MarkData.Marks.Max(m => m.Id) + 1 : 1,
                CourseId                  = course.Id,
                CourseCode                = course.Code,
                CourseTitle               = course.Title,
                StudentId                 = student?.Id ?? 0,
                StudentRegistrationNumber = studentRegNo.Trim(),
                StudentFullName           = student?.FullName ?? studentRegNo,
                LecturerName              = CurrentUser.FullName,
                AssessmentType            = assessEnum,
                MaxScore                  = maxScore > 0 ? maxScore : 30,
                Score                     = score,
                Remarks                   = remarks?.Trim() ?? string.Empty,
                Status                    = publishNow ? MarkStatus.Published : MarkStatus.Draft,
                CreatedOn                 = DateTime.Now,
                PublishedOn               = publishNow ? DateTime.Now : null
            };

            MarkData.Marks.Add(newMark);

            StatusMessage = publishNow
                ? $"✓ Mark for {studentRegNo} published. Student can view it now."
                : $"✓ Mark for {studentRegNo} saved as draft. Publish when ready.";
            StatusType = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostPublishMark(int markId)
        {
            var mark = MarkData.Marks.FirstOrDefault(m => m.Id == markId);
            if (mark == null)
            {
                StatusMessage = "Mark not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            mark.Status      = MarkStatus.Published;
            mark.PublishedOn = DateTime.Now;

            StatusMessage = $"✓ Mark for {mark.StudentFullName} ({mark.AssessmentLabel}) published.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUnpublishMark(int markId)
        {
            var mark = MarkData.Marks.FirstOrDefault(m => m.Id == markId);
            if (mark == null)
            {
                StatusMessage = "Mark not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            mark.Status      = MarkStatus.Draft;
            mark.PublishedOn = null;

            StatusMessage = $"Mark for {mark.StudentFullName} unpublished (reverted to draft).";
            StatusType    = "warning";
            return RedirectToPage();
        }

        public IActionResult OnPostRespondClaim(
            int claimId, string decision, double? correctedScore, string explanation)
        {
            var claim = ClaimData.Claims.FirstOrDefault(c => c.Id == claimId);
            if (claim == null)
            {
                StatusMessage = "Claim not found.";
                StatusType    = "danger";
                return RedirectToPage();
            }

            if (string.IsNullOrWhiteSpace(explanation))
            {
                StatusMessage = "Please provide an explanation to the student.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            claim.Status           = decision switch
            {
                "upheld"   => ClaimStatus.Upheld,
                "partial"  => ClaimStatus.PartiallyUpheld,
                _          => ClaimStatus.Rejected
            };
            claim.LecturerResponse = explanation.Trim();
            claim.CorrectedScore   = correctedScore;
            claim.RespondedOn      = DateTime.Now;
            if (claim.Status != ClaimStatus.Rejected && correctedScore.HasValue)
            {
                var mark = MarkData.Marks.FirstOrDefault(m => m.Id == claim.MarkId);
                if (mark != null)
                {
                    mark.Score  = correctedScore.Value;
                    mark.Status = MarkStatus.Corrected;
                }
            }

            StatusMessage = claim.Status == ClaimStatus.Rejected
                ? $"Response sent to {claim.StudentFullName}. Claim rejected."
                : $"✓ Response sent to {claim.StudentFullName}. Mark updated.";
            StatusType = claim.Status == ClaimStatus.Rejected ? "warning" : "success";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                        ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "Dr.", LastName = "Mugisha" };

            Courses = CourseData.Courses
                .Where(c => c.Lecturer.Contains(CurrentUser.LastName, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (!Courses.Any()) Courses = CourseData.Courses.ToList();

            var courseIds = Courses.Select(c => c.Id).ToList();

            Marks = MarkData.Marks
                .Where(m => m.LecturerName.Contains(CurrentUser.LastName, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (!Marks.Any()) Marks = MarkData.Marks.ToList();

            DraftMarks    = Marks.Where(m => m.Status == MarkStatus.Draft).ToList();
            PendingClaims = ClaimData.Claims.Where(c => c.Status == ClaimStatus.UnderReview).ToList();

            SessionReports = SessionReportData.Reports
                .Where(r => r.LecturerName.Contains(CurrentUser.LastName, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (!SessionReports.Any()) SessionReports = SessionReportData.Reports.ToList();

            Students = StudentData.Students
                .Where(s => s.EnrolledCourseIds.Any(id => courseIds.Contains(id)))
                .ToList<StudentModel>();

            TotalStudents       = Students.Select(s => s.Id).Distinct().Count();
            PendingSubmissions  = SessionReports.Count(r => r.Status == SessionReportStatus.Draft);
            TotalDeliveredHours = Courses.Sum(c => c.DeliveredHours);
        }
    }
}
