using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

public static class ExamData
{
    public static List<ExamTimetable> Exams { get; } = new()
    {
        // ── Regular Exams ──────────────────────────────────────────────────
        new ExamTimetable
        {
            Id          = 1,
            CourseCode  = "CSE301",
            CourseTitle = "Software Engineering",
            Department  = "Computer Science & IT",
            Year        = 3, Semester = 1,
            Type        = ExamType.Regular,
            ExamDate    = new DateTime(2026, 11, 10),
            StartTime   = "08:00", EndTime = "11:00",
            Venue       = "Block C – Room 401",
            Invigilator = "Dr. J. Mugisha",
            IsPublished = true,
            PublishedOn = DateTime.Now.AddDays(-2)
        },
        new ExamTimetable
        {
            Id          = 2,
            CourseCode  = "CSE305",
            CourseTitle = "Database Systems",
            Department  = "Computer Science & IT",
            Year        = 3, Semester = 1,
            Type        = ExamType.Regular,
            ExamDate    = new DateTime(2026, 11, 12),
            StartTime   = "13:00", EndTime = "16:00",
            Venue       = "Block C – Room 401",
            Invigilator = "Mrs. A. Byukusenge",
            IsPublished = true,
            PublishedOn = DateTime.Now.AddDays(-2)
        },
        new ExamTimetable
        {
            Id          = 3,
            CourseCode  = "CSE310",
            CourseTitle = "Human-Computer Interaction",
            Department  = "Computer Science & IT",
            Year        = 3, Semester = 1,
            Type        = ExamType.Regular,
            ExamDate    = new DateTime(2026, 11, 14),
            StartTime   = "08:00", EndTime = "11:00",
            Venue       = "Block A – Room 204",
            Invigilator = "Mr. E. Niyonsenga",
            IsPublished = true,
            PublishedOn = DateTime.Now.AddDays(-1)
        },
        new ExamTimetable
        {
            Id          = 4,
            CourseCode  = "CSE402",
            CourseTitle = "Software Architecture",
            Department  = "Computer Science & IT",
            Year        = 4, Semester = 1,
            Type        = ExamType.Regular,
            ExamDate    = new DateTime(2026, 11, 17),
            StartTime   = "13:00", EndTime = "16:00",
            Venue       = "Block C – Room 401",
            Invigilator = "Dr. P. Habimana",
            IsPublished = true,
            PublishedOn = DateTime.Now.AddDays(-1)
        },

        // ── Special Exams ──────────────────────────────────────────────────
        new ExamTimetable
        {
            Id          = 5,
            CourseCode  = "CSE305",
            CourseTitle = "Database Systems",
            Department  = "Computer Science & IT",
            Year        = 3, Semester = 1,
            Type        = ExamType.Special,
            ExamDate    = new DateTime(2026, 11, 20),
            StartTime   = "09:00", EndTime = "12:00",
            Venue       = "Block B – Room 101",
            Invigilator = "Mrs. A. Byukusenge",
            Notes       = "Special exam for students who missed the regular exam with valid excuse.",
            IsPublished = true,
            PublishedOn = DateTime.Now
        },
        new ExamTimetable
        {
            Id          = 6,
            CourseCode  = "CSE301",
            CourseTitle = "Software Engineering",
            Department  = "Computer Science & IT",
            Year        = 3, Semester = 1,
            Type        = ExamType.Supplementary,
            ExamDate    = new DateTime(2026, 11, 22),
            StartTime   = "08:00", EndTime = "11:00",
            Venue       = "Block A – Room 108",
            Invigilator = "Dr. J. Mugisha",
            Notes       = "Supplementary exam for students who failed the regular exam.",
            IsPublished = false
        },
        new ExamTimetable
        {
            Id          = 7,
            CourseCode  = "CSE402",
            CourseTitle = "Software Architecture",
            Department  = "Computer Science & IT",
            Year        = 4, Semester = 1,
            Type        = ExamType.Retake,
            ExamDate    = new DateTime(2026, 11, 25),
            StartTime   = "13:00", EndTime = "16:00",
            Venue       = "Block B – Room 202",
            Invigilator = "Dr. P. Habimana",
            Notes       = "Retake exam — only registered students allowed.",
            IsPublished = false
        },
    };
}
