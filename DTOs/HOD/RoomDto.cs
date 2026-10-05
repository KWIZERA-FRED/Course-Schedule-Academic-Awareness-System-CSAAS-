namespace CourseScheduleSystem.Web.DTOs.HOD;

/// <summary>
/// View-safe room representation used in the HOD room-arrangement grid —
/// exposes availability and booked slots without internal notes or full dictionary.
/// </summary>
public class RoomDto
{
    public int    Id           { get; set; }
    public string Block        { get; set; } = string.Empty;
    public string Number       { get; set; } = string.Empty;
    public string FullName     { get; set; } = string.Empty;
    public int    Capacity     { get; set; }
    public string Type         { get; set; } = string.Empty;   // "Lecture" | "Laboratory" | etc.
    public bool   IsAvailable  { get; set; }

    /// <summary>
    /// Booked time slots: key = schedule string (e.g. "Mon 08:00 - 10:00"), value = course code.
    /// </summary>
    public Dictionary<string, string> BookedSlots { get; set; } = new();

    public string Notes        { get; set; } = string.Empty;
}
