using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingWithEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "EntityPersonID", "Birthday", "FirstName", "LastName", "Motherland", "PESEL", "SoftDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Adamczyk", "Poland", "01111111111", false },
                    { 2, new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bartosz", "Bednarek", "Poland", "0222222222", false },
                    { 3, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celina", "Czarna", "Poland", "03333333333", false },
                    { 4, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Danuta", "Dobrzycka", "Poland", "14444444444", false },
                    { 5, new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eustachy", "Epoletnik", "Poland", "15555555555", false },
                    { 6, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filomena", "Fomicz", "Poland", "16666666666", false }
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "EntityPersonID", "FirstDayAtJob", "IdCode", "Salary", "Subject" },
                values: new object[,]
                {
                    { 4, new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111", 10000, "Programming" },
                    { 5, new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "22222", 9000, "Science" },
                    { 6, new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "33333", 8000, "Philosophy" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "EntityPersonID", "BeginningOfStudying", "Index" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "111111" },
                    { 2, new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222222" },
                    { 3, new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "333333" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { 1, "C01", 2, false, "Databases", 4, false },
                    { 2, "C02", 3, true, "Algorithms", 4, false },
                    { 3, "C03", 4, true, "Computer science", 5, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: 5);
        }
    }
}
