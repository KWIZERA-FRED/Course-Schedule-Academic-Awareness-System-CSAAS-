namespace CourseScheduleSystem.Web.Models;

public enum ExamType
{
    Regular,
    Special,
    Supplementary,
    Retake
}

public class ExamTimetable
{
    public int      Id             { get; set; }
    public string   CourseCode     { get; set; } = string.Empty;
    public string   CourseTitle    { get; set; } = string.Empty;
    public string   Department     { get; set; } = string.Empty;
    public int      Year           { get; set; }
    public int      Semester       { get; set; }
    public ExamType Type           { get; set; } = ExamType.Regular;
    public DateTime ExamDate       { get; set; }
    public string   StartTime      { get; set; } = string.Empty;
    public string   EndTime        { get; set; } = string.Empty;
    public string   Venue          { get; set; } = string.Empty;
    public string   Invigilator    { get; set; } = string.Empty;
    public string   Notes          { get; set; } = string.Empty;
    public bool     IsPublished    { get; set; } = false;
    public DateTime CreatedOn      { get; set; } = DateTime.Now;
    public DateTime? PublishedOn   { get; set; }

    public string Duration => $"{StartTime} – {EndTime}";
    public string ExamDateFormatted => ExamDate.ToString("ddd dd MMM yyyy");
}
