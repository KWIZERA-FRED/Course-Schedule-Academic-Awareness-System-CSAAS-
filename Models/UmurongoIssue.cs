namespace CourseScheduleSystem.Web.Models;

public enum IssueStatus  { Open, InProgress, Resolved, Escalated }
public enum IssuePriority { Low, Medium, High, Critical }

public class UmurongoIssue
{
    public int           Id          { get; set; }
    public string        Title       { get; set; } = string.Empty;
    public string        Description { get; set; } = string.Empty;
    public string        ReportedBy  { get; set; } = string.Empty;
    public string        Category    { get; set; } = string.Empty;
    public IssuePriority Priority    { get; set; } = IssuePriority.Medium;
    public IssueStatus   Status      { get; set; } = IssueStatus.Open;
    public DateTime      ReportedOn  { get; set; } = DateTime.Now;
    public DateTime?     ResolvedOn  { get; set; }
    public string        HODNotes    { get; set; } = string.Empty;
    public string        DeanNotes   { get; set; } = string.Empty;
    public bool          EscalatedToDean { get; set; } = false;
}
