namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// A single private message exchanged between an HOD and a Class Representative.
/// </summary>
public class ChatMessage
{
    public int    Id           { get; set; }

    /// <summary>Email of the HOD involved in this conversation</summary>
    public string HODEmail     { get; set; } = string.Empty;

    /// <summary>Email of the Class Representative involved</summary>
    public string CPEmail      { get; set; } = string.Empty;

    /// <summary>Email of the user who sent this message</summary>
    public string SenderEmail  { get; set; } = string.Empty;

    /// <summary>Display name of the sender</summary>
    public string SenderName   { get; set; } = string.Empty;

    /// <summary>"HOD" or "CP"</summary>
    public string SenderRole   { get; set; } = string.Empty;

    public string Text         { get; set; } = string.Empty;

    public DateTime SentAt     { get; set; } = DateTime.Now;

    public bool IsRead         { get; set; } = false;
}
