namespace CourseScheduleSystem.Web.Models;
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
public enum MarkStatus
{
    Draft,
    Published,
    Corrected
}
public class Mark
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    public string CourseCode { get; set; } = string.Empty;

    public string CourseTitle { get; set; } = string.Empty;
    public int StudentId { get; set; }

    public string StudentRegistrationNumber { get; set; } = string.Empty;

    public string StudentFullName { get; set; } = string.Empty;
    public string LecturerName { get; set; } = string.Empty;

    public AssessmentType AssessmentType { get; set; }
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
    public int MaxScore { get; set; }
    public double Score { get; set; }
    public double Percentage => MaxScore > 0 ? Math.Round(Score / MaxScore * 100, 1) : 0;
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
    public string Remarks { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? PublishedOn { get; set; }
}
