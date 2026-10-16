using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

public static class TimetableStore
{
    public static List<AcademicTimetable> Timetables { get; } = new();

    public static List<string> AcademicYears { get; } = new()
    {
        "2024/2025", "2025/2026", "2026/2027"
    };

    public static string[] Departments { get; } = new[]
    {
        "Computer Science & IT",
        "Business & Management",
        "Social Sciences",
        "Law & Governance",
        "Civil Engineering",
        "Electrical Engineering",
        "Mechanical Engineering"
    };

    /// <summary>Returns published timetables visible to students.</summary>
    public static List<AcademicTimetable> GetPublished(
        string? department = null, int? year = null, int? semester = null, string? academicYear = null)
    {
        return Timetables
            .Where(t => t.IsPublished)
            .Where(t => department == null || t.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
            .Where(t => year     == null || t.YearOfStudy == year)
            .Where(t => semester == null || t.Semester    == semester)
            .Where(t => academicYear == null || t.AcademicYear == academicYear)
            .OrderByDescending(t => t.PublishedOn)
            .ToList();
    }

    /// <summary>Returns all timetables for a specific HOD department.</summary>
    public static List<AcademicTimetable> GetForDepartment(string department)
    {
        return Timetables
            .Where(t => t.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(t => t.UploadedOn)
            .ToList();
    }
}
