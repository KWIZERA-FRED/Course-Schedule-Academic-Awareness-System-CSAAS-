using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.HOD;

/// <summary>
/// Carries the Add Course modal form fields —
/// matches OnPostAddCourse parameters on HOD/Dashboard.cshtml.cs.
/// </summary>
public class AddCourseDto
{
    [Required(ErrorMessage = "Course code is required.")]
    [StringLength(20)]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course title is required.")]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(100)]
    public string Lecturer { get; set; } = string.Empty;

    [StringLength(50)]
    public string ScheduleTime { get; set; } = string.Empty;

    [StringLength(100)]
    public string Venue { get; set; } = string.Empty;

    [Range(1, 200)]
    public int ScheduledHours { get; set; } = 36;

    [Range(1, 4)]
    public int Year { get; set; } = 1;

    [Range(1, 2)]
    public int Semester { get; set; } = 1;

    public string WhatsappUrl { get; set; } = string.Empty;
}
