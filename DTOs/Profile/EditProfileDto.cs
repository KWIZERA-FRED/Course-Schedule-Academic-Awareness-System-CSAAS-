using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Profile;

/// <summary>
/// Carries the personal-info edit form fields — replaces the four [BindProperty]
/// fields on Profile/Index.cshtml.cs.
/// </summary>
public class EditProfileDto
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

    [Phone(ErrorMessage = "Enter a valid phone number.")]
    [StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;
}
