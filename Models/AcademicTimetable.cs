namespace CourseScheduleSystem.Web.Models;

public enum TimetableStatus
{
    Draft,
    Published,
    Archived
}

/// <summary>
/// Represents an uploaded academic timetable (Excel file) for a specific
/// department, academic year, year of study and semester.
/// Each HOD can only upload and manage timetables for their own department.
/// </summary>
public class AcademicTimetable
{
    public int    Id             { get; set; }

    /// <summary>e.g. "2025/2026"</summary>
    public string AcademicYear   { get; set; } = string.Empty;

    public string Department     { get; set; } = string.Empty;

    /// <summary>1, 2, 3 or 4</summary>
    public int    YearOfStudy    { get; set; }

    /// <summary>1, 2 or 3</summary>
    public int    Semester       { get; set; }

    /// <summary>Original uploaded file name e.g. "CS_Y2_S1_2025.xlsx"</summary>
    public string FileName       { get; set; } = string.Empty;

    /// <summary>Stored path under wwwroot/uploads/timetables/</summary>
    public string StoredPath     { get; set; } = string.Empty;

    public TimetableStatus Status { get; set; } = TimetableStatus.Draft;

    public string UploadedBy     { get; set; } = string.Empty;  // HOD full name
    public string UploadedByEmail{ get; set; } = string.Empty;

    public DateTime UploadedOn   { get; set; } = DateTime.Now;
    public DateTime? PublishedOn { get; set; }

    public string Notes          { get; set; } = string.Empty;

    // Computed
    public string DisplayTitle   => $"{Department} — Year {YearOfStudy}, Sem {Semester} ({AcademicYear})";
    public bool   IsPublished    => Status == TimetableStatus.Published;
}
