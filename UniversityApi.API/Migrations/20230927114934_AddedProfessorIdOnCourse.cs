using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfessorIdOnCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorEntityPersonID",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("2101c6a2-639e-4812-90d9-e11cb845762a"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("355b226b-5b9e-4d2b-bb1e-2cb71574f6cd"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("55f998ed-b6e0-4b6d-ba39-b2d91c701dde"));

            migrationBuilder.RenameColumn(
                name: "ProfessorEntityPersonID",
                table: "Courses",
                newName: "ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ProfessorEntityPersonID",
                table: "Courses",
                newName: "IX_Courses_ProfessorId");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorId", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("3e64612e-7dfc-4c98-a781-f44bf31c2fa0"), "C03", 4, true, "Computer science", null, false },
                    { new Guid("ba73d4b0-951d-460b-987f-baec826aca31"), "C02", 3, true, "Algorithms", null, false },
                    { new Guid("f06d7645-06d0-4fd2-adf9-fd92b1d8154c"), "C01", 2, false, "Databases", null, false }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "EntityPersonID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses");

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

            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "Courses",
                newName: "ProfessorEntityPersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                newName: "IX_Courses_ProfessorEntityPersonID");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("2101c6a2-639e-4812-90d9-e11cb845762a"), "C03", 4, true, "Computer science", null, false },
                    { new Guid("355b226b-5b9e-4d2b-bb1e-2cb71574f6cd"), "C01", 2, false, "Databases", null, false },
                    { new Guid("55f998ed-b6e0-4b6d-ba39-b2d91c701dde"), "C02", 3, true, "Algorithms", null, false }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorEntityPersonID",
                table: "Courses",
                column: "ProfessorEntityPersonID",
                principalTable: "Professors",
                principalColumn: "EntityPersonID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
