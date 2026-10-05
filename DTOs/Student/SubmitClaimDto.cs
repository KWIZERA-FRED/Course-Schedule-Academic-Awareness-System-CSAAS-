using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Student;

/// <summary>
/// Carries the claim submission form — matches the OnPostSubmitClaim handler parameters
/// on Student/Dashboard.cshtml.cs.
/// </summary>
public class SubmitClaimDto
{
    [Required]
    public string CourseCode      { get; set; } = string.Empty;

    [Required]
    public string AssessmentLabel { get; set; } = string.Empty;

    [Required]
    public string LecturerName    { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please describe the reason for your claim.")]
    [MinLength(10, ErrorMessage = "Please give at least a brief description.")]
    [StringLength(1000)]
    public string Reason          { get; set; } = string.Empty;
}
