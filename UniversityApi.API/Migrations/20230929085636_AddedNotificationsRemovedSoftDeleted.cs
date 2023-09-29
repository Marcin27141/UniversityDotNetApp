using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationsRemovedSoftDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("3e64612e-7dfc-4c98-a781-f44bf31c2fa0"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("ba73d4b0-951d-460b-987f-baec826aca31"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("f06d7645-06d0-4fd2-adf9-fd92b1d8154c"));

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "EntityNotification",
                columns: table => new
                {
                    EntityNotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityNotification", x => x.EntityNotificationId);
                    table.ForeignKey(
                        name: "FK_EntityNotification_People_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "People",
                        principalColumn: "EntityPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorId" },
                values: new object[,]
                {
                    { new Guid("66f00aa8-17b4-43ef-894e-333547ec3add"), "C02", 3, true, "Algorithms", null },
                    { new Guid("7f1cb3a4-7170-4547-a36b-71a0ac1f7844"), "C03", 4, true, "Computer science", null },
                    { new Guid("c5e819f9-a029-484e-9542-f2f705e35780"), "C01", 2, false, "Databases", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityNotification_RecipientId",
                table: "EntityNotification",
                column: "RecipientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityNotification");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("66f00aa8-17b4-43ef-894e-333547ec3add"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("7f1cb3a4-7170-4547-a36b-71a0ac1f7844"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("c5e819f9-a029-484e-9542-f2f705e35780"));

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorId", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("3e64612e-7dfc-4c98-a781-f44bf31c2fa0"), "C03", 4, true, "Computer science", null, false },
                    { new Guid("ba73d4b0-951d-460b-987f-baec826aca31"), "C02", 3, true, "Algorithms", null, false },
                    { new Guid("f06d7645-06d0-4fd2-adf9-fd92b1d8154c"), "C01", 2, false, "Databases", null, false }
                });
        }
    }
}
