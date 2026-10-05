namespace CourseScheduleSystem.Web.DTOs.Auth;

/// <summary>
/// Safe user representation sent to views — strips PasswordHash and internal audit fields.
/// </summary>
public class UserDto
{
    public int    Id         { get; set; }
    public string FirstName  { get; set; } = string.Empty;
    public string LastName   { get; set; } = string.Empty;
    public string FullName   => $"{FirstName} {LastName}";
    public string Email      { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string Role       { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string PhoneNumber{ get; set; } = string.Empty;
    public bool   IsActive   { get; set; }
    public DateTime CreatedOn{ get; set; }
}
