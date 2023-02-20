using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ChangingentitiesnamestoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Students",
                newName: "EntityStudentID");

            migrationBuilder.RenameColumn(
                name: "ProfessorID",
                table: "Professors",
                newName: "EntityProfessorID");

            migrationBuilder.RenameColumn(
                name: "PersonalDataID",
                table: "PersonalData",
                newName: "EntityPersonalDataID");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Courses",
                newName: "EntityCourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityStudentID",
                table: "Students",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "EntityProfessorID",
                table: "Professors",
                newName: "ProfessorID");

            migrationBuilder.RenameColumn(
                name: "EntityPersonalDataID",
                table: "PersonalData",
                newName: "PersonalDataID");

            migrationBuilder.RenameColumn(
                name: "EntityCourseID",
                table: "Courses",
                newName: "CourseID");
        }
    }
}
