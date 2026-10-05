using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.CP;

/// <summary>
/// Carries the Create / Update WhatsApp group modal fields —
/// shared by OnPostCreateGroup and OnPostUpdateGroup on CP/Dashboard.cshtml.cs.
/// </summary>
public class WhatsAppGroupInputDto
{
    [Required(ErrorMessage = "Course is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid course.")]
    public int CourseId { get; set; }

    [Required(ErrorMessage = "Please provide a valid WhatsApp group link.")]
    [Url(ErrorMessage = "The group link must be a valid URL (e.g. https://chat.whatsapp.com/...).")]
    [StringLength(500)]
    public string GroupUrl { get; set; } = string.Empty;

    /// <summary>
    /// Number of days from today until the joining deadline.
    /// </summary>
    [Range(1, 60, ErrorMessage = "Deadline must be between 1 and 60 days.")]
    public int DeadlineDays { get; set; } = 14;
}
