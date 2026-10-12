namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Represents a time slot that a lecturer marks as available or unavailable.
/// Used by the Smart Scheduling Module to suggest conflict-free timetable slots.
/// </summary>
public class TeacherAvailability
{
    public int    Id              { get; set; }
    public string LecturerEmail   { get; set; } = string.Empty;
    public string LecturerName    { get; set; } = string.Empty;

    /// <summary>Day of the week: Monday, Tuesday, Wednesday, Thursday, Friday</summary>
    public string Day             { get; set; } = string.Empty;

    /// <summary>Time slot e.g. "08:00 - 10:00"</summary>
    public string TimeSlot        { get; set; } = string.Empty;

    /// <summary>Full slot key used for conflict checks e.g. "Mon 08:00 - 10:00"</summary>
    public string SlotKey         => $"{Day[..3]} {TimeSlot}";

    public bool   IsAvailable     { get; set; } = true;

    /// <summary>Optional reason when unavailable e.g. "Research meeting"</summary>
    public string UnavailableNote { get; set; } = string.Empty;

    public DateTime UpdatedOn     { get; set; } = DateTime.Now;
}
