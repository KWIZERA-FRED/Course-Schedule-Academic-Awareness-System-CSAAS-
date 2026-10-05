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

        public User              CurrentUser  { get; private set; } = default!;
        public User?             ActiveHOD    { get; private set; }
        public List<User>        AllHODs      { get; private set; } = new();

        public List<ChatMessage> Conversation { get; private set; } = new();
        public int               TotalUnread  { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string? HodEmail { get; set; }

        [TempData] public string? StatusMessage { get; set; }

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
                    ChatData.MarkRead(ActiveHOD.Email, CurrentUser.Email, "CP");
                }
            }
            else if (AllHODs.Count > 0)
            {
                ActiveHOD    = AllHODs[0];
                HodEmail     = ActiveHOD.Email;
                Conversation = ChatData.GetConversation(ActiveHOD.Email, CurrentUser.Email);
                ChatData.MarkRead(ActiveHOD.Email, CurrentUser.Email, "CP");
            }
        }

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

        private void LoadUser()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
                        ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                          ?? new User { FirstName = "Class Rep", LastName = "" };
        }
    }
}
