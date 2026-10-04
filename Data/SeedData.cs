using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

// =============================================================
//  CSAS — STATIC IN-MEMORY SEED DATA
//  All domain seed data lives here except Courses (CourseData.cs)
//  Models live in: Models/
// =============================================================


// ─────────────────────────────────────────────────────────────
//  STUDENTS
// ─────────────────────────────────────────────────────────────

public static class StudentData
{
    public static List<Student> Students { get; } = new()
    {
        new Student
        {
            Id                   = 1,
            RegistrationNumber   = "UNILAK/2024/001",
            FirstName            = "Jane",
            LastName             = "Uwase",
            Email                = "jane.uwase@unilak.ac.rw",
            Year                 = 3,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 92.0,
            EnrolledOn           = new DateTime(2024, 9, 1),
            EnrolledCourseIds    = new List<int> { 1, 2, 3, 4 }
        },
        new Student
        {
            Id                   = 2,
            RegistrationNumber   = "UNILAK/2024/002",
            FirstName            = "Eric",
            LastName             = "Nshimiye",
            Email                = "eric.nshimiye@unilak.ac.rw",
            Year                 = 3,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 88.0,
            EnrolledOn           = new DateTime(2024, 9, 1),
            EnrolledCourseIds    = new List<int> { 1, 2, 3, 4 }
        },
        new Student
        {
            Id                   = 3,
            RegistrationNumber   = "UNILAK/2024/003",
            FirstName            = "Alice",
            LastName             = "Mutesi",
            Email                = "alice.mutesi@unilak.ac.rw",
            Year                 = 3,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 74.0,
            EnrolledOn           = new DateTime(2024, 9, 1),
            EnrolledCourseIds    = new List<int> { 1, 2, 3, 4 }
        },
        new Student
        {
            Id                   = 4,
            RegistrationNumber   = "UNILAK/2024/005",
            FirstName            = "Patrick",
            LastName             = "Habimana",
            Email                = "patrick.habimana@unilak.ac.rw",
            Year                 = 4,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 96.0,
            EnrolledOn           = new DateTime(2023, 9, 1),
            EnrolledCourseIds    = new List<int> { 4 }
        },
        new Student
        {
            Id                   = 5,
            RegistrationNumber   = "UNILAK/2024/006",
            FirstName            = "Grace",
            LastName             = "Iradukunda",
            Email                = "grace.iradukunda@unilak.ac.rw",
            Year                 = 4,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 90.0,
            EnrolledOn           = new DateTime(2023, 9, 1),
            EnrolledCourseIds    = new List<int> { 4 }
        },
        new Student
        {
            Id                   = 6,
            RegistrationNumber   = "UNILAK/2024/007",
            FirstName            = "Jean",
            LastName             = "Nsanzimana",
            Email                = "jean.nsanzimana@unilak.ac.rw",
            Year                 = 4,
            Semester             = 1,
            Department           = "Computer Science & IT",
            Programme            = "BIT",
            AttendanceRate       = 65.0,
            EnrolledOn           = new DateTime(2023, 9, 1),
            EnrolledCourseIds    = new List<int> { 4 }
        }
    };
}


// ─────────────────────────────────────────────────────────────
//  USERS  (one per role for demo / login purposes)
//
//  Demo passwords (plain-text for dev — swap for bcrypt in prod):
//  ┌─────────────────────────────────┬──────────────────────┬──────────────┐
//  │ Role                            │ Login Identifier     │ Password     │
//  ├─────────────────────────────────┼──────────────────────┼──────────────┤
//  │ Student                         │ UNILAK/2024/001      │ student123   │
//  │ Class Representative            │ UNILAK/2024/002      │ classrep123  │
//  │ Lecturer                        │ STAFF/001            │ lecturer123  │
//  │ Head of Department              │ STAFF/010            │ hod123       │
//  │ Dean                            │ STAFF/020            │ dean123      │
//  │ Director of Quality             │ STAFF/030            │ quality123   │
//  └─────────────────────────────────┴──────────────────────┴──────────────┘
//  You can also use the email address instead of the identifier.
// ─────────────────────────────────────────────────────────────

public static class UserData
{
    public static List<User> Users { get; } = new()
    {
        // Student
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

        // Class Representative
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

        // Lecturer
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

        // Head of Department
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

        // Dean
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

        // Director of Quality
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
    };
}


// ─────────────────────────────────────────────────────────────
//  MARKS
// ─────────────────────────────────────────────────────────────

