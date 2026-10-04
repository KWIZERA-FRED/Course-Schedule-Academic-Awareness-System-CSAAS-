namespace CourseScheduleSystem.Web.Models;
public enum UserRole
{
    Student,
    ClassRepresentative,
    Lecturer,
    HOD,
    Dean,
    DirectorOfQuality
}
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }
    public string Department { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? LastLoginOn { get; set; }
}
