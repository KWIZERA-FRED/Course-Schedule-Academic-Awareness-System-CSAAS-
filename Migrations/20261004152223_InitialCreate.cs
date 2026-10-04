using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseScheduleSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HODEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CPEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SenderRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentFullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StudentRegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssessmentLabel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LecturerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LecturerResponse = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CorrectedScore = table.Column<double>(type: "float", nullable: true),
                    RaisedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Lecturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ScheduleTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WhatsappGroupUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JoinDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledHours = table.Column<int>(type: "int", nullable: false),
                    DeliveredHours = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escalations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Lecturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeliveryPct = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EscalatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EscalatedTo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EscalatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentRegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentFullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LecturerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssessmentType = table.Column<int>(type: "int", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Block = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BookedSlotsJson = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LecturerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClassRepresentativeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationHours = table.Column<double>(type: "float", nullable: false),
                    TopicCovered = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CPSignedOffOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CPNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HODApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HODNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DeanApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeanNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Programme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttendanceRate = table.Column<double>(type: "float", nullable: false),
                    EnrolledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrolledCourseIdsRaw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code",
                table: "Courses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegistrationNumber",
                table: "Students",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Escalations");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SessionReports");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