public static class MarkData
{
    public static List<Mark> Marks { get; } = new()
    {
        // ── CSE301 – Software Engineering ──────────────────

        new Mark
        {
            Id                         = 1,
            CourseId                   = 1,
            CourseCode                 = "CSE301",
            CourseTitle                = "Software Engineering",
            StudentId                  = 1,
            StudentRegistrationNumber  = "UNILAK/2024/001",
            StudentFullName            = "Jane Uwase",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 26,
            Status                     = MarkStatus.Published,
            Remarks                    = "Excellent work on requirements section.",
            CreatedOn                  = new DateTime(2026, 9, 20),
            PublishedOn                = new DateTime(2026, 9, 21)
        },
        new Mark
        {
            Id                         = 2,
            CourseId                   = 1,
            CourseCode                 = "CSE301",
            CourseTitle                = "Software Engineering",
            StudentId                  = 1,
            StudentRegistrationNumber  = "UNILAK/2024/001",
            StudentFullName            = "Jane Uwase",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.Assignment1,
            MaxScore                   = 20,
            Score                      = 18,
            Status                     = MarkStatus.Published,
            CreatedOn                  = new DateTime(2026, 9, 25),
            PublishedOn                = new DateTime(2026, 9, 25)
        },
        new Mark
        {
            Id                         = 3,
            CourseId                   = 1,
            CourseCode                 = "CSE301",
            CourseTitle                = "Software Engineering",
            StudentId                  = 2,
            StudentRegistrationNumber  = "UNILAK/2024/002",
            StudentFullName            = "Eric Nshimiye",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 24,
            Status                     = MarkStatus.Published,
            CreatedOn                  = new DateTime(2026, 9, 20),
            PublishedOn                = new DateTime(2026, 9, 21)
        },
        new Mark
        {
            Id                         = 4,
            CourseId                   = 1,
            CourseCode                 = "CSE301",
            CourseTitle                = "Software Engineering",
            StudentId                  = 3,
            StudentRegistrationNumber  = "UNILAK/2024/003",
            StudentFullName            = "Alice Mutesi",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 17,
            Status                     = MarkStatus.Draft,
            Remarks                    = "Needs improvement on UML section.",
            CreatedOn                  = new DateTime(2026, 9, 20)
        },

        // ── CSE305 – Database Systems ──────────────────────

        new Mark
        {
            Id                         = 5,
            CourseId                   = 2,
            CourseCode                 = "CSE305",
            CourseTitle                = "Database Systems",
            StudentId                  = 1,
            StudentRegistrationNumber  = "UNILAK/2024/001",
            StudentFullName            = "Jane Uwase",
            LecturerName               = "Mrs. A. Byukusenge",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 19,
            Status                     = MarkStatus.Published,
            CreatedOn                  = new DateTime(2026, 9, 22),
            PublishedOn                = new DateTime(2026, 9, 23)
        },

        // ── CSE402 – Software Architecture ────────────────

        new Mark
        {
            Id                         = 6,
            CourseId                   = 4,
            CourseCode                 = "CSE402",
            CourseTitle                = "Software Architecture",
            StudentId                  = 4,
            StudentRegistrationNumber  = "UNILAK/2024/005",
            StudentFullName            = "Patrick Habimana",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 28,
            Status                     = MarkStatus.Published,
            CreatedOn                  = new DateTime(2026, 9, 18),
            PublishedOn                = new DateTime(2026, 9, 19)
        },
        new Mark
        {
            Id                         = 7,
            CourseId                   = 4,
            CourseCode                 = "CSE402",
            CourseTitle                = "Software Architecture",
            StudentId                  = 6,
            StudentRegistrationNumber  = "UNILAK/2024/007",
            StudentFullName            = "Jean Nsanzimana",
            LecturerName               = "Dr. J. Mugisha",
            AssessmentType             = AssessmentType.CAT1,
            MaxScore                   = 30,
            Score                      = 12,
            Status                     = MarkStatus.Draft,
            Remarks                    = "Did not attempt section C.",
            CreatedOn                  = new DateTime(2026, 9, 18)
        }
    };
}


// ─────────────────────────────────────────────────────────────
//  CLAIMS
// ─────────────────────────────────────────────────────────────

