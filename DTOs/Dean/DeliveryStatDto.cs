namespace CourseScheduleSystem.Web.DTOs.Dean;

/// <summary>
/// A single department delivery-rate entry — replaces the raw
/// <c>Dictionary&lt;string, double&gt; DeliveryByDepartment</c> entries passed to views.
/// Used by Dean and Quality dashboards.
/// </summary>
public class DeliveryStatDto
{
    public string Department   { get; set; } = string.Empty;
    public double DeliveryRate { get; set; }
    public int    CourseCount  { get; set; }

    // Computed helpers for the view — avoids logic in Razor
    public string RatingLabel => DeliveryRate switch
    {
        >= 90 => "Excellent",
        >= 75 => "Good",
        >= 60 => "Monitor",
        _     => "Critical"
    };

    public string RatingBadgeClass => DeliveryRate switch
    {
        >= 90 => "badge-approved",
        >= 75 => "badge-verified",
        >= 60 => "badge-pending",
        _     => "badge-missed"
    };

    public string BarColor => DeliveryRate switch
    {
        >= 90 => "#059669",
        >= 75 => "#1C3B83",
        >= 60 => "#d97706",
        _     => "#dc2626"
    };
}
