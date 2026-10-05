namespace CourseScheduleSystem.Web.DTOs.Student;

/// <summary>
/// Subset of Course exposed to the student view — excludes internal admin fields.
/// Used for the Course Explorer search cards and the weekly schedule table.
/// </summary>
public class CourseCardDto
{
    public int      Id               { get; set; }
    public string   Code             { get; set; } = string.Empty;
    public string   Title            { get; set; } = string.Empty;
    public string   Lecturer         { get; set; } = string.Empty;
    public string   ScheduleTime     { get; set; } = string.Empty;
    public string   Venue            { get; set; } = string.Empty;
    public string   WhatsappGroupUrl { get; set; } = string.Empty;
    public DateTime JoinDeadline     { get; set; }
    public int      ScheduledHours   { get; set; }
    public int      DeliveredHours   { get; set; }

    // Computed helpers consumed by the view
    public bool   IsGroupOpen    => JoinDeadline >= DateTime.Now;
    public int    DaysLeft       => (JoinDeadline - DateTime.Now).Days;
    public int    DeliveryPct    => ScheduledHours > 0
                                    ? (int)Math.Round((double)DeliveredHours / ScheduledHours * 100)
                                    : 0;
}