public static class ClaimData
{
    public static List<Claim> Claims { get; } = new()
    {
        new Claim
        {
            Id                         = 1,
            MarkId                     = 5,
            StudentId                  = 1,
            StudentFullName            = "Jane Uwase",
            StudentRegistrationNumber  = "UNILAK/2024/001",
            CourseCode                 = "CSE305",
            CourseTitle                = "Database Systems",
            AssessmentLabel            = "CAT 1",
            LecturerName               = "Mrs. A. Byukusenge",
            Reason                     = "I believe my answer to Q3 was partially correct but was marked zero.",
            Status                     = ClaimStatus.UnderReview,
            LecturerResponse           = string.Empty,
            RaisedOn                   = new DateTime(2026, 10, 1)
        }
    };
}


// ─────────────────────────────────────────────────────────────
//  SESSION REPORTS
// ─────────────────────────────────────────────────────────────

public static class SessionReportData
{
    public static List<SessionReport> Reports { get; } = new()
    {
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
            Venue                   = "Block A – Room 204",
            Status                  = SessionReportStatus.DeanApproved,
            SubmittedOn             = new DateTime(2026, 9, 29, 10, 30, 0),
            CPSignedOffOn           = new DateTime(2026, 9, 29, 11, 0, 0),
            CPNotes                 = "Session confirmed. Full attendance.",
            HODApprovedOn           = new DateTime(2026, 9, 30, 9, 0, 0),
            HODNotes                = "Approved.",
            DeanApprovedOn          = new DateTime(2026, 9, 30, 14, 0, 0),
            DeanNotes               = "Final approval granted."
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
            Venue                   = "Block A – Room 204",
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
            Venue                   = "Block B – Room 101",
            Status                  = SessionReportStatus.CPSignedOff,
            SubmittedOn             = new DateTime(2026, 10, 2, 12, 30, 0),
            CPSignedOffOn           = new DateTime(2026, 10, 2, 14, 0, 0),
            CPNotes                 = "Session confirmed. Good attendance."
        },
        new SessionReport
        {
            Id                      = 6,
            CourseId                = 3,
            CourseCode              = "CSE310",
            CourseTitle             = "Human-Computer Interaction",
            LecturerName            = "Mr. E. Niyonsenga",
            ClassRepresentativeName = "Eric Nshimiye",
            SessionDate             = new DateTime(2026, 10, 1, 13, 0, 0),
            DurationHours           = 2,
            TopicCovered            = "Usability Testing Methods",
            Venue                   = "Block A – Room 108",
            Status                  = SessionReportStatus.CPSignedOff,
            SubmittedOn             = new DateTime(2026, 10, 1, 15, 30, 0),
            CPSignedOffOn           = new DateTime(2026, 10, 1, 16, 30, 0),
            CPNotes                 = "Session delivered. 35 students attended."
        },
        new SessionReport
        {
            Id                      = 7,
            CourseId                = 1,
            CourseCode              = "CSE301",
            CourseTitle             = "Software Engineering",
            LecturerName            = "Dr. J. Mugisha",
            ClassRepresentativeName = "Eric Nshimiye",
            SessionDate             = new DateTime(2026, 10, 2, 8, 0, 0),
            DurationHours           = 2,
            TopicCovered            = "Design Patterns",
            Venue                   = "Block A – Room 204",
            Status                  = SessionReportStatus.Draft
        },
        new SessionReport
        {
            Id                      = 4,
            CourseId                = 4,
            CourseCode              = "CSE402",
            CourseTitle             = "Software Architecture",
            LecturerName            = "Dr. J. Mugisha",
            ClassRepresentativeName = "Patrick Habimana",
            SessionDate             = new DateTime(2026, 9, 30, 14, 0, 0),
            DurationHours           = 2,
            TopicCovered            = "Microservices Architecture",
            Venue                   = "Block C – Room 302",
            Status                  = SessionReportStatus.DeanApproved,
            SubmittedOn             = new DateTime(2026, 9, 30, 16, 0, 0),
            CPSignedOffOn           = new DateTime(2026, 9, 30, 17, 0, 0),
            CPNotes                 = "Confirmed. All students attended.",
            HODApprovedOn           = new DateTime(2026, 10, 1, 8, 30, 0),
            HODNotes                = "Good session.",
            DeanApprovedOn          = new DateTime(2026, 10, 1, 11, 0, 0),
            DeanNotes               = "Approved."
        },
        new SessionReport
        {
            Id                      = 5,
            CourseId                = 4,
            CourseCode              = "CSE402",
            CourseTitle             = "Software Architecture",
            LecturerName            = "Dr. J. Mugisha",
            ClassRepresentativeName = "Patrick Habimana",
            SessionDate             = new DateTime(2026, 9, 25, 8, 0, 0),
            DurationHours           = 2,
            TopicCovered            = "Event-Driven Systems",
            Venue                   = "Block C – Room 302",
            Status                  = SessionReportStatus.DeanApproved,
            SubmittedOn             = new DateTime(2026, 9, 25, 10, 0, 0),
            CPSignedOffOn           = new DateTime(2026, 9, 25, 11, 0, 0),
            HODApprovedOn           = new DateTime(2026, 9, 26, 9, 0, 0),
            DeanApprovedOn          = new DateTime(2026, 9, 26, 14, 0, 0)
        }
    };
}


