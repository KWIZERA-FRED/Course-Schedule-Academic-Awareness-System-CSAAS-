using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

public static class ChatData
{

    public static List<ChatMessage> Messages { get; } = new()
    {
        new ChatMessage
        {
            Id          = 1,
            HODEmail    = "a.uwimana@unilak.ac.rw",
            CPEmail     = "eric.nshimiye@unilak.ac.rw",
            SenderEmail = "a.uwimana@unilak.ac.rw",
            SenderName  = "Dr. Amina Uwimana",
            SenderRole  = "HOD",
            Text        = "Hello Eric, please make sure all session reports for this week are submitted on time.",
            SentAt      = DateTime.Now.AddHours(-3),
            IsRead      = true
        },
        new ChatMessage
        {
            Id          = 2,
            HODEmail    = "a.uwimana@unilak.ac.rw",
            CPEmail     = "eric.nshimiye@unilak.ac.rw",
            SenderEmail = "eric.nshimiye@unilak.ac.rw",
            SenderName  = "Eric Nshimiye",
            SenderRole  = "CP",
            Text        = "Understood Dr. Uwimana. I will follow up with all lecturers today.",
            SentAt      = DateTime.Now.AddHours(-2),
            IsRead      = true
        }
    };

    private static int _nextId = 3;

    public static ChatMessage AddMessage(
        string hodEmail, string cpEmail,
        string senderEmail, string senderName, string senderRole,
        string text)
    {
        var msg = new ChatMessage
        {
            Id          = _nextId++,
            HODEmail    = hodEmail,
            CPEmail     = cpEmail,
            SenderEmail = senderEmail,
            SenderName  = senderName,
            SenderRole  = senderRole,
            Text        = text.Trim(),
            SentAt      = DateTime.Now,
            IsRead      = false
        };
        Messages.Add(msg);
        return msg;
    }
    public static List<ChatMessage> GetConversation(string hodEmail, string cpEmail)
        => Messages
            .Where(m =>
                m.HODEmail.Equals(hodEmail, StringComparison.OrdinalIgnoreCase) &&
                m.CPEmail.Equals(cpEmail,  StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.SentAt)
            .ToList();
    public static void MarkRead(string hodEmail, string cpEmail, string readerRole)
    {
        foreach (var m in Messages.Where(m =>
            m.HODEmail.Equals(hodEmail, StringComparison.OrdinalIgnoreCase) &&
            m.CPEmail.Equals(cpEmail,   StringComparison.OrdinalIgnoreCase) &&
            !m.SenderRole.Equals(readerRole, StringComparison.OrdinalIgnoreCase)))
        {
            m.IsRead = true;
        }
    }
    public static int UnreadCount(string readerEmail, string readerRole)
        => Messages.Count(m =>
            !m.IsRead &&
            !m.SenderEmail.Equals(readerEmail, StringComparison.OrdinalIgnoreCase) &&
            (readerRole == "HOD"
                ? m.HODEmail.Equals(readerEmail, StringComparison.OrdinalIgnoreCase)
                : m.CPEmail.Equals(readerEmail,  StringComparison.OrdinalIgnoreCase)));

    public static List<CpEntry> CpList { get; } = new()
    {
        new CpEntry
        {
            Id         = 1,
            FullName   = "Eric Nshimiye",
            Email      = "eric.nshimiye@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/002",
            Department = "Computer Science & IT",
            Year       = 3,
            Programme  = "BIT"
        },
        new CpEntry
        {
            Id         = 2,
            FullName   = "Alice Mutesi",
            Email      = "alice.mutesi@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/010",
            Department = "Computer Science & IT",
            Year       = 2,
            Programme  = "BIT"
        },
        new CpEntry
        {
            Id         = 3,
            FullName   = "James Nkurunziza",
            Email      = "j.nkurunziza@unilak.ac.rw",
            RegNumber  = "UNILAK/2023/045",
            Department = "Computer Science & IT",
            Year       = 4,
            Programme  = "BIT"
        },
        new CpEntry
        {
            Id         = 4,
            FullName   = "Grace Uwimana",
            Email      = "g.uwimana@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/031",
            Department = "Civil Engineering",
            Year       = 2,
            Programme  = "BCE"
        },
        new CpEntry
        {
            Id         = 5,
            FullName   = "Patrick Ngabo",
            Email      = "p.ngabo@unilak.ac.rw",
            RegNumber  = "UNILAK/2023/022",
            Department = "Civil Engineering",
            Year       = 3,
            Programme  = "BCE"
        },
        new CpEntry
        {
            Id         = 6,
            FullName   = "Diane Mukamana",
            Email      = "d.mukamana@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/055",
            Department = "Electrical Engineering",
            Year       = 2,
            Programme  = "BEE"
        },
        new CpEntry
        {
            Id         = 7,
            FullName   = "Samuel Habimana",
            Email      = "s.habimana@unilak.ac.rw",
            RegNumber  = "UNILAK/2023/060",
            Department = "Electrical Engineering",
            Year       = 3,
            Programme  = "BEE"
        },
        new CpEntry
        {
            Id         = 8,
            FullName   = "Claudine Nyiransabimana",
            Email      = "c.nyiransabimana@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/071",
            Department = "Mechanical Engineering",
            Year       = 1,
            Programme  = "BME"
        },
        new CpEntry
        {
            Id         = 9,
            FullName   = "Emmanuel Kayitare",
            Email      = "e.kayitare@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/088",
            Department = "Business Administration",
            Year       = 2,
            Programme  = "BBA"
        },
        new CpEntry
        {
            Id         = 10,
            FullName   = "Sylvie Ingabire",
            Email      = "s.ingabire@unilak.ac.rw",
            RegNumber  = "UNILAK/2023/092",
            Department = "Business Administration",
            Year       = 3,
            Programme  = "BBA"
        },
        new CpEntry
        {
            Id         = 11,
            FullName   = "Thierry Niyomukiza",
            Email      = "t.niyomukiza@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/101",
            Department = "Accounting & Finance",
            Year       = 2,
            Programme  = "BAF"
        },
        new CpEntry
        {
            Id         = 12,
            FullName   = "Vestine Umutoniwase",
            Email      = "v.umutoniwase@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/115",
            Department = "Social Sciences",
            Year       = 1,
            Programme  = "BSS"
        },
        new CpEntry
        {
            Id         = 13,
            FullName   = "Fidele Nzabirinda",
            Email      = "f.nzabirinda@unilak.ac.rw",
            RegNumber  = "UNILAK/2023/119",
            Department = "Social Sciences",
            Year       = 3,
            Programme  = "BSS"
        },
        new CpEntry
        {
            Id         = 14,
            FullName   = "Liliane Rugema",
            Email      = "l.rugema@unilak.ac.rw",
            RegNumber  = "UNILAK/2024/132",
            Department = "Law",
            Year       = 2,
            Programme  = "LLB"
        }
    };
}

public class CpEntry
{
    public int    Id         { get; set; }
    public string FullName   { get; set; } = string.Empty;
    public string Email      { get; set; } = string.Empty;
    public string RegNumber  { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int    Year       { get; set; }
    public string Programme  { get; set; } = string.Empty;
    public string Initials => FullName.Length >= 2
        ? $"{FullName[0]}{FullName.Split(' ').LastOrDefault()?[0]}"
        : FullName.ToUpper();
}
