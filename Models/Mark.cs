namespace CourseScheduleSystem.Web.Models;

/// <summary>
/// Assessment types available in the system.
/// </summary>
public enum AssessmentType
{
    CAT1,
    CAT2,
    Assignment1,
    Assignment2,
    Practical,
    MidSemester,
    FinalExam
}

/// <summary>
/// Publication state of a mark entry.
/// </summary>
public enum MarkStatus
{
    /// <summary>Saved by lecturer but not yet visible to the student</summary>
    Draft,

    /// <summary>Visible to the student on their dashboard</summary>
    Published,

    /// <summary>Mark was corrected after a successful claim</summary>
    Corrected
}

/// <summary>
/// Represents a single assessment mark posted by a lecturer for one student.
/// </summary>
public class Mark
{
    public int Id { get; set; }

    /// <summary>The course this mark belongs to</summary>
    public int CourseId { get; set; }

    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;

    /// <summary>The student this mark was awarded to</summary>
    public int StudentId { get; set; }

    public string StudentRegistrationNumber { get; set; } = string.Empty;

    public string StudentFullName { get; set; } = string.Empty;

    /// <summary>The lecturer who posted the mark</summary>
    public string LecturerName { get; set; } = string.Empty;

    public AssessmentType AssessmentType { get; set; }

    /// <summary>Display-friendly assessment label e.g. "CAT 1"</summary>
    public string AssessmentLabel => AssessmentType switch
    {
        AssessmentType.CAT1        => "CAT 1",
        AssessmentType.CAT2        => "CAT 2",
        AssessmentType.Assignment1 => "Assignment 1",
        AssessmentType.Assignment2 => "Assignment 2",
        AssessmentType.Practical   => "Practical",
        AssessmentType.MidSemester => "Mid-Semester",
        AssessmentType.FinalExam   => "Final Exam",
        _                          => AssessmentType.ToString()
    };

    /// <summary>Maximum possible score e.g. 30 or 70</summary>
    public int MaxScore { get; set; }

    /// <summary>Score the student obtained</summary>
    public double Score { get; set; }

    /// <summary>Percentage score (0–100)</summary>
    public double Percentage => MaxScore > 0 ? Math.Round(Score / MaxScore * 100, 1) : 0;

    /// <summary>Letter grade derived from percentage</summary>
    public string Grade => Percentage switch
    {
        >= 90 => "A+",
        >= 80 => "A",
        >= 75 => "B+",
        >= 70 => "B",
        >= 65 => "C+",
        >= 60 => "C",
        >= 50 => "D",
        _     => "F"
    };

    public MarkStatus Status { get; set; } = MarkStatus.Draft;

    /// <summary>Optional remarks from the lecturer</summary>
    public string Remarks { get; set; } = string.Empty;

    /// <summary>When the lecturer saved this mark</summary>
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    /// <summary>When the mark was published to the student</summary>
    public DateTime? PublishedOn { get; set; }
}
