using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    EntityPersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motherland = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.EntityPersonID);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    EntityPersonID = table.Column<int>(type: "int", nullable: false),
                    IdCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDayAtJob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.EntityPersonID);
                    table.ForeignKey(
                        name: "FK_Professors_People_EntityPersonID",
                        column: x => x.EntityPersonID,
                        principalTable: "People",
                        principalColumn: "EntityPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    EntityPersonID = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginningOfStudying = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.EntityPersonID);
                    table.ForeignKey(
                        name: "FK_Students_People_EntityPersonID",
                        column: x => x.EntityPersonID,
                        principalTable: "People",
                        principalColumn: "EntityPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    EntityCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ECTS = table.Column<int>(type: "int", nullable: false),
                    IsFinishedWithExam = table.Column<bool>(type: "bit", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProfessorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.EntityCourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "EntityPersonID");
                });

            migrationBuilder.CreateTable(
                name: "EntityCourseEntityStudent",
                columns: table => new
                {
                    CoursesEntityCourseID = table.Column<int>(type: "int", nullable: false),
                    StudentsEntityPersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCourseEntityStudent", x => new { x.CoursesEntityCourseID, x.StudentsEntityPersonID });
                    table.ForeignKey(
                        name: "FK_EntityCourseEntityStudent_Courses_CoursesEntityCourseID",
                        column: x => x.CoursesEntityCourseID,
                        principalTable: "Courses",
                        principalColumn: "EntityCourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityCourseEntityStudent_Students_StudentsEntityPersonID",
                        column: x => x.StudentsEntityPersonID,
                        principalTable: "Students",
                        principalColumn: "EntityPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorID",
                table: "Courses",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCourseEntityStudent_StudentsEntityPersonID",
                table: "EntityCourseEntityStudent",
                column: "StudentsEntityPersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityCourseEntityStudent");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
