namespace CourseScheduleSystem.Web.DTOs.Shared;

/// <summary>
/// View-safe notification row — strips the internal UserEmail routing field
/// so the view never sees which email address a notification belongs to.
/// Used on Student/Notifications.cshtml and the unread-count badge.
/// </summary>
public class NotificationDto
{
    public int      Id        { get; set; }

    /// <summary>
    /// Notification category: "Info" | "Warning" | "Success" | "Deadline" |
    /// "MarkPosted" | "ClaimUpdate" | "SessionUpdate"
    /// </summary>
    public string   Type      { get; set; } = string.Empty;

    public string   Title     { get; set; } = string.Empty;
    public string   Message   { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool     IsRead    { get; set; }

    /// <summary>
    /// Optional in-app anchor or page URL the notification links to.
    /// </summary>
    public string   Link      { get; set; } = string.Empty;

    // Computed helper — avoids switch logic inside Razor
    public string BadgeClass => Type switch
    {
        "Warning"       => "dash-alert-warning",
        "Success"       => "dash-alert-success",
        "MarkPosted"    => "dash-alert-info",
        "ClaimUpdate"   => "dash-alert-info",
        "SessionUpdate" => "dash-alert-info",
        "Deadline"      => "dash-alert-warning",
        _               => "dash-alert-info"
    };

    public string IconClass => Type switch
    {
        "Warning"       => "bi-exclamation-triangle-fill",
        "Success"       => "bi-check-circle-fill",
        "MarkPosted"    => "bi-bar-chart-line-fill",
        "ClaimUpdate"   => "bi-chat-left-text-fill",
        "SessionUpdate" => "bi-calendar-check-fill",
        "Deadline"      => "bi-clock-fill",
        _               => "bi-bell-fill"
    };
}
