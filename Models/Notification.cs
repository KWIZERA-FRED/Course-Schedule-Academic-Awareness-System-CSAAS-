namespace CourseScheduleSystem.Web.Models;

public enum NotificationType
{
    Info,
    Warning,
    Success,
    Deadline,
    MarkPosted,
    ClaimUpdate,
    SessionUpdate
}
public class Notification
{
    public int              Id         { get; set; }
    public string           UserEmail  { get; set; } = string.Empty;
    public NotificationType Type       { get; set; } = NotificationType.Info;
    public string           Title      { get; set; } = string.Empty;
    public string           Message    { get; set; } = string.Empty;
    public DateTime         CreatedAt  { get; set; } = DateTime.Now;
    public bool             IsRead     { get; set; } = false;
    public string           Link       { get; set; } = string.Empty; // optional nav link
}
