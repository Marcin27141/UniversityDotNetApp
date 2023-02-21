using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SetNullInCourseOnProfDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorID",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorID",
                table: "Courses",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "EntityProfessorID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorID",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorID",
                table: "Courses",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "EntityProfessorID");
        }
    }
}
