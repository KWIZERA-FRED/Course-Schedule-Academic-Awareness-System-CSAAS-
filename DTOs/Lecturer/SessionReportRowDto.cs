namespace CourseScheduleSystem.Web.DTOs.Lecturer;

/// <summary>
/// Read-only row for the Lecturer session-report history table —
/// shows the full approval chain without exposing internal notes to the view.
/// </summary>
public class SessionReportRowDto
{
    public int      ReportId        { get; set; }
    public string   CourseCode      { get; set; } = string.Empty;
    public string   CourseTitle     { get; set; } = string.Empty;
    public DateTime SessionDate     { get; set; }
    public string   TopicCovered    { get; set; } = string.Empty;
    public double   DurationHours   { get; set; }
    public string   Status          { get; set; } = string.Empty;

    // Approval chain timestamps — null means that stage has not been reached yet
    public DateTime? CPSignedOffOn  { get; set; }
    public DateTime? HODApprovedOn  { get; set; }
    public DateTime? DeanApprovedOn { get; set; }

    public string RejectionReason   { get; set; } = string.Empty;
}
