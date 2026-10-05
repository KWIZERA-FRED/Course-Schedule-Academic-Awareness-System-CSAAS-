namespace CourseScheduleSystem.Web.Models;
public class ChatMessage
{
    public int    Id           { get; set; }
    public string HODEmail     { get; set; } = string.Empty;
    public string CPEmail      { get; set; } = string.Empty;
    public string SenderEmail  { get; set; } = string.Empty;
    public string SenderName   { get; set; } = string.Empty;
    public string SenderRole   { get; set; } = string.Empty;

    public string Text         { get; set; } = string.Empty;

    public DateTime SentAt     { get; set; } = DateTime.Now;

    public bool IsRead         { get; set; } = false;
}
