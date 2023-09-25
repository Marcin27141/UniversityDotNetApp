using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSoftDeletedFromPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("1b9be93a-e979-49a8-b360-63480bceef60"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("1ef5b7de-62fb-4b77-8b65-cf6126265d51"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("854d8120-023f-434b-8c07-7fce68d52ca0"));

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "People");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("19398c2f-0a10-4659-af2b-7a4f9d562742"), "C02", 3, true, "Algorithms", null, false },
                    { new Guid("42cc1330-1a0f-4ef1-bcd4-a28114c132e4"), "C01", 2, false, "Databases", null, false },
                    { new Guid("86882315-711c-4d86-8393-c80342f48cc7"), "C03", 4, true, "Computer science", null, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("19398c2f-0a10-4659-af2b-7a4f9d562742"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("42cc1330-1a0f-4ef1-bcd4-a28114c132e4"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("86882315-711c-4d86-8393-c80342f48cc7"));

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("1b9be93a-e979-49a8-b360-63480bceef60"), "C01", 2, false, "Databases", null, false },
                    { new Guid("1ef5b7de-62fb-4b77-8b65-cf6126265d51"), "C03", 4, true, "Computer science", null, false },
                    { new Guid("854d8120-023f-434b-8c07-7fce68d52ca0"), "C02", 3, true, "Algorithms", null, false }
                });
        }
    }
}
