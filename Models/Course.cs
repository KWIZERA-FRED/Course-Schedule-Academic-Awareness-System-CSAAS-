namespace CourseScheduleSystem.Web.Models;
public class Course
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Lecturer { get; set; } = string.Empty;
    public string ScheduleTime { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public string WhatsappGroupUrl { get; set; } = string.Empty;
    public DateTime JoinDeadline { get; set; }
    public int ScheduledHours { get; set; } = 36;
    public int DeliveredHours { get; set; } = 0;
    public string Department { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Semester { get; set; }
}
