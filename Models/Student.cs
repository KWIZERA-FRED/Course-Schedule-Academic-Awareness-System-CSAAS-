namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Represents a student enrolled at UNILAK.
/// </summary>
public class Student
{
    public int Id { get; set; }

    /// <summary>e.g. UNILAK/2024/001</summary>
    public string RegistrationNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    /// <summary>Full name derived from first + last</summary>
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;

    /// <summary>Academic year e.g. 3</summary>
    public int Year { get; set; }

    public int Semester { get; set; }

    /// <summary>e.g. Computer Science &amp; IT</summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>e.g. BIT / CSE / CE</summary>
    public string Programme { get; set; } = string.Empty;

    /// <summary>Overall attendance rate across all courses (0–100)</summary>
    public double AttendanceRate { get; set; }

    /// <summary>Date the student registered</summary>
    public DateTime EnrolledOn { get; set; }

    /// <summary>IDs of courses this student is enrolled in</summary>
    public List<int> EnrolledCourseIds { get; set; } = new();
}
