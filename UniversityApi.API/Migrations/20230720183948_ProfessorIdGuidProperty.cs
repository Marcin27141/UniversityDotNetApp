using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class ProfessorIdGuidProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    EntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motherland = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonStatus = table.Column<int>(type: "int", nullable: false),
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
                    EntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    EntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    EntityCourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ECTS = table.Column<int>(type: "int", nullable: false),
                    IsFinishedWithExam = table.Column<bool>(type: "bit", nullable: false),
                    ProfessorEntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.EntityCourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Professors_ProfessorEntityPersonID",
                        column: x => x.ProfessorEntityPersonID,
                        principalTable: "Professors",
                        principalColumn: "EntityPersonID");
                });

            migrationBuilder.CreateTable(
                name: "EntityCourseEntityStudent",
                columns: table => new
                {
                    CoursesEntityCourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsEntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("1b9be93a-e979-49a8-b360-63480bceef60"), "C01", 2, false, "Databases", null, false },
                    { new Guid("1ef5b7de-62fb-4b77-8b65-cf6126265d51"), "C03", 4, true, "Computer science", null, false },
                    { new Guid("854d8120-023f-434b-8c07-7fce68d52ca0"), "C02", 3, true, "Algorithms", null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorEntityPersonID",
                table: "Courses",
                column: "ProfessorEntityPersonID");

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
