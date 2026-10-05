using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;
public static class CourseData
{
    public static List<Course> Courses { get; } = new()
    {
        new Course
        {
            Id              = 1,
            Code            = "CSE301",
            Title           = "Software Engineering",
            Lecturer        = "Dr. J. Mugisha",
            ScheduleTime    = "Mon 08:00 - 10:00",
            Venue           = "Block A - Room 204",
            WhatsappGroupUrl = "https://chat.whatsapp.com/example1",
            JoinDeadline    = DateTime.Now.AddDays(5),
            ScheduledHours  = 36,
            DeliveredHours  = 34,
            Department      = "Computer Science & IT",
            Year            = 3,
            Semester        = 1
        },
        new Course
        {
            Id              = 2,
            Code            = "CSE305",
            Title           = "Database Systems",
            Lecturer        = "Mrs. A. Byukusenge",
            ScheduleTime    = "Tue 10:00 - 12:00",
            Venue           = "Block B - Room 101",
            WhatsappGroupUrl = "https://chat.whatsapp.com/example2",
            JoinDeadline    = DateTime.Now.AddDays(3),
            ScheduledHours  = 36,
            DeliveredHours  = 30,
            Department      = "Computer Science & IT",
            Year            = 3,
            Semester        = 1
        },
        new Course
        {
            Id              = 3,
            Code            = "CSE310",
            Title           = "Human-Computer Interaction",
            Lecturer        = "Mr. E. Niyonsenga",
            ScheduleTime    = "Wed 13:00 - 15:00",
            Venue           = "Block A - Room 108",
            WhatsappGroupUrl = "https://chat.whatsapp.com/example3",
            JoinDeadline    = DateTime.Now.AddDays(-1),
            ScheduledHours  = 36,
            DeliveredHours  = 22,
            Department      = "Computer Science & IT",
            Year            = 3,
            Semester        = 1
        },
        new Course
        {
            Id              = 4,
            Code            = "CSE402",
            Title           = "Software Architecture",
            Lecturer        = "Dr. P. Habimana",
            ScheduleTime    = "Thu 08:00 - 10:00",
            Venue           = "Block C - Room 302",
            WhatsappGroupUrl = "https://chat.whatsapp.com/example4",
            JoinDeadline    = DateTime.Now.AddDays(7),
            ScheduledHours  = 36,
            DeliveredHours  = 36,
            Department      = "Computer Science & IT",
            Year            = 4,
            Semester        = 1
        }
    };
}
