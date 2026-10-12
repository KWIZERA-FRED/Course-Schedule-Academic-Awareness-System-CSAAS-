using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseScheduleSystem.Web.Data;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Pages.Lecturer
{
    [Authorize(Roles = "Lecturer")]
    public class AvailabilityModel : PageModel
    {
        public User                      CurrentUser    { get; private set; } = default!;
        public List<TeacherAvailability> MyAvailability { get; private set; } = new();
        public string[]                  AllDays        => AvailabilityData.AllDays;
        public string[]                  AllTimeSlots   => AvailabilityData.AllTimeSlots;

        [TempData] public string? StatusMessage { get; set; }
        [TempData] public string? StatusType    { get; set; }

        public void OnGet() => LoadData();

        public IActionResult OnPostSaveAvailability(string[] availableSlots)
        {
            LoadData();

            // Remove all existing entries for this lecturer
            AvailabilityData.Availabilities.RemoveAll(a =>
                a.LecturerEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase));

            int nextId = AvailabilityData.Availabilities.Count > 0
                ? AvailabilityData.Availabilities.Max(a => a.Id) + 1 : 1;

            // Rebuild from submitted checkboxes
            foreach (var day in AvailabilityData.AllDays)
            {
                foreach (var slot in AvailabilityData.AllTimeSlots)
                {
                    var slotKey   = $"{day[..3]} {slot}";
                    var isChecked = availableSlots != null && availableSlots.Contains(slotKey);
                    AvailabilityData.Availabilities.Add(new TeacherAvailability
                    {
                        Id            = nextId++,
                        LecturerEmail = CurrentUser.Email,
                        LecturerName  = CurrentUser.FullName,
                        Day           = day,
                        TimeSlot      = slot,
                        IsAvailable   = isChecked,
                        UpdatedOn     = DateTime.Now
                    });
                }
            }

            // Update available slots count
            var availCount = availableSlots?.Length ?? 0;
            StatusMessage = $"✓ Availability updated. {availCount} slot{(availCount != 1 ? "s" : "")} marked as available.";
            StatusType    = "success";
            return RedirectToPage();
        }

        private void LoadData()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? string.Empty;
            CurrentUser = UserData.Users.FirstOrDefault(u => u.Email == email)
                ?? new User { FirstName = "Dr.", LastName = "Mugisha", Email = email };

            MyAvailability = AvailabilityData.Availabilities
                .Where(a => a.LecturerEmail.Equals(CurrentUser.Email, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Seed defaults if first time
            if (!MyAvailability.Any())
            {
                int nextId = AvailabilityData.Availabilities.Count > 0
                    ? AvailabilityData.Availabilities.Max(a => a.Id) + 1 : 1;
                foreach (var day in AvailabilityData.AllDays)
                {
                    foreach (var slot in AvailabilityData.AllTimeSlots)
                    {
                        var entry = new TeacherAvailability
                        {
                            Id            = nextId++,
                            LecturerEmail = CurrentUser.Email,
                            LecturerName  = CurrentUser.FullName,
                            Day           = day,
                            TimeSlot      = slot,
                            IsAvailable   = true,
                            UpdatedOn     = DateTime.Now
                        };
                        AvailabilityData.Availabilities.Add(entry);
                        MyAvailability.Add(entry);
                    }
                }
            }
        }
    }
}
