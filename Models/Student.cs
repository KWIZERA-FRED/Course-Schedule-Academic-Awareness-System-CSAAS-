namespace CourseScheduleSystem.Web.Models;
public class Student
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;
    public int Year { get; set; }

    public int Semester { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Programme { get; set; } = string.Empty;
    public double AttendanceRate { get; set; }
    public DateTime EnrolledOn { get; set; }
    public List<int> EnrolledCourseIds { get; set; } = new();
}
