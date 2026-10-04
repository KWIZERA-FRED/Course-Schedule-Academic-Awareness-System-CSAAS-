namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Type of room/space available in the university.
/// </summary>
public enum RoomType
{
    Lecture,
    Laboratory,
    Seminar,
    Auditorium,
    ComputerLab
}

/// <summary>
/// Represents a physical room or venue in the university campus.
/// Used by the HOD for room arrangement — assigning rooms to courses
/// and detecting scheduling conflicts before they happen.
/// </summary>
public class Room
{
    public int Id { get; set; }

    /// <summary>Building block e.g. "Block A", "Block B"</summary>
    public string Block { get; set; } = string.Empty;

    /// <summary>Room number e.g. "204", "Lab 02"</summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>Full display name e.g. "Block A – Room 204"</summary>
    public string FullName => $"{Block} – {Number}";

    /// <summary>Maximum number of students the room can hold</summary>
    public int Capacity { get; set; }

    public RoomType Type { get; set; }

    /// <summary>Whether this room is currently available for scheduling</summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Slots already booked for this room.
    /// Key = "Mon 08:00-10:00" style string matching Course.ScheduleTime.
    /// Value = course code occupying that slot.
    /// </summary>
    public Dictionary<string, string> BookedSlots { get; set; } = new();

    /// <summary>Any notes about the room e.g. "Projector needs repair"</summary>
    public string Notes { get; set; } = string.Empty;
}
