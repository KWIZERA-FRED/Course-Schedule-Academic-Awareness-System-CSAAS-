using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Profile;

/// <summary>
/// Carries the change-password form fields — replaces NewPassword / ConfirmPassword
/// [BindProperty] fields on Profile/Index.cshtml.cs.
/// </summary>
public class ChangePasswordDto
{
    [Required(ErrorMessage = "New password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your new password.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
