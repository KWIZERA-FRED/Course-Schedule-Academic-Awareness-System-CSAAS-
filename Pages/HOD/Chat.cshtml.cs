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

        public User              CurrentUser    { get; private set; } = default!;
        public List<CpEntry>     AllCPs         { get; private set; } = new();
        public CpEntry?          ActiveCP       { get; private set; }
        public List<ChatMessage> Conversation   { get; private set; } = new();
        public int               TotalUnread    { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string? CpEmail { get; set; }

        [TempData] public string? StatusMessage { get; set; }

        public void OnGet()
        {
            LoadUser();
            AllCPs = ChatData.CpList.ToList();

            TotalUnread = ChatData.UnreadCount(CurrentUser.Email, "HOD");

            if (!string.IsNullOrWhiteSpace(CpEmail))
            {
                ActiveCP = AllCPs.FirstOrDefault(cp =>
                    cp.Email.Equals(CpEmail, StringComparison.OrdinalIgnoreCase));

                if (ActiveCP != null)
                {
                    Conversation = ChatData.GetConversation(CurrentUser.Email, ActiveCP.Email);
                    ChatData.MarkRead(CurrentUser.Email, ActiveCP.Email, "HOD");
                }
            }
        }

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

        private void LoadUser()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                        ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "HOD", LastName = "" };
        }
    }
}
