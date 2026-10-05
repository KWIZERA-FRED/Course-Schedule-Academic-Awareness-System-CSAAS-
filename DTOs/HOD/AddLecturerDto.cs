using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.HOD;

/// <summary>
/// Carries the Add Lecturer modal form fields —
/// matches OnPostAddLecturer parameters on HOD/Dashboard.cshtml.cs.
/// </summary>
public class AddLecturerDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Optional staff identifier (e.g. "STAFF/012"). Auto-generated if blank.
    /// </summary>
    [StringLength(30)]
    public string StaffId { get; set; } = string.Empty;

    /// <summary>
    /// Title prefix: "Dr.", "Prof.", "Mr.", "Mrs.", "Ms."
    /// </summary>
    [StringLength(10)]
    public string Title { get; set; } = "Dr.";
}
