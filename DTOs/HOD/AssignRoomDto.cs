using System.ComponentModel.DataAnnotations;

namespace CourseScheduleSystem.Web.DTOs.HOD;

/// <summary>
/// Carries the Assign Room action parameters —
/// matches OnPostAssignRoom parameters on HOD/Dashboard.cshtml.cs.
/// </summary>
public class AssignRoomDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid course ID.")]
    public int CourseId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid room ID.")]
    public int RoomId { get; set; }
}
