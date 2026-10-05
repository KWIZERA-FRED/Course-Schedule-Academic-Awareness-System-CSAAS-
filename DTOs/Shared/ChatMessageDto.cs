namespace CourseScheduleSystem.Web.DTOs.Shared;

/// <summary>
/// View-safe chat message — strips the HODEmail / CPEmail routing fields
/// from ChatMessage so the view only receives what it needs to render the bubble.
/// Used in HOD/Chat.cshtml and CP/Chat.cshtml.
/// </summary>
public class ChatMessageDto
{
    public int      Id           { get; set; }
    public string   SenderName   { get; set; } = string.Empty;

    /// <summary>
    /// "HOD" or "CP" — used to decide bubble alignment (left / right).
    /// </summary>
    public string   SenderRole   { get; set; } = string.Empty;

    public string   Text         { get; set; } = string.Empty;
    public DateTime SentAt       { get; set; }
    public bool     IsRead       { get; set; }

    // Convenience helper for the view
    public bool IsMine(string currentUserRole) =>
        string.Equals(SenderRole, currentUserRole, StringComparison.OrdinalIgnoreCase);
}
