using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.CP
{
    [Authorize(Roles = "ClassRepresentative")]
    public class ChatModel : PageModel
    {
        // ── Data exposed to the view ───────────────────────────

        public User              CurrentUser  { get; private set; } = default!;

        /// <summary>The HOD this CP is currently chatting with</summary>
        public User?             ActiveHOD    { get; private set; }

        /// <summary>All HODs — a CP can receive/send messages to any HOD</summary>
        public List<User>        AllHODs      { get; private set; } = new();

        public List<ChatMessage> Conversation { get; private set; } = new();
        public int               TotalUnread  { get; private set; }

        // ── Which HOD is selected (query string) ───────────────

        [BindProperty(SupportsGet = true)]
        public string? HodEmail { get; set; }

        // ── Feedback ───────────────────────────────────────────

        [TempData] public string? StatusMessage { get; set; }

        // ══════════════════════════════════════════════════════
        //  GET
        // ══════════════════════════════════════════════════════

        public void OnGet()
        {
            LoadUser();

            AllHODs     = UserData.Users.Where(u => u.Role == UserRole.HOD).ToList();
            TotalUnread = ChatData.UnreadCount(CurrentUser.Email, "CP");

            if (!string.IsNullOrWhiteSpace(HodEmail))
            {
                ActiveHOD = AllHODs.FirstOrDefault(h =>
                    h.Email.Equals(HodEmail, StringComparison.OrdinalIgnoreCase));

                if (ActiveHOD != null)
                {
                    Conversation = ChatData.GetConversation(ActiveHOD.Email, CurrentUser.Email);
                    // Mark incoming HOD messages as read
                    ChatData.MarkRead(ActiveHOD.Email, CurrentUser.Email, "CP");
                }
            }
            else if (AllHODs.Count > 0)
            {
                // Auto-select the first HOD if none specified
                ActiveHOD    = AllHODs[0];
                HodEmail     = ActiveHOD.Email;
                Conversation = ChatData.GetConversation(ActiveHOD.Email, CurrentUser.Email);
                ChatData.MarkRead(ActiveHOD.Email, CurrentUser.Email, "CP");
            }
        }

        // ══════════════════════════════════════════════════════
        //  POST — Send a message
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostSend(string hodEmail, string text)
        {
            LoadUser();

            if (string.IsNullOrWhiteSpace(text))
                return RedirectToPage(new { hodEmail });

            var hod = UserData.Users.FirstOrDefault(u =>
                u.Email.Equals(hodEmail, StringComparison.OrdinalIgnoreCase) &&
                u.Role == UserRole.HOD);

            if (hod == null)
            {
                StatusMessage = "HOD not found.";
                return RedirectToPage();
            }

            ChatData.AddMessage(
                hodEmail   : hod.Email,
                cpEmail    : CurrentUser.Email,
                senderEmail: CurrentUser.Email,
                senderName : CurrentUser.FullName,
                senderRole : "CP",
                text       : text);

            return RedirectToPage(new { hodEmail });
        }

        // ═════════════════════════════════════════════════════��
        //  Private helpers
        // ══════════════════════════════════════════════════════

        private void LoadUser()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                        ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "Class Rep", LastName = "" };
        }
    }
}
