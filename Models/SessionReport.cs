namespace CourseScheduleSystem.Web.Models;
public enum SessionReportStatus
{
    Draft,
    Submitted,
    CPSignedOff,
    HODApproved,
    DeanApproved,
    Rejected
}
public class SessionReport
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;
    public string LecturerName { get; set; } = string.Empty;
    public string ClassRepresentativeName { get; set; } = string.Empty;
    public DateTime SessionDate { get; set; }
    public double DurationHours { get; set; }
    public string TopicCovered { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;

    public SessionReportStatus Status { get; set; } = SessionReportStatus.Draft;

    public DateTime? SubmittedOn { get; set; }

    public DateTime? CPSignedOffOn { get; set; }
    public string CPNotes { get; set; } = string.Empty;

    public DateTime? HODApprovedOn { get; set; }

    public string HODNotes { get; set; } = string.Empty;

    public DateTime? DeanApprovedOn { get; set; }

    public string DeanNotes { get; set; } = string.Empty;
    public string RejectionReason { get; set; } = string.Empty;
}
