namespace CourseScheduleSystem.Web.DTOs.CP;

/// <summary>
/// Read-only row shown in the CP pending sign-offs table and recent sign-offs list —
/// contains only the fields the CP needs to see (no HOD/Dean internal notes).
/// </summary>
public class SessionReportRowDto
{
    public int      ReportId                 { get; set; }
    public string   CourseCode               { get; set; } = string.Empty;
    public string   CourseTitle              { get; set; } = string.Empty;
    public string   LecturerName             { get; set; } = string.Empty;
    public DateTime SessionDate              { get; set; }
    public double   DurationHours            { get; set; }
    public string   TopicCovered             { get; set; } = string.Empty;
    public string   Venue                    { get; set; } = string.Empty;
    public string   Status                   { get; set; } = string.Empty;
    public DateTime? CPSignedOffOn           { get; set; }
    public string   CPNotes                  { get; set; } = string.Empty;
}
