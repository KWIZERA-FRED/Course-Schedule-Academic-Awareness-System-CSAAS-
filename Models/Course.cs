namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Represents a university course offered in the current semester.
/// </summary>
public class Course
{
    public int Id { get; set; }

    /// <summary>e.g. CSE301</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>e.g. Software Engineering</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Full name of the assigned lecturer</summary>
    public string Lecturer { get; set; } = string.Empty;

    /// <summary>e.g. Mon 08:00 - 10:00</summary>
    public string ScheduleTime { get; set; } = string.Empty;

    /// <summary>e.g. Block A - Room 204</summary>
    public string Venue { get; set; } = string.Empty;

    /// <summary>Official WhatsApp group invite link</summary>
    public string WhatsappGroupUrl { get; set; } = string.Empty;

    /// <summary>Deadline after which students can no longer join the WhatsApp group</summary>
    public DateTime JoinDeadline { get; set; }

    /// <summary>Total scheduled teaching hours for the semester</summary>
    public int ScheduledHours { get; set; } = 36;

    /// <summary>Hours already delivered and verified</summary>
    public int DeliveredHours { get; set; } = 0;

    /// <summary>Department that owns this course</summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>Academic year e.g. Year 3</summary>
    public int Year { get; set; }

    /// <summary>Semester number e.g. 1 or 2</summary>
    public int Semester { get; set; }
}
