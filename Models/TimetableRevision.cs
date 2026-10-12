namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Records every change made to a course timetable slot (schedule time, venue, lecturer).
/// Provides a full revision history visible to HOD and Admin.
/// </summary>
public class TimetableRevision
{
    public int    Id            { get; set; }
    public int    CourseId      { get; set; }
    public string CourseCode    { get; set; } = string.Empty;
    public string CourseTitle   { get; set; } = string.Empty;

    public string FieldChanged  { get; set; } = string.Empty;  // "ScheduleTime" | "Venue" | "Lecturer"
    public string OldValue      { get; set; } = string.Empty;
    public string NewValue      { get; set; } = string.Empty;

    public string ChangedBy     { get; set; } = string.Empty;  // User full name
    public string ChangedByRole { get; set; } = string.Empty;  // "HOD" | "Admin"
    public DateTime ChangedOn   { get; set; } = DateTime.Now;

    public string Reason        { get; set; } = string.Empty;
}
