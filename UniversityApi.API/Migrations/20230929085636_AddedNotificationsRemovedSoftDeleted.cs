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
               table: "People",
               columns: new[] { "EntityPersonID", "ApplicationUserId", "Birthday", "FirstName", "LastName", "Motherland", "PESEL", "PersonStatus"},
               values: new object[,]
               {
                    { new Guid("18b517f2-b789-4230-e63c-08dbbf4015ce"), new Guid("1a4b454b-4911-40fd-8c60-576f4398dc0f"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eustachy", "Epoletnik", "Poland", "15555555555", 2 },
                    { new Guid("28b517f2-b789-4230-e63c-08dbbf4015ce"), new Guid("9ac971e2-4529-4aa6-bdd1-08c53e6e69cc"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Danuta", "Dobrzycka", "Poland", "14444444444", 2 },
                    { new Guid("5f20e76f-8f68-422d-e63d-08dbbf4015ce"), new Guid("1b4fe74c-b5a3-42dc-9a42-50b904a47c62"), new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celina", "Czarna", "Poland", "03333333333", 1 },
                    { new Guid("1f20e76f-8f68-422d-e63d-08dbbf4015ce"), new Guid("2e16b8b3-2dd6-4462-a10c-3d4fbac4098c"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Adamczyk", "Poland", "01111111111", 1 },
                    { new Guid("2f20e76f-8f68-422d-e63d-08dbbf4015ce"), new Guid("45d44738-8a71-4120-add0-e2882c2b44d8"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bartosz", "Bednarek", "Poland", "0222222222", 1 },
               });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "EntityPersonID", "FirstDayAtJob", "IdCode", "Salary", "Subject" },
                values: new object[,]
                {
                    { new Guid("18b517f2-b789-4230-e63c-08dbbf4015ce"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "22222", 9000, "Science" },
                    { new Guid("28b517f2-b789-4230-e63c-08dbbf4015ce"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111", 10000, "Programming" },
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "EntityPersonID", "BeginningOfStudying", "Index" },
                values: new object[,]
                {
                    { new Guid("5f20e76f-8f68-422d-e63d-08dbbf4015ce"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "333333" },
                    { new Guid("1f20e76f-8f68-422d-e63d-08dbbf4015ce"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "111111" },
                    { new Guid("2f20e76f-8f68-422d-e63d-08dbbf4015ce"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222222" }
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
