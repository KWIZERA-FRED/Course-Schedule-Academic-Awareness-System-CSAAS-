using Microsoft.EntityFrameworkCore;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;
public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.Migrate();
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Id           = 1,
                    FirstName    = "Jane",
                    LastName     = "Uwase",
                    Email        = "jane.uwase@unilak.ac.rw",
                    Identifier   = "UNILAK/2024/001",
                    PasswordHash = "student123",
                    Role         = UserRole.Student,
                    Department   = "Computer Science & IT",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2024, 9, 1)
                },
                new User
                {
                    Id           = 2,
                    FirstName    = "Eric",
                    LastName     = "Nshimiye",
                    Email        = "eric.nshimiye@unilak.ac.rw",
                    Identifier   = "UNILAK/2024/002",
                    PasswordHash = "classrep123",
                    Role         = UserRole.ClassRepresentative,
                    Department   = "Computer Science & IT",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2024, 9, 1)
                },
                new User
                {
                    Id           = 3,
                    FirstName    = "Jean",
                    LastName     = "Mugisha",
                    Email        = "j.mugisha@unilak.ac.rw",
                    Identifier   = "STAFF/001",
                    PasswordHash = "lecturer123",
                    Role         = UserRole.Lecturer,
                    Department   = "Computer Science & IT",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2022, 1, 15)
                },
                new User
                {
                    Id           = 4,
                    FirstName    = "Amina",
                    LastName     = "Uwimana",
                    Email        = "a.uwimana@unilak.ac.rw",
                    Identifier   = "STAFF/010",
                    PasswordHash = "hod123",
                    Role         = UserRole.HOD,
                    Department   = "Computer Science & IT",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2020, 3, 1)
                },
                new User
                {
                    Id           = 5,
                    FirstName    = "Claude",
                    LastName     = "Ndayisaba",
                    Email        = "c.ndayisaba@unilak.ac.rw",
                    Identifier   = "STAFF/020",
                    PasswordHash = "dean123",
                    Role         = UserRole.Dean,
                    Department   = "Faculty of Engineering & Technology",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2019, 6, 1)
                },
                new User
                {
                    Id           = 6,
                    FirstName    = "Marie",
                    LastName     = "Ingabire",
                    Email        = "m.ingabire@unilak.ac.rw",
                    Identifier   = "STAFF/030",
                    PasswordHash = "quality123",
                    Role         = UserRole.DirectorOfQuality,
                    Department   = "Academic Quality Directorate",
                    IsActive     = true,
                    CreatedOn    = new DateTime(2018, 8, 1)
                }
            );
            context.SaveChanges();
        }
        if (!context.Courses.Any())
        {
            context.Courses.AddRange(
                new Course
                {
                    Id               = 1,
                    Code             = "CSE301",
                    Title            = "Software Engineering",
                    Lecturer         = "Dr. J. Mugisha",
                    ScheduleTime     = "Mon 08:00 - 10:00",
                    Venue            = "Block A - Room 204",
                    WhatsappGroupUrl = "",
                    JoinDeadline     = DateTime.Now.AddDays(5),
                    ScheduledHours   = 36,
                    DeliveredHours   = 34,
                    Department       = "Computer Science & IT",
                    Year             = 3,
                    Semester         = 1
                },
                new Course
                {
                    Id               = 2,
                    Code             = "CSE305",
                    Title            = "Database Systems",
                    Lecturer         = "Mrs. A. Byukusenge",
                    ScheduleTime     = "Tue 10:00 - 12:00",
                    Venue            = "Block B - Room 101",
                    WhatsappGroupUrl = "",
                    JoinDeadline     = DateTime.Now.AddDays(3),
                    ScheduledHours   = 36,
                    DeliveredHours   = 30,
                    Department       = "Computer Science & IT",
                    Year             = 3,
                    Semester         = 1
                },
                new Course
                {
                    Id               = 3,
                    Code             = "CSE310",
                    Title            = "Human-Computer Interaction",
                    Lecturer         = "Mr. E. Niyonsenga",
                    ScheduleTime     = "Wed 13:00 - 15:00",
                    Venue            = "Block A - Room 108",
                    WhatsappGroupUrl = "",
                    JoinDeadline     = DateTime.Now.AddDays(-1),
                    ScheduledHours   = 36,
                    DeliveredHours   = 22,
                    Department       = "Computer Science & IT",
                    Year             = 3,
                    Semester         = 1
                },
                new Course
                {
                    Id               = 4,
                    Code             = "CSE402",
                    Title            = "Software Architecture",
                    Lecturer         = "Dr. P. Habimana",
                    ScheduleTime     = "Thu 08:00 - 10:00",
                    Venue            = "Block C - Room 302",
                    WhatsappGroupUrl = "",
                    JoinDeadline     = DateTime.Now.AddDays(7),
                    ScheduledHours   = 36,
                    DeliveredHours   = 36,
                    Department       = "Computer Science & IT",
                    Year             = 4,
                    Semester         = 1
                }
            );
            context.SaveChanges();
        }
        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new Student
                {
                    Id                 = 1,
                    RegistrationNumber = "UNILAK/2024/001",
                    FirstName          = "Jane",
                    LastName           = "Uwase",
                    Email              = "jane.uwase@unilak.ac.rw",
                    Year               = 3,
                    Semester           = 1,
                    Department         = "Computer Science & IT",
                    Programme          = "BIT",
                    AttendanceRate     = 92.0,
                    EnrolledOn         = new DateTime(2024, 9, 1)
                },
                new Student
                {
                    Id                 = 2,
                    RegistrationNumber = "UNILAK/2024/002",
                    FirstName          = "Eric",
                    LastName           = "Nshimiye",
                    Email              = "eric.nshimiye@unilak.ac.rw",
                    Year               = 3,
                    Semester           = 1,
                    Department         = "Computer Science & IT",
                    Programme          = "BIT",
                    AttendanceRate     = 88.0,
                    EnrolledOn         = new DateTime(2024, 9, 1)
                }
            );
            context.SaveChanges();
        }
        if (!context.Rooms.Any())
        {
            context.Rooms.AddRange(
                new Room { Id = 1, Block = "Block A", Number = "Room 101", Capacity = 60,  Type = RoomType.Lecture,     IsAvailable = true,  Notes = "" },
                new Room { Id = 2, Block = "Block A", Number = "Room 108", Capacity = 50,  Type = RoomType.Seminar,     IsAvailable = true,  Notes = "Projector installed." },
                new Room { Id = 3, Block = "Block A", Number = "Room 204", Capacity = 80,  Type = RoomType.Lecture,     IsAvailable = true,  Notes = "" },
                new Room { Id = 4, Block = "Block B", Number = "Room 101", Capacity = 70,  Type = RoomType.Lecture,     IsAvailable = true,  Notes = "" },
                new Room { Id = 5, Block = "Block B", Number = "Lab 01",   Capacity = 40,  Type = RoomType.Laboratory,  IsAvailable = true,  Notes = "32 workstations." },
                new Room { Id = 6, Block = "Block B", Number = "Lab 02",   Capacity = 36,  Type = RoomType.ComputerLab, IsAvailable = true,  Notes = "Networking lab." },
                new Room { Id = 7, Block = "Block C", Number = "Room 201", Capacity = 65,  Type = RoomType.Lecture,     IsAvailable = true,  Notes = "" },
                new Room { Id = 8, Block = "Block C", Number = "Room 302", Capacity = 75,  Type = RoomType.Lecture,     IsAvailable = true,  Notes = "" },
                new Room { Id = 9, Block = "Block D", Number = "Lab 03",   Capacity = 38,  Type = RoomType.ComputerLab, IsAvailable = true,  Notes = "Software dev lab." },
                new Room { Id = 10,Block = "Block D", Number = "Room 101", Capacity = 50,  Type = RoomType.Lecture,     IsAvailable = false, Notes = "Under renovation." }
            );
            context.SaveChanges();
        }
        if (!context.SessionReports.Any())
        {
            context.SessionReports.AddRange(
                new SessionReport
                {
                    Id                      = 1,
                    CourseId                = 1,
                    CourseCode              = "CSE301",
                    CourseTitle             = "Software Engineering",
                    LecturerName            = "Dr. J. Mugisha",
                    ClassRepresentativeName = "Eric Nshimiye",
                    SessionDate             = new DateTime(2026, 9, 29, 8, 0, 0),
                    DurationHours           = 2,
                    TopicCovered            = "Requirements Engineering",
                    Venue                   = "Block A - Room 204",
                    Status                  = SessionReportStatus.DeanApproved,
                    SubmittedOn             = new DateTime(2026, 9, 29, 10, 30, 0),
                    CPSignedOffOn           = new DateTime(2026, 9, 29, 11, 0, 0),
                    CPNotes                 = "Session confirmed. Full attendance.",
                    HODApprovedOn           = new DateTime(2026, 9, 30, 9, 0, 0),
                    DeanApprovedOn          = new DateTime(2026, 9, 30, 14, 0, 0)
                },
                new SessionReport
                {
                    Id                      = 2,
                    CourseId                = 1,
                    CourseCode              = "CSE301",
                    CourseTitle             = "Software Engineering",
                    LecturerName            = "Dr. J. Mugisha",
                    ClassRepresentativeName = "Eric Nshimiye",
                    SessionDate             = new DateTime(2026, 10, 1, 10, 0, 0),
                    DurationHours           = 2,
                    TopicCovered            = "UML Diagrams & Use Cases",
                    Venue                   = "Block A - Room 204",
                    Status                  = SessionReportStatus.CPSignedOff,
                    SubmittedOn             = new DateTime(2026, 10, 1, 12, 0, 0),
                    CPSignedOffOn           = new DateTime(2026, 10, 1, 15, 0, 0),
                    CPNotes                 = "Session delivered. 38 students present."
                },
                new SessionReport
                {
                    Id                      = 3,
                    CourseId                = 2,
                    CourseCode              = "CSE305",
                    CourseTitle             = "Database Systems",
                    LecturerName            = "Mrs. A. Byukusenge",
                    ClassRepresentativeName = "Eric Nshimiye",
                    SessionDate             = new DateTime(2026, 10, 2, 10, 0, 0),
                    DurationHours           = 2,
                    TopicCovered            = "SQL Joins & Subqueries",
                    Venue                   = "Block B - Room 101",
                    Status                  = SessionReportStatus.CPSignedOff,
                    SubmittedOn             = new DateTime(2026, 10, 2, 12, 30, 0),
                    CPSignedOffOn           = new DateTime(2026, 10, 2, 14, 0, 0),
                    CPNotes                 = "Session confirmed. Good attendance."
                }
            );
            context.SaveChanges();
        }
        if (!context.Marks.Any())
        {
            context.Marks.AddRange(
                new Mark
                {
                    Id                        = 1,
                    CourseId                  = 1,
                    CourseCode                = "CSE301",
                    CourseTitle               = "Software Engineering",
                    StudentId                 = 1,
                    StudentRegistrationNumber = "UNILAK/2024/001",
                    StudentFullName           = "Jane Uwase",
                    LecturerName              = "Dr. J. Mugisha",
                    AssessmentType            = AssessmentType.CAT1,
                    MaxScore                  = 30,
                    Score                     = 26,
                    Status                    = MarkStatus.Published,
                    CreatedOn                 = new DateTime(2026, 9, 20),
                    PublishedOn               = new DateTime(2026, 9, 21)
                },
                new Mark
                {
                    Id                        = 2,
                    CourseId                  = 2,
                    CourseCode                = "CSE305",
                    CourseTitle               = "Database Systems",
                    StudentId                 = 1,
                    StudentRegistrationNumber = "UNILAK/2024/001",
                    StudentFullName           = "Jane Uwase",
                    LecturerName              = "Mrs. A. Byukusenge",
                    AssessmentType            = AssessmentType.CAT1,
                    MaxScore                  = 30,
                    Score                     = 19,
                    Status                    = MarkStatus.Published,
                    CreatedOn                 = new DateTime(2026, 9, 22),
                    PublishedOn               = new DateTime(2026, 9, 23)
                }
            );
            context.SaveChanges();
        }
        if (!context.Claims.Any())
        {
            context.Claims.Add(new Claim
            {
                Id                        = 1,
                MarkId                    = 2,
                StudentId                 = 1,
                StudentFullName           = "Jane Uwase",
                StudentRegistrationNumber = "UNILAK/2024/001",
                CourseCode                = "CSE305",
                CourseTitle               = "Database Systems",
                AssessmentLabel           = "CAT 1",
                LecturerName              = "Mrs. A. Byukusenge",
                Reason                    = "I believe my answer to Q3 was partially correct but was marked zero.",
                Status                    = ClaimStatus.UnderReview,
                RaisedOn                  = new DateTime(2026, 10, 1)
            });
            context.SaveChanges();
        }
    }
}
