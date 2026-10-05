namespace CourseScheduleSystem.Web.Models;
public enum RoomType
{
    Lecture,
    Laboratory,
    Seminar,
    Auditorium,
    ComputerLab
}
public class Room
{
    public int Id { get; set; }
    public string Block { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string FullName => $"{Block} – {Number}";
    public int Capacity { get; set; }

    public RoomType Type { get; set; }
    public bool IsAvailable { get; set; } = true;
    public Dictionary<string, string> BookedSlots { get; set; } = new();
    public string Notes { get; set; } = string.Empty;
}
