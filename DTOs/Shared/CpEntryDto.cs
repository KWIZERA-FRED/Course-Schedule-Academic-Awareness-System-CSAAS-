namespace CourseScheduleSystem.Web.DTOs.Shared;

/// <summary>
/// Proper namespaced replacement for the <c>CpEntry</c> class defined at global
/// scope inside Data/ChatData.cs. Represents a Class Representative entry used
/// in the HOD/CP chat partner list.
/// </summary>
public class CpEntryDto
{
    public int    Id         { get; set; }
    public string FullName   { get; set; } = string.Empty;
    public string Email      { get; set; } = string.Empty;
    public string RegNumber  { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int    Year       { get; set; }
    public string Programme  { get; set; } = string.Empty;

    /// <summary>
    /// Two-letter initials derived from FullName — used for avatar circles.
    /// </summary>
    public string Initials => FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries) is { Length: >= 2 } parts
        ? $"{parts[0][0]}{parts[^1][0]}"
        : FullName.Length >= 2 ? FullName[..2].ToUpper() : FullName.ToUpper();
}
