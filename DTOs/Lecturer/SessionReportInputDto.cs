using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Lecturer;

/// <summary>
/// Carries the Submit Session Report modal form fields —
/// matches OnPostSubmitSessionReport parameters on Lecturer/Dashboard.cshtml.cs.
/// </summary>
public class SessionReportInputDto
{
    [Required(ErrorMessage = "Please select a course.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid course.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Please enter the topic covered in this session.")]
    [StringLength(300, ErrorMessage = "Topic cannot exceed 300 characters.")]
    public string TopicCovered { get; set; } = string.Empty;

    [Range(0.5, 8, ErrorMessage = "Duration must be between 0.5 and 8 hours.")]
    public double DurationHours { get; set; } = 2;
}
