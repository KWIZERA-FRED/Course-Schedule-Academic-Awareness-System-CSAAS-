using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// Carries the Escalate course form fields —
/// matches OnPostEscalate parameters on Quality/Dashboard.cshtml.cs.
/// </summary>
public class EscalationInputDto
{
    [Required(ErrorMessage = "Course ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid course ID.")]
    public int CourseId { get; set; }

    /// <summary>
    /// Optional notes the Quality Director attaches when escalating.
    /// If blank, a default threshold-breach message is used.
    /// </summary>
    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;
}
