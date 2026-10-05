namespace CourseScheduleSystem.Web.Models;
public class EscalationRecord
{
    public int    Id            { get; set; }   // PK for EF Core
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
