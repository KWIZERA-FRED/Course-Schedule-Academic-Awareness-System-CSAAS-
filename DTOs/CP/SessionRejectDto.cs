using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.CP;

/// <summary>
/// Carries the Reject Session modal fields —
/// matches OnPostRejectSession parameters on CP/Dashboard.cshtml.cs.
/// </summary>
public class SessionRejectDto
{
    [Required(ErrorMessage = "Report ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid report ID.")]
    public int ReportId { get; set; }

    [Required(ErrorMessage = "Please explain why the session cannot be confirmed.")]
    [MinLength(5, ErrorMessage = "Reason must be at least 5 characters.")]
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;
}
