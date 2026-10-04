namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Roles available in the CSAS system.
/// </summary>
public enum UserRole
{
    Student,
    ClassRepresentative,
    Lecturer,
    HOD,
    Dean,
    DirectorOfQuality
}

/// <summary>
/// Represents an authenticated system user.
/// Every role (Student, Lecturer, HOD, Dean, Quality Director, CP)
/// maps to a User record.
/// </summary>
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    /// <summary>UNILAK email e.g. jane.uwase@unilak.ac.rw</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// For students: registration number e.g. UNILAK/2024/001.
    /// For staff: staff ID.
    /// Also accepted as the login username.
    /// </summary>
    public string Identifier { get; set; } = string.Empty;

    /// <summary>Hashed password — never store plain text</summary>
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    /// <summary>Department or faculty this user belongs to</summary>
    public string Department { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? LastLoginOn { get; set; }
}
