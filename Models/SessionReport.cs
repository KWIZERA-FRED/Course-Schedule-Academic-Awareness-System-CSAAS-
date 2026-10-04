namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// The approval stage a session report is currently at.
/// Flows: Submitted → CPSignedOff → HODApproved → DeanApproved
/// </summary>
public enum SessionReportStatus
{
    /// <summary>Lecturer saved but not yet submitted</summary>
    Draft,

    /// <summary>Lecturer submitted — awaiting CP sign-off</summary>
    Submitted,

    /// <summary>Class Representative confirmed the session happened</summary>
    CPSignedOff,

    /// <summary>HOD reviewed and approved</summary>
    HODApproved,

    /// <summary>Dean gave final approval — fully verified</summary>
    DeanApproved,

    /// <summary>Rejected at any stage — returned for correction</summary>
    Rejected
}

/// <summary>
/// Represents a record of a single teaching session submitted by a lecturer.
/// Flows through CP → HOD → Dean approval chain.
/// </summary>
public class SessionReport
{
    public int Id { get; set; }

    /// <summary>The course this session belongs to</summary>
    public int CourseId { get; set; }

    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;

    /// <summary>The lecturer who delivered the session</summary>
    public string LecturerName { get; set; } = string.Empty;

    /// <summary>The Class Representative for this course</summary>
    public string ClassRepresentativeName { get; set; } = string.Empty;

    /// <summary>Date and start time of the session</summary>
    public DateTime SessionDate { get; set; }

    /// <summary>Duration in hours e.g. 2</summary>
    public double DurationHours { get; set; }

    /// <summary>Topic or content covered during the session</summary>
    public string TopicCovered { get; set; } = string.Empty;

    /// <summary>Venue where the session was held</summary>
    public string Venue { get; set; } = string.Empty;

    public SessionReportStatus Status { get; set; } = SessionReportStatus.Draft;

    // ── Approval chain timestamps ────────────────────────────

    public DateTime? SubmittedOn { get; set; }

    public DateTime? CPSignedOffOn { get; set; }

    /// <summary>Notes added by the CP when signing off</summary>
    public string CPNotes { get; set; } = string.Empty;

    public DateTime? HODApprovedOn { get; set; }

    public string HODNotes { get; set; } = string.Empty;

    public DateTime? DeanApprovedOn { get; set; }

    public string DeanNotes { get; set; } = string.Empty;

    /// <summary>Reason provided if the report was rejected at any stage</summary>
    public string RejectionReason { get; set; } = string.Empty;
}
