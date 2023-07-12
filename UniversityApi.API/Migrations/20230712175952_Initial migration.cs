using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    EntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProfessorID = table.Column<int>(type: "int", nullable: true),
                    ProfessorEntityPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c30542b-d69e-47e2-84cd-8ee162fe4901", "81b20b20-8eb3-4e0f-9ec9-653dfbeb4543", "Administrator", "ADMINISTRATOR" },
                    { "ba022248-dd76-4dc7-a7a6-15df17c7bd55", "f6fa6dc1-fd55-4be9-80c0-37d73904bc37", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("78c52e9b-802f-47d9-9ff5-2f33eaeb034d"), "C01", 2, false, "Databases", null, 4, false },
                    { new Guid("bedbac50-c312-4c09-98ee-f18f5372b646"), "C03", 4, true, "Computer science", null, 5, false },
                    { new Guid("e013432a-33de-4735-8d9e-70992ba3acb7"), "C02", 3, true, "Algorithms", null, 4, false }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "EntityPersonID", "Birthday", "FirstName", "LastName", "Motherland", "PESEL", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("3498a1e1-489b-4296-ad10-7c41ba1f1f9f"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Danuta", "Dobrzycka", "Poland", "14444444444", false },
                    { new Guid("a71babb0-4fbd-467a-a18e-1aa9fe91971e"), new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celina", "Czarna", "Poland", "03333333333", false },
                    { new Guid("add84880-1570-4fe8-b305-c1e51d9e40a2"), new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filomena", "Fomicz", "Poland", "16666666666", false },
                    { new Guid("bf84744e-4767-4bf3-a5d4-1403fc0f06fa"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bartosz", "Bednarek", "Poland", "0222222222", false },
                    { new Guid("f0589de7-392f-4a9e-982c-c59f665ff419"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eustachy", "Epoletnik", "Poland", "15555555555", false },
                    { new Guid("fb86a22d-d501-4c01-8469-d9e165988e00"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Adamczyk", "Poland", "01111111111", false }
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "EntityPersonID", "FirstDayAtJob", "IdCode", "Salary", "Subject" },
                values: new object[,]
                {
                    { new Guid("3498a1e1-489b-4296-ad10-7c41ba1f1f9f"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111", 10000, "Programming" },
                    { new Guid("add84880-1570-4fe8-b305-c1e51d9e40a2"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "33333", 8000, "Philosophy" },
                    { new Guid("f0589de7-392f-4a9e-982c-c59f665ff419"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "22222", 9000, "Science" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "EntityPersonID", "BeginningOfStudying", "Index" },
                values: new object[,]
                {
                    { new Guid("a71babb0-4fbd-467a-a18e-1aa9fe91971e"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "333333" },
                    { new Guid("bf84744e-4767-4bf3-a5d4-1403fc0f06fa"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222222" },
                    { new Guid("fb86a22d-d501-4c01-8469-d9e165988e00"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "111111" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EntityCourseEntityStudent");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
