namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// Read-only UmurongoSmart issue row shown in Dean and HOD dashboards —
/// exposes only the fields appropriate for each role.
/// Use <see cref="IncludeDeanNotes"/> to control whether Dean-level notes are visible.
/// </summary>
public class IssueRowDto
{
    public int      IssueId        { get; set; }
    public string   Title          { get; set; } = string.Empty;
    public string   Description    { get; set; } = string.Empty;
    public string   Category       { get; set; } = string.Empty;
    public string   Priority       { get; set; } = string.Empty;  // "Low" | "Medium" | "High" | "Critical"
    public string   Status         { get; set; } = string.Empty;  // "Open" | "InProgress" | "Resolved" | "Escalated"
    public string   ReportedBy     { get; set; } = string.Empty;
    public DateTime ReportedOn     { get; set; }
    public DateTime? ResolvedOn    { get; set; }
    public string   HODNotes       { get; set; } = string.Empty;
    public string   DeanNotes      { get; set; } = string.Empty;
    public bool     EscalatedToDean{ get; set; }
}
