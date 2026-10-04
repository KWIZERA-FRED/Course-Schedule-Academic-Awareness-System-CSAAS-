using Microsoft.EntityFrameworkCore;
using CourseScheduleSystem.Web.Models;

namespace CourseScheduleSystem.Web.Data;

/// <summary>
/// Entity Framework Core DbContext for the CSAS application.
/// Manages all database tables (DbSets) and their relationships.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── Tables ─────────────────────────────────────────────

    public DbSet<User>           Users          { get; set; }
    public DbSet<Course>         Courses        { get; set; }
    public DbSet<Student>        Students       { get; set; }
    public DbSet<Mark>           Marks          { get; set; }
    public DbSet<Claim>          Claims         { get; set; }
    public DbSet<SessionReport>  SessionReports { get; set; }
    public DbSet<Room>           Rooms          { get; set; }
    public DbSet<ChatMessage>    ChatMessages   { get; set; }
    public DbSet<EscalationRecord> Escalations  { get; set; }

    // ── Model configuration ─────────────────────────────────

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── User ──────────────────────────────────────────
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Identifier).HasMaxLength(100);
            e.Property(u => u.PasswordHash).IsRequired();
            e.Property(u => u.FirstName).HasMaxLength(100);
            e.Property(u => u.LastName).HasMaxLength(100);
            e.Property(u => u.Department).HasMaxLength(200);
        });

        // ── Course ────────────────────────────────────────
        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Code).IsRequired().HasMaxLength(20);
            e.HasIndex(c => c.Code).IsUnique();
            e.Property(c => c.Title).IsRequired().HasMaxLength(200);
            e.Property(c => c.Lecturer).HasMaxLength(200);
            e.Property(c => c.Venue).HasMaxLength(200);
            e.Property(c => c.Department).HasMaxLength(200);
            e.Property(c => c.WhatsappGroupUrl).HasMaxLength(500);
        });

        // ── Student ───────────────────────────────────────
        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.RegistrationNumber).IsRequired().HasMaxLength(50);
            e.HasIndex(s => s.RegistrationNumber).IsUnique();
            e.Property(s => s.Email).HasMaxLength(200);
            e.Property(s => s.FirstName).HasMaxLength(100);
            e.Property(s => s.LastName).HasMaxLength(100);
            e.Property(s => s.Department).HasMaxLength(200);
            e.Property(s => s.Programme).HasMaxLength(100);
            // EnrolledCourseIds stored as comma-separated string
            e.Ignore(s => s.EnrolledCourseIds);
            e.Property<string>("EnrolledCourseIdsRaw").HasMaxLength(500);
        });

        // ── Mark ──────────────────────────────────────────
        modelBuilder.Entity<Mark>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.CourseCode).HasMaxLength(20);
            e.Property(m => m.CourseTitle).HasMaxLength(200);
            e.Property(m => m.StudentRegistrationNumber).HasMaxLength(50);
            e.Property(m => m.StudentFullName).HasMaxLength(200);
            e.Property(m => m.LecturerName).HasMaxLength(200);
            e.Property(m => m.Remarks).HasMaxLength(500);
        });

        // ── Claim ─────────────────────────────────────────
        modelBuilder.Entity<Claim>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.CourseCode).HasMaxLength(20);
            e.Property(c => c.CourseTitle).HasMaxLength(200);
            e.Property(c => c.AssessmentLabel).HasMaxLength(100);
            e.Property(c => c.LecturerName).HasMaxLength(200);
            e.Property(c => c.StudentFullName).HasMaxLength(200);
            e.Property(c => c.StudentRegistrationNumber).HasMaxLength(50);
            e.Property(c => c.Reason).HasMaxLength(1000);
            e.Property(c => c.LecturerResponse).HasMaxLength(1000);
        });

        // ── SessionReport ─────────────────────────────────
        modelBuilder.Entity<SessionReport>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.CourseCode).HasMaxLength(20);
            e.Property(r => r.CourseTitle).HasMaxLength(200);
            e.Property(r => r.LecturerName).HasMaxLength(200);
            e.Property(r => r.ClassRepresentativeName).HasMaxLength(200);
            e.Property(r => r.TopicCovered).HasMaxLength(500);
            e.Property(r => r.Venue).HasMaxLength(200);
            e.Property(r => r.CPNotes).HasMaxLength(500);
            e.Property(r => r.HODNotes).HasMaxLength(500);
            e.Property(r => r.DeanNotes).HasMaxLength(500);
            e.Property(r => r.RejectionReason).HasMaxLength(500);
        });

        // ── Room ──────────────────────────────────────────
        modelBuilder.Entity<Room>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.Block).HasMaxLength(100);
            e.Property(r => r.Number).HasMaxLength(50);
            e.Property(r => r.Notes).HasMaxLength(500);
            // BookedSlots dictionary stored as JSON string
            e.Ignore(r => r.BookedSlots);
            e.Property<string>("BookedSlotsJson").HasMaxLength(2000);
        });

        // ── ChatMessage ───────────────────────────────────
        modelBuilder.Entity<ChatMessage>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.HODEmail).HasMaxLength(200);
            e.Property(m => m.CPEmail).HasMaxLength(200);
            e.Property(m => m.SenderEmail).HasMaxLength(200);
            e.Property(m => m.SenderName).HasMaxLength(200);
            e.Property(m => m.SenderRole).HasMaxLength(50);
            e.Property(m => m.Text).IsRequired().HasMaxLength(2000);
        });

        // ── EscalationRecord ──────────────────────────────
        modelBuilder.Entity<EscalationRecord>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.CourseCode).HasMaxLength(20);
            e.Property(r => r.CourseTitle).HasMaxLength(200);
            e.Property(r => r.Department).HasMaxLength(200);
            e.Property(r => r.Lecturer).HasMaxLength(200);
            e.Property(r => r.Notes).HasMaxLength(1000);
            e.Property(r => r.EscalatedBy).HasMaxLength(200);
            e.Property(r => r.EscalatedTo).HasMaxLength(200);
        });
    }
}
