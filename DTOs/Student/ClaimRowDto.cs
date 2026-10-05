namespace CourseScheduleSystem.Web.DTOs.Student;

/// <summary>
/// Read-only row shown on the student My Claims table.
/// </summary>
public class ClaimRowDto
{
    public int      ClaimId         { get; set; }
    public string   CourseCode      { get; set; } = string.Empty;
    public string   CourseTitle     { get; set; } = string.Empty;
    public string   AssessmentLabel { get; set; } = string.Empty;
    public string   LecturerName    { get; set; } = string.Empty;
    public string   Reason          { get; set; } = string.Empty;
    public string   Status          { get; set; } = string.Empty;  // "UnderReview" | "Upheld" | "Rejected" | "PartiallyUpheld"
    public string   LecturerResponse{ get; set; } = string.Empty;
    public DateTime RaisedOn        { get; set; }
}
