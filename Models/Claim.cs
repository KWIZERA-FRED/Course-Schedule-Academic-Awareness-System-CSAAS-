namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Possible states of a mark claim.
/// </summary>
public enum ClaimStatus
{
    /// <summary>Submitted by student, awaiting lecturer review</summary>
    UnderReview,

    /// <summary>Lecturer accepted the claim and corrected the mark</summary>
    Upheld,

    /// <summary>Lecturer reviewed and the mark stands as originally posted</summary>
    Rejected,

    /// <summary>Mark was partially corrected</summary>
    PartiallyUpheld
}

/// <summary>
/// Represents a dispute raised by a student about a posted mark.
/// </summary>
public class Claim
{
    public int Id { get; set; }

    /// <summary>The mark being disputed</summary>
    public int MarkId { get; set; }

    /// <summary>The student who raised the claim</summary>
    public int StudentId { get; set; }

    public string StudentFullName { get; set; } = string.Empty;

    public string StudentRegistrationNumber { get; set; } = string.Empty;

    /// <summary>Course code for quick display</summary>
    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;

    /// <summary>Assessment label e.g. "CAT 1"</summary>
    public string AssessmentLabel { get; set; } = string.Empty;

    /// <summary>The lecturer responsible for responding</summary>
    public string LecturerName { get; set; } = string.Empty;

    /// <summary>Student's written reason for the claim</summary>
    public string Reason { get; set; } = string.Empty;

    public ClaimStatus Status { get; set; } = ClaimStatus.UnderReview;

    /// <summary>Lecturer's written response to the student</summary>
    public string LecturerResponse { get; set; } = string.Empty;

    /// <summary>Corrected score if the claim was upheld or partially upheld</summary>
    public double? CorrectedScore { get; set; }

    /// <summary>When the student submitted the claim</summary>
    public DateTime RaisedOn { get; set; } = DateTime.Now;

    /// <summary>When the lecturer responded</summary>
    public DateTime? RespondedOn { get; set; }
}
