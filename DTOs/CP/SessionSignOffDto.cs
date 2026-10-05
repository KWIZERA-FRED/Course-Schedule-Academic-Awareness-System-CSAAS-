using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.CP;

/// <summary>
/// Carries the Sign-Off modal fields —
/// matches OnPostSignOff parameters on CP/Dashboard.cshtml.cs.
/// </summary>
public class SessionSignOffDto
{
    [Required(ErrorMessage = "Report ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid report ID.")]
    public int ReportId { get; set; }

    /// <summary>
    /// Optional CP attendance / observation notes.
    /// </summary>
    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;
}
