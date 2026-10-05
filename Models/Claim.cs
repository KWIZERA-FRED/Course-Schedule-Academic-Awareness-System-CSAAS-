namespace CourseScheduleSystem.Web.Models;
public enum ClaimStatus
{
    UnderReview,
    Upheld,
    Rejected,
    PartiallyUpheld
}
public class Claim
{
    public int Id { get; set; }
    public int MarkId { get; set; }
    public int StudentId { get; set; }

    public string StudentFullName { get; set; } = string.Empty;

    public string StudentRegistrationNumber { get; set; } = string.Empty;
    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;
    public string AssessmentLabel { get; set; } = string.Empty;
    public string LecturerName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;

    public ClaimStatus Status { get; set; } = ClaimStatus.UnderReview;
    public string LecturerResponse { get; set; } = string.Empty;
    public double? CorrectedScore { get; set; }
    public DateTime RaisedOn { get; set; } = DateTime.Now;
    public DateTime? RespondedOn { get; set; }
}
