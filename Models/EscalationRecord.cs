namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Records a course escalation raised by the Director of Quality.
/// </summary>
public class EscalationRecord
{
    public string CourseCode    { get; set; } = string.Empty;
    public string CourseTitle   { get; set; } = string.Empty;
    public string Department    { get; set; } = string.Empty;
    public string Lecturer      { get; set; } = string.Empty;
    public double DeliveryPct   { get; set; }
    public string Notes         { get; set; } = string.Empty;
    public string EscalatedBy   { get; set; } = string.Empty;
    public string EscalatedTo   { get; set; } = string.Empty;
    public DateTime EscalatedOn { get; set; } = DateTime.Now;
}
