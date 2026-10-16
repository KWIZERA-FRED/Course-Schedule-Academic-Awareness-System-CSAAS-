using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.HOD
{
    [Authorize(Roles = "HOD")]
    public class TimetableModel : PageModel
    {
        public User                     CurrentUser  { get; private set; } = default!;
        public List<AcademicTimetable>  MyTimetables { get; private set; } = new();
        public string[]                 AcademicYears => TimetableStore.AcademicYears.ToArray();

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public async Task<IActionResult> OnPostUploadAsync(
            string academicYear, int yearOfStudy, int semester, string notes, IFormFile? excelFile)
        {
            LoadData();

            if (excelFile == null || excelFile.Length == 0)
            {
                StatusMessage = "Please select an Excel file to upload.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            var ext = Path.GetExtension(excelFile.FileName).ToLowerInvariant();
            if (ext != ".xlsx" && ext != ".xls" && ext != ".csv")
            {
                StatusMessage = "Only Excel files (.xlsx, .xls) and CSV files are accepted.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            if (string.IsNullOrWhiteSpace(academicYear))
            {
                StatusMessage = "Please select an academic year.";
                StatusType    = "warning";
                return RedirectToPage();
            }

            // Save file to wwwroot/uploads/timetables/
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "timetables");
            Directory.CreateDirectory(uploadDir);

            var safeFileName = $"{CurrentUser.Department.Replace(" ", "_").Replace("&", "and")}__Y{yearOfStudy}S{semester}__{academicYear.Replace("/", "-")}__{DateTime.Now:yyyyMMddHHmmss}{ext}";
            var fullPath     = Path.Combine(uploadDir, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await excelFile.CopyToAsync(stream);
            }

            var existing = TimetableStore.Timetables.FirstOrDefault(t =>
                t.Department == CurrentUser.Department &&
                t.AcademicYear == academicYear &&
                t.YearOfStudy  == yearOfStudy  &&
                t.Semester     == semester      &&
                t.Status       != TimetableStatus.Archived);

            if (existing != null)
            {
                // Archive the old one
                existing.Status = TimetableStatus.Archived;
            }

            TimetableStore.Timetables.Add(new AcademicTimetable
            {
                Id              = TimetableStore.Timetables.Count > 0 ? TimetableStore.Timetables.Max(t => t.Id) + 1 : 1,
                AcademicYear    = academicYear,
                Department      = CurrentUser.Department,
                YearOfStudy     = yearOfStudy,
                Semester        = semester,
                FileName        = excelFile.FileName,
                StoredPath      = $"/uploads/timetables/{safeFileName}",
                Status          = TimetableStatus.Draft,
                UploadedBy      = CurrentUser.FullName,
                UploadedByEmail = CurrentUser.Email,
                UploadedOn      = DateTime.Now,
                Notes           = notes?.Trim() ?? string.Empty
            });

            TimetableRevisionData.Record(0, "TIMETABLE", CurrentUser.Department,
                "Uploaded", "", $"Y{yearOfStudy} S{semester} {academicYear}", CurrentUser.FullName, "HOD",
                $"New timetable uploaded: {excelFile.FileName}");

            StatusMessage = $"✓ Timetable uploaded successfully. Review it and publish when ready.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostPublish(int timetableId)
        {
            LoadData();
            var t = TimetableStore.Timetables.FirstOrDefault(x => x.Id == timetableId && x.Department == CurrentUser.Department);
            if (t == null) { StatusMessage = "Timetable not found."; StatusType = "danger"; return RedirectToPage(); }
            t.Status      = TimetableStatus.Published;
            t.PublishedOn = DateTime.Now;
            TimetableRevisionData.Record(0, "TIMETABLE", t.Department, "Published", "Draft", "Published",
                CurrentUser.FullName, "HOD", $"Y{t.YearOfStudy} S{t.Semester} {t.AcademicYear} published to students");
            StatusMessage = $"✓ Timetable published. Students can now see it.";
            StatusType    = "success";
            return RedirectToPage();
        }

        public IActionResult OnPostUnpublish(int timetableId)
        {
            LoadData();
            var t = TimetableStore.Timetables.FirstOrDefault(x => x.Id == timetableId && x.Department == CurrentUser.Department);
            if (t == null) { StatusMessage = "Timetable not found."; StatusType = "danger"; return RedirectToPage(); }
            t.Status      = TimetableStatus.Draft;
            t.PublishedOn = null;
            StatusMessage = "Timetable unpublished. Students can no longer see it.";
            StatusType    = "warning";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int timetableId)
        {
            LoadData();
            var t = TimetableStore.Timetables.FirstOrDefault(x => x.Id == timetableId && x.Department == CurrentUser.Department);
            if (t == null) { StatusMessage = "Timetable not found."; StatusType = "danger"; return RedirectToPage(); }

            // Delete file from disk
            if (!string.IsNullOrEmpty(t.StoredPath))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    t.StoredPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            }

            TimetableStore.Timetables.Remove(t);
            StatusMessage = $"Timetable deleted.";
            StatusType    = "warning";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser  = UserData.Users.FirstOrDefault(u => u.Email == email)
                ?? new User { FirstName = "Dr.", LastName = "Uwimana" };
            MyTimetables = TimetableStore.GetForDepartment(CurrentUser.Department);
        }
    }
}
