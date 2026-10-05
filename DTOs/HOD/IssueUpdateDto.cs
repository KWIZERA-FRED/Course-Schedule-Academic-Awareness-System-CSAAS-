using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.HOD;

/// <summary>
/// Carries the Update Issue modal fields —
/// matches OnPostUpdateIssue parameters on HOD/Dashboard.cshtml.cs.
/// </summary>
public class IssueUpdateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid issue ID.")]
    public int IssueId { get; set; }

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Target status string: "Open" | "InProgress" | "Resolved" | "Escalated"
    /// </summary>
    [Required(ErrorMessage = "Please select a status.")]
    public string Status { get; set; } = "Open";
}
