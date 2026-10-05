namespace CourseScheduleSystem.Web.DTOs.Shared;

/// <summary>
/// Wraps the TempData StatusMessage / StatusType pair used on every dashboard —
/// centralises the alert-rendering logic instead of repeating it in every Razor page.
/// </summary>
public class StatusMessageDto
{
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Severity level: "success" | "warning" | "danger" | "info"
    /// </summary>
    public string Type    { get; set; } = "info";

    // Computed helpers consumed by a shared partial or inline Razor code
    public string AlertClass => Type switch
    {
        "success" => "dash-alert-success",
        "danger"  => "dash-alert-danger",
        "warning" => "dash-alert-warning",
        _         => "dash-alert-info"
    };

    public string IconClass => Type switch
    {
        "success" => "bi-check-circle-fill",
        "danger"  => "bi-x-circle-fill",
        "warning" => "bi-exclamation-triangle-fill",
        _         => "bi-info-circle-fill"
    };
}
