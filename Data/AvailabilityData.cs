using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

public static class AvailabilityData
{
    private static readonly string[] Days      = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    private static readonly string[] TimeSlots = { "08:00 - 10:00", "10:00 - 12:00", "13:00 - 15:00", "15:00 - 17:00" };

    public static List<TeacherAvailability> Availabilities { get; } = new()
    {
        // Dr. J. Mugisha — unavailable Wednesday afternoon (research)
        new TeacherAvailability { Id=1,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Monday",    TimeSlot="08:00 - 10:00", IsAvailable=true },
        new TeacherAvailability { Id=2,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Monday",    TimeSlot="10:00 - 12:00", IsAvailable=true },
        new TeacherAvailability { Id=3,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Monday",    TimeSlot="13:00 - 15:00", IsAvailable=true },
        new TeacherAvailability { Id=4,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Tuesday",   TimeSlot="08:00 - 10:00", IsAvailable=true },
        new TeacherAvailability { Id=5,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Tuesday",   TimeSlot="13:00 - 15:00", IsAvailable=true },
        new TeacherAvailability { Id=6,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Wednesday", TimeSlot="08:00 - 10:00", IsAvailable=true },
        new TeacherAvailability { Id=7,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Wednesday", TimeSlot="13:00 - 15:00", IsAvailable=false, UnavailableNote="Research meeting" },
        new TeacherAvailability { Id=8,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Thursday",  TimeSlot="08:00 - 10:00", IsAvailable=true },
        new TeacherAvailability { Id=9,  LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Thursday",  TimeSlot="10:00 - 12:00", IsAvailable=true },
        new TeacherAvailability { Id=10, LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Friday",    TimeSlot="08:00 - 10:00", IsAvailable=true },
        new TeacherAvailability { Id=11, LecturerEmail="j.mugisha@unilak.ac.rw", LecturerName="Dr. J. Mugisha",      Day="Friday",    TimeSlot="10:00 - 12:00", IsAvailable=true },
    };

    /// <summary>Returns all time slots a lecturer is available for, in SlotKey format.</summary>
    public static List<string> GetAvailableSlots(string lecturerEmail)
        => Availabilities
            .Where(a => a.LecturerEmail.Equals(lecturerEmail, StringComparison.OrdinalIgnoreCase) && a.IsAvailable)
            .Select(a => a.SlotKey)
            .ToList();

    /// <summary>Returns all days and time slots (for building the availability grid).</summary>
    public static string[] AllDays      => Days;
    public static string[] AllTimeSlots => TimeSlots;

    /// <summary>Checks if a lecturer is available for a specific slot key.</summary>
    public static bool IsLecturerAvailable(string lecturerEmail, string slotKey)
    {
        var avail = Availabilities.FirstOrDefault(a =>
            a.LecturerEmail.Equals(lecturerEmail, StringComparison.OrdinalIgnoreCase) &&
            a.SlotKey.Equals(slotKey, StringComparison.OrdinalIgnoreCase));
        return avail == null || avail.IsAvailable;
    }
}

public static class TimetableRevisionData
{
    public static List<TimetableRevision> Revisions { get; } = new();

    /// <summary>Records a timetable change. Call this when HOD updates a course's schedule/venue/lecturer.</summary>
    public static void Record(int courseId, string courseCode, string courseTitle,
        string field, string oldValue, string newValue, string changedBy, string role, string reason = "")
    {
        Revisions.Add(new TimetableRevision
        {
            Id            = Revisions.Count > 0 ? Revisions.Max(r => r.Id) + 1 : 1,
            CourseId      = courseId,
            CourseCode    = courseCode,
            CourseTitle   = courseTitle,
            FieldChanged  = field,
            OldValue      = oldValue,
            NewValue      = newValue,
            ChangedBy     = changedBy,
            ChangedByRole = role,
            ChangedOn     = DateTime.Now,
            Reason        = reason
        });
    }
}
