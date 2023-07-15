using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92346aea-5fcd-4d68-ab3b-a1b21239bf58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8545da7-86d6-4638-8b4c-3689cd854a4f");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("1ee39cc1-83af-433d-91b0-273451b65e0c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("2eaa21b8-3629-4890-91e5-e08bd6f0e466"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("3ff0a2dc-0e6a-4a1e-8404-d495c1193c34"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("23b23c60-6879-462d-a90e-25801c14e0b2"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("5b787535-3514-462b-8f55-3d03af6ff059"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("d5113967-eb87-466b-afea-75bb6268dc08"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("69abb32a-8371-4655-a50c-8bcb2c217c4a"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("9d090a5e-d330-447b-a1bc-6c70ead5d2c8"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("b5d4d0be-f9db-4e5e-9ac0-cd2149ba83ce"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("23b23c60-6879-462d-a90e-25801c14e0b2"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("5b787535-3514-462b-8f55-3d03af6ff059"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("69abb32a-8371-4655-a50c-8bcb2c217c4a"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("9d090a5e-d330-447b-a1bc-6c70ead5d2c8"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("b5d4d0be-f9db-4e5e-9ac0-cd2149ba83ce"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("d5113967-eb87-466b-afea-75bb6268dc08"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "People",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92346aea-5fcd-4d68-ab3b-a1b21239bf58", "84d5146f-85b4-47b3-8b53-010f4933202b", "Administrator", "ADMINISTRATOR" },
                    { "e8545da7-86d6-4638-8b4c-3689cd854a4f", "0d0d4d5e-8801-46e4-94b2-9d2b4caff29c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("1ee39cc1-83af-433d-91b0-273451b65e0c"), "C03", 4, true, "Computer science", null, null, false },
                    { new Guid("2eaa21b8-3629-4890-91e5-e08bd6f0e466"), "C02", 3, true, "Algorithms", null, null, false },
                    { new Guid("3ff0a2dc-0e6a-4a1e-8404-d495c1193c34"), "C01", 2, false, "Databases", null, null, false }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "EntityPersonID", "ApplicationUserId", "Birthday", "FirstName", "LastName", "Motherland", "PESEL", "PersonStatus", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("23b23c60-6879-462d-a90e-25801c14e0b2"), null, new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eustachy", "Epoletnik", "Poland", "15555555555", 0, false },
                    { new Guid("5b787535-3514-462b-8f55-3d03af6ff059"), null, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Danuta", "Dobrzycka", "Poland", "14444444444", 0, false },
                    { new Guid("69abb32a-8371-4655-a50c-8bcb2c217c4a"), null, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celina", "Czarna", "Poland", "03333333333", 0, false },
                    { new Guid("9d090a5e-d330-447b-a1bc-6c70ead5d2c8"), null, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Adamczyk", "Poland", "01111111111", 0, false },
                    { new Guid("b5d4d0be-f9db-4e5e-9ac0-cd2149ba83ce"), null, new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bartosz", "Bednarek", "Poland", "0222222222", 0, false },
                    { new Guid("d5113967-eb87-466b-afea-75bb6268dc08"), null, new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filomena", "Fomicz", "Poland", "16666666666", 0, false }
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "EntityPersonID", "FirstDayAtJob", "IdCode", "Salary", "Subject" },
                values: new object[,]
                {
                    { new Guid("23b23c60-6879-462d-a90e-25801c14e0b2"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "22222", 9000, "Science" },
                    { new Guid("5b787535-3514-462b-8f55-3d03af6ff059"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111", 10000, "Programming" },
                    { new Guid("d5113967-eb87-466b-afea-75bb6268dc08"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "33333", 8000, "Philosophy" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "EntityPersonID", "BeginningOfStudying", "Index" },
                values: new object[,]
                {
                    { new Guid("69abb32a-8371-4655-a50c-8bcb2c217c4a"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "333333" },
                    { new Guid("9d090a5e-d330-447b-a1bc-6c70ead5d2c8"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "111111" },
                    { new Guid("b5d4d0be-f9db-4e5e-9ac0-cd2149ba83ce"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222222" }
                });
        }
    }
}
