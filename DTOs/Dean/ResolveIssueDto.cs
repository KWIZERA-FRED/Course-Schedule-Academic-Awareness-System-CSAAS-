using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// Carries the Resolve Issue action parameters —
/// matches OnPostResolveIssue parameters on Dean/Dashboard.cshtml.cs.
/// </summary>
public class ResolveIssueDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid issue ID.")]
    public int IssueId { get; set; }

    /// <summary>
    /// Dean's resolution notes. Defaults to "Resolved by Dean." if blank.
    /// </summary>
    [StringLength(500)]
    public string DeanNotes { get; set; } = string.Empty;
}
