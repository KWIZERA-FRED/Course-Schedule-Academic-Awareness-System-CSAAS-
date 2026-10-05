namespace CourseScheduleSystem.Web.DTOs.CP;

/// <summary>
/// Read-only summary of a course WhatsApp group shown in the CP group management card.
/// </summary>
public class GroupStatusDto
{
    public int      CourseId         { get; set; }
    public string   CourseCode       { get; set; } = string.Empty;
    public string   CourseTitle      { get; set; } = string.Empty;
    public string   WhatsAppGroupUrl { get; set; } = string.Empty;
    public DateTime JoinDeadline     { get; set; }
    public bool     HasGroup         => !string.IsNullOrEmpty(WhatsAppGroupUrl);
    public bool     IsOpen           => JoinDeadline >= DateTime.Now;
    public int      DaysLeft         => (JoinDeadline - DateTime.Now).Days;
}
