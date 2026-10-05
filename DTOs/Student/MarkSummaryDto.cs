namespace CourseScheduleSystem.Web.DTOs.Student;

/// <summary>
/// Flattened mark row shown on the student Marks table —
/// strips internal IDs and exposes only what the student needs to see.
/// </summary>
public class MarkSummaryDto
{
    public int    MarkId         { get; set; }
    public string CourseCode     { get; set; } = string.Empty;
    public string CourseTitle    { get; set; } = string.Empty;
    public string LecturerName   { get; set; } = string.Empty;
    public string AssessmentLabel{ get; set; } = string.Empty;
    public int    MaxScore       { get; set; }
    public double Score          { get; set; }
    public double Percentage     { get; set; }
    public string Grade          { get; set; } = string.Empty;
    public string Status         { get; set; } = string.Empty;  // "Draft" | "Published" | "Corrected"
    public bool   HasOpenClaim   { get; set; }
}
