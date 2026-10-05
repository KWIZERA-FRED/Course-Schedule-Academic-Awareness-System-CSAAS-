namespace CourseScheduleSystem.Web.DTOs.Lecturer;

/// <summary>
/// Lightweight student row shown in the Lecturer class-list table —
/// strips sensitive fields (email, enrolled course list) from the Student model.
/// </summary>
public class StudentRowDto
{
    public int    StudentId          { get; set; }
    public string FullName           { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Programme          { get; set; } = string.Empty;
    public int    Year               { get; set; }
    public double AttendanceRate     { get; set; }
}
