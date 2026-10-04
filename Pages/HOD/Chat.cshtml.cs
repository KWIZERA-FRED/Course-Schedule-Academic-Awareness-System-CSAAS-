using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.HOD
{
    [Authorize(Roles = "HOD")]
    public class ChatModel : PageModel
    {
        // ── Data exposed to the view ───────────────────────────

        public User              CurrentUser    { get; private set; } = default!;
        public List<CpEntry>     AllCPs         { get; private set; } = new();
        public CpEntry?          ActiveCP       { get; private set; }
        public List<ChatMessage> Conversation   { get; private set; } = new();
        public int               TotalUnread    { get; private set; }

        // ── Which CP is currently selected (from query string) ─

        [BindProperty(SupportsGet = true)]
        public string? CpEmail { get; set; }

        // ── Feedback ───────────────────────────────────────────

        [TempData] public string? StatusMessage { get; set; }

        // ══════════════════════════════════════════════════════
        //  GET
        // ══════════════════════════════════════════════════════

        public void OnGet()
        {
            LoadUser();

            // All CPs across UNILAK — HOD can message any of them
            AllCPs = ChatData.CpList.ToList();

            TotalUnread = ChatData.UnreadCount(CurrentUser.Email, "HOD");

            if (!string.IsNullOrWhiteSpace(CpEmail))
            {
                ActiveCP = AllCPs.FirstOrDefault(cp =>
                    cp.Email.Equals(CpEmail, StringComparison.OrdinalIgnoreCase));

                if (ActiveCP != null)
                {
                    Conversation = ChatData.GetConversation(CurrentUser.Email, ActiveCP.Email);
                    // Mark incoming CP messages as read
                    ChatData.MarkRead(CurrentUser.Email, ActiveCP.Email, "HOD");
                }
            }
        }

        // ══════════════════════════════════════════════════════
        //  POST — Send a message
        // ══════════════════════════════════════════════════════

        public IActionResult OnPostSend(string cpEmail, string text)
        {
            LoadUser();

            if (string.IsNullOrWhiteSpace(text))
                return RedirectToPage(new { cpEmail });

            var cp = ChatData.CpList.FirstOrDefault(c =>
                c.Email.Equals(cpEmail, StringComparison.OrdinalIgnoreCase));

            if (cp == null)
            {
                StatusMessage = "CP not found.";
                return RedirectToPage();
            }

            ChatData.AddMessage(
                hodEmail   : CurrentUser.Email,
                cpEmail    : cp.Email,
                senderEmail: CurrentUser.Email,
                senderName : CurrentUser.FullName,
                senderRole : "HOD",
                text       : text);

            return RedirectToPage(new { cpEmail });
        }

        // ══════════════════════════════════════════════════════
        //  Private helpers
        // ══════════════════════════════════════════════════════

        private void LoadUser()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                        ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "HOD", LastName = "" };
        }
    }
}
