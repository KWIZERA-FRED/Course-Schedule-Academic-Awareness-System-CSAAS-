using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Lecturer;

/// <summary>
/// Carries the Respond to Claim modal fields —
/// matches OnPostRespondClaim parameters on Lecturer/Dashboard.cshtml.cs.
/// </summary>
public class ClaimResponseDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid claim ID.")]
    public int ClaimId { get; set; }

    /// <summary>
    /// Decision string: "upheld" | "rejected" | "partial"
    /// </summary>
    [Required(ErrorMessage = "Please select a decision.")]
    public string Decision { get; set; } = "rejected";

    /// <summary>
    /// Optional corrected score — only used when Decision is "upheld" or "partial".
    /// </summary>
    [Range(0, 200, ErrorMessage = "Corrected score must be between 0 and 200.")]
    public double? CorrectedScore { get; set; }

    [Required(ErrorMessage = "Please provide an explanation to the student.")]
    [MinLength(5, ErrorMessage = "Explanation must be at least 5 characters.")]
    [StringLength(1000)]
    public string Explanation { get; set; } = string.Empty;
}
