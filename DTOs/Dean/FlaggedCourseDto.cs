namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// Read-only representation of a course flagged below the delivery threshold —
/// used in Quality dashboard flagged-courses table and Dean audit log.
/// </summary>
public class FlaggedCourseDto
{
    public int    CourseId     { get; set; }
    public string Code         { get; set; } = string.Empty;
    public string Title        { get; set; } = string.Empty;
    public string Department   { get; set; } = string.Empty;
    public string Lecturer     { get; set; } = string.Empty;
    public int    DeliveryPct  { get; set; }
}