// ─────────────────────────────────────────────────────────────
//  ROOMS
//  All physical venues on campus.
//  BookedSlots: key = schedule time string, value = course code.
// ─────────────────────────────────────────────────────────────

public static class RoomData
{
    public static List<Room> Rooms { get; } = new()
    {
        // ── Block A ──────────────────────────────────────────

        new Room
        {
            Id          = 1,
            Block       = "Block A",
            Number      = "Room 101",
            Capacity    = 60,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = ""
        },
        new Room
        {
            Id          = 2,
            Block       = "Block A",
            Number      = "Room 108",
            Capacity    = 50,
            Type        = RoomType.Seminar,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>
            {
                { "Wed 13:00 - 15:00", "CSE310" }
            },
            Notes = "Projector installed — book early."
        },
        new Room
        {
            Id          = 3,
            Block       = "Block A",
            Number      = "Room 204",
            Capacity    = 80,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>
            {
                { "Mon 08:00 - 10:00", "CSE301" }
            },
            Notes = ""
        },
        new Room
        {
            Id          = 4,
            Block       = "Block A",
            Number      = "Room 305",
            Capacity    = 45,
            Type        = RoomType.Seminar,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "Air conditioning under repair."
        },

        // ── Block B ──────────────────────────────────────────

        new Room
        {
            Id          = 5,
            Block       = "Block B",
            Number      = "Room 101",
            Capacity    = 70,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>
            {
                { "Tue 10:00 - 12:00", "CSE305" }
            },
            Notes = ""
        },
        new Room
        {
            Id          = 6,
            Block       = "Block B",
            Number      = "Room 202",
            Capacity    = 55,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = ""
        },
        new Room
        {
            Id          = 7,
            Block       = "Block B",
            Number      = "Lab 01",
            Capacity    = 40,
            Type        = RoomType.Laboratory,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "32 workstations, Linux/Windows dual-boot."
        },
        new Room
        {
            Id          = 8,
            Block       = "Block B",
            Number      = "Lab 02",
            Capacity    = 36,
            Type        = RoomType.ComputerLab,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "30 PCs — networking lab."
        },

        // ── Block C ──────────────────────────────────────────

        new Room
        {
            Id          = 9,
            Block       = "Block C",
            Number      = "Room 201",
            Capacity    = 65,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = ""
        },
        new Room
        {
            Id          = 10,
            Block       = "Block C",
            Number      = "Room 302",
            Capacity    = 75,
            Type        = RoomType.Lecture,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>
            {
                { "Thu 08:00 - 10:00", "CSE402" }
            },
            Notes = ""
        },
        new Room
        {
            Id          = 11,
            Block       = "Block C",
            Number      = "Room 401",
            Capacity    = 100,
            Type        = RoomType.Auditorium,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "Large auditorium — for seminars and exams."
        },

        // ── Block D ──────────────────────────────────────────

        new Room
        {
            Id          = 12,
            Block       = "Block D",
            Number      = "Room 101",
            Capacity    = 50,
            Type        = RoomType.Lecture,
            IsAvailable = false,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "Under renovation until end of semester."
        },
        new Room
        {
            Id          = 13,
            Block       = "Block D",
            Number      = "Lab 03",
            Capacity    = 38,
            Type        = RoomType.ComputerLab,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "35 PCs — software development lab."
        },
        new Room
        {
            Id          = 14,
            Block       = "Block D",
            Number      = "Lab 04",
            Capacity    = 30,
            Type        = RoomType.Laboratory,
            IsAvailable = true,
            BookedSlots = new Dictionary<string, string>(),
            Notes       = "Electronics and hardware lab."
        }
    };
}


// ─────────────────────────────────────────────────────────────
//  ESCALATIONS (Quality Director → Dean)
// ─────────────────────────────────────────────────────────────

public static class EscalationData
{
    public static List<EscalationRecord> Escalations { get; } = new();
}
