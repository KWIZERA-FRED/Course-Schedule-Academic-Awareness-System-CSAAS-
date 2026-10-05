using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// Shared input for Dean Approve / Reject session handlers —
/// matches OnPostApproveSession and OnPostRejectSession on Dean/Dashboard.cshtml.cs.
/// </summary>
public class SessionApprovalDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid report ID.")]
    public int ReportId { get; set; }

    /// <summary>
    /// Required only when rejecting — explains what the HOD must correct.
    /// </summary>
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;
}
