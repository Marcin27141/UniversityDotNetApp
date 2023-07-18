using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class SoftRemovableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4467d9c1-61a5-4d5a-b9ef-ccf9dbbb1357");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb032d0b-cca2-4a20-8c17-b4c599345e03");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("6ec85534-e05d-4b89-a9d9-2f2990d72260"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("8f269107-9346-4739-b0e8-1622cade77bb"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("ac462fde-a21c-4dc9-b294-22b221034e3d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d3c829b-d5e6-4a56-a08d-5cde921e3917", "502d34b9-9231-465a-854a-dbfa0ddffb4c", "User", "USER" },
                    { "9feb705b-496d-47f1-ba62-79e5a47c9c63", "26a363bd-ae00-44dd-87a2-a5e03faa9a0f", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("0567384f-5fc3-4f9b-a770-f65be8188f10"), "C01", 2, false, "Databases", null, null, false },
                    { new Guid("749f7297-6db8-4cd1-9ce4-e1a0b598de49"), "C02", 3, true, "Algorithms", null, null, false },
                    { new Guid("b7b95630-5874-4cb9-988b-7b6e6cff2f42"), "C03", 4, true, "Computer science", null, null, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d3c829b-d5e6-4a56-a08d-5cde921e3917");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9feb705b-496d-47f1-ba62-79e5a47c9c63");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("0567384f-5fc3-4f9b-a770-f65be8188f10"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("749f7297-6db8-4cd1-9ce4-e1a0b598de49"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("b7b95630-5874-4cb9-988b-7b6e6cff2f42"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4467d9c1-61a5-4d5a-b9ef-ccf9dbbb1357", "f0e2337a-9725-4de9-a6c0-a7a7e59f085e", "User", "USER" },
                    { "cb032d0b-cca2-4a20-8c17-b4c599345e03", "85780104-cae0-4dfc-9bdd-baf55c000c96", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("6ec85534-e05d-4b89-a9d9-2f2990d72260"), "C02", 3, true, "Algorithms", null, null, false },
                    { new Guid("8f269107-9346-4739-b0e8-1622cade77bb"), "C03", 4, true, "Computer science", null, null, false },
                    { new Guid("ac462fde-a21c-4dc9-b294-22b221034e3d"), "C01", 2, false, "Databases", null, null, false }
                });
        }
    }
}
