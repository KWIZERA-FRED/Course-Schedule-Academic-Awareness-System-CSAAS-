using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.Lecturer;

/// <summary>
/// Carries the Post Student Mark modal form fields —
/// matches OnPostSaveMark parameters on Lecturer/Dashboard.cshtml.cs.
/// </summary>
public class MarkInputDto
{
    [Required(ErrorMessage = "Please select a course.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid course.")]
    public int CourseId { get; set; }

    /// <summary>
    /// String form of AssessmentType enum: "CAT1", "CAT2", "Assignment1", etc.
    /// </summary>
    [Required(ErrorMessage = "Please select an assessment type.")]
    public string AssessmentType { get; set; } = "CAT1";

    [Required(ErrorMessage = "Student registration number is required.")]
    public string StudentRegNo { get; set; } = string.Empty;

    [Range(1, 200, ErrorMessage = "Max score must be between 1 and 200.")]
    public int MaxScore { get; set; } = 30;

    [Required(ErrorMessage = "Score is required.")]
    [Range(0, 200, ErrorMessage = "Score cannot exceed max score.")]
    public double Score { get; set; }

    [StringLength(500)]
    public string Remarks { get; set; } = string.Empty;

    /// <summary>
    /// When true the mark is immediately set to Published; otherwise it stays Draft.
    /// </summary>
    public bool PublishNow { get; set; } = true;
}
