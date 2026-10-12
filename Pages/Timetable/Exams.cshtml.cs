using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Timetable
{
    [Authorize]
    public class ExamsModel : PageModel
    {
        public User                  CurrentUser    { get; private set; } = default!;
        public List<ExamTimetable>   RegularExams   { get; private set; } = new();
        public List<ExamTimetable>   SpecialExams   { get; private set; } = new();
        public List<ExamTimetable>   AllExams       { get; private set; } = new();
        public string                RoleLabel      { get; private set; } = string.Empty;
        public string                DashboardUrl   { get; private set; } = "/";
        public bool                  IsHOD          { get; private set; }

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostAddExam(
            string courseCode, string courseTitle, string department,
            int year, int semester, string examType,
            DateTime examDate, string startTime, string endTime,
            string venue, string invigilator, string notes)
        {
            LoadData();
            if (string.IsNullOrWhiteSpace(courseCode) || examDate == default)
            {
                StatusMessage = "Course code and exam date are required.";
                StatusType    = "warning";
                return RedirectToPage();
            }
            var type = examType switch
            {
                "Special"       => ExamType.Special,
                "Supplementary" => ExamType.Supplementary,
                "Retake"        => ExamType.Retake,
                _               => ExamType.Regular
            };
            ExamData.Exams.Add(new ExamTimetable
            {
                Id          = ExamData.Exams.Count > 0 ? ExamData.Exams.Max(e => e.Id) + 1 : 1,
                CourseCode  = courseCode.Trim().ToUpper(),
                CourseTitle = courseTitle.Trim(),
                Department  = department.Trim(),
                Year        = year, Semester = semester,
                Type        = type,
                ExamDate    = examDate,
                StartTime   = startTime,
                EndTime     = endTime,
                Venue       = venue.Trim(),
                Invigilator = invigilator.Trim(),
                Notes       = notes?.Trim() ?? string.Empty,
                IsPublished = false,
                CreatedOn   = DateTime.Now
            });
            StatusMessage = $"✓ Exam added for {courseCode.ToUpper()}. Publish it when ready.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostPublishExam(int examId)
        {
            var exam = ExamData.Exams.FirstOrDefault(e => e.Id == examId);
            if (exam == null) { StatusMessage = "Exam not found."; StatusType = "danger"; return RedirectToPage(); }
            exam.IsPublished  = true;
            exam.PublishedOn  = DateTime.Now;
            StatusMessage = $"✓ {exam.CourseCode} {exam.Type} exam published. Students can now see it.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUnpublishExam(int examId)
        {
            var exam = ExamData.Exams.FirstOrDefault(e => e.Id == examId);
            if (exam == null) { StatusMessage = "Exam not found."; StatusType = "danger"; return RedirectToPage(); }
            exam.IsPublished  = false;
            exam.PublishedOn  = null;
            StatusMessage = $"{exam.CourseCode} exam unpublished (hidden from students).";
            StatusType    = "warning";
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteExam(int examId)
        {
            var exam = ExamData.Exams.FirstOrDefault(e => e.Id == examId);
            if (exam == null) { StatusMessage = "Exam not found."; StatusType = "danger"; return RedirectToPage(); }
            ExamData.Exams.Remove(exam);
            StatusMessage = $"Exam for {exam.CourseCode} deleted.";
            StatusType    = "warning";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email      = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            var identifier = User.FindFirst("Identifier")?.Value ?? string.Empty;
            var roleStr    = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? string.Empty;

            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email || u.Identifier == identifier)
                ?? new User { FirstName = "User", LastName = "" };

            IsHOD = roleStr == "HOD";

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

            // HOD sees all exams in their department; students/others see only published
            var source = IsHOD
                ? ExamData.Exams.Where(e => e.Department == CurrentUser.Department ||
                      string.IsNullOrEmpty(e.Department)).ToList()
                : ExamData.Exams.Where(e => e.IsPublished).ToList();

            if (!source.Any() && !IsHOD)
                source = ExamData.Exams.Where(e => e.IsPublished).ToList();

            AllExams     = source.OrderBy(e => e.ExamDate).ThenBy(e => e.StartTime).ToList();
            RegularExams = AllExams.Where(e => e.Type == ExamType.Regular).ToList();
            SpecialExams = AllExams.Where(e => e.Type != ExamType.Regular).ToList();
        }
    }
}
