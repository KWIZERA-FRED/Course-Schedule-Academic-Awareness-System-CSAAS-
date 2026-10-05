using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Auth;

/// <summary>
/// Captures login form input — replaces the three [BindProperty] fields on LoginModel.
/// </summary>
public class LoginInputDto
{
    [Required(ErrorMessage = "Please enter your email or registration number.")]
    public string Identifier { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Optional role hint shown on the login form (not used for auth logic).
    /// </summary>
    public string? Role { get; set; }
}
