using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityApi.API.Migrations
{
    /// <inheritdoc />
    public partial class PersonStatusProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c30542b-d69e-47e2-84cd-8ee162fe4901");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba022248-dd76-4dc7-a7a6-15df17c7bd55");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("78c52e9b-802f-47d9-9ff5-2f33eaeb034d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("bedbac50-c312-4c09-98ee-f18f5372b646"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("e013432a-33de-4735-8d9e-70992ba3acb7"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("3498a1e1-489b-4296-ad10-7c41ba1f1f9f"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("add84880-1570-4fe8-b305-c1e51d9e40a2"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("f0589de7-392f-4a9e-982c-c59f665ff419"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("a71babb0-4fbd-467a-a18e-1aa9fe91971e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bf84744e-4767-4bf3-a5d4-1403fc0f06fa"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("fb86a22d-d501-4c01-8469-d9e165988e00"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("3498a1e1-489b-4296-ad10-7c41ba1f1f9f"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("a71babb0-4fbd-467a-a18e-1aa9fe91971e"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("add84880-1570-4fe8-b305-c1e51d9e40a2"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bf84744e-4767-4bf3-a5d4-1403fc0f06fa"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("f0589de7-392f-4a9e-982c-c59f665ff419"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("fb86a22d-d501-4c01-8469-d9e165988e00"));

            migrationBuilder.AddColumn<int>(
                name: "PersonStatus",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b4141da-1da8-46b1-b086-c39b3e46e9f2", "38ef3d2d-282a-403f-822d-10941f9a647c", "User", "USER" },
                    { "b47b6078-2ca1-4640-a622-67ef069b9a35", "05e1f2e4-c4de-4b69-8f71-62484c8b4e27", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "EntityCourseID", "CourseCode", "ECTS", "IsFinishedWithExam", "Name", "ProfessorEntityPersonID", "ProfessorID", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("a4b03c7d-fd8d-45d5-b423-8f297317d664"), "C03", 4, true, "Computer science", null, 5, false },
                    { new Guid("bc711d17-f5d7-4184-b450-df1fd58cd050"), "C02", 3, true, "Algorithms", null, 4, false },
                    { new Guid("d6a1da7d-6e31-46ab-af42-0be53a2fd1d4"), "C01", 2, false, "Databases", null, 4, false }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "EntityPersonID", "Birthday", "FirstName", "LastName", "Motherland", "PESEL", "PersonStatus", "SoftDeleted" },
                values: new object[,]
                {
                    { new Guid("043a6545-2ea8-494d-85af-32ffc2db6b31"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Danuta", "Dobrzycka", "Poland", "14444444444", 0, false },
                    { new Guid("29a6ab07-f28a-44ba-ab07-1517aefccbac"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bartosz", "Bednarek", "Poland", "0222222222", 0, false },
                    { new Guid("478f6dd6-7d98-4923-bbe2-d47c55dffa30"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Adamczyk", "Poland", "01111111111", 0, false },
                    { new Guid("7586dc7f-1539-428b-99f3-d3fa5729c8ce"), new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Filomena", "Fomicz", "Poland", "16666666666", 0, false },
                    { new Guid("bc61fc4c-d2db-4a31-90f3-63e91dbe3c70"), new DateTime(1998, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celina", "Czarna", "Poland", "03333333333", 0, false },
                    { new Guid("bf9320e4-8403-4bae-b647-a7e290aa050a"), new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eustachy", "Epoletnik", "Poland", "15555555555", 0, false }
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "EntityPersonID", "FirstDayAtJob", "IdCode", "Salary", "Subject" },
                values: new object[,]
                {
                    { new Guid("043a6545-2ea8-494d-85af-32ffc2db6b31"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111", 10000, "Programming" },
                    { new Guid("7586dc7f-1539-428b-99f3-d3fa5729c8ce"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "33333", 8000, "Philosophy" },
                    { new Guid("bf9320e4-8403-4bae-b647-a7e290aa050a"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "22222", 9000, "Science" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "EntityPersonID", "BeginningOfStudying", "Index" },
                values: new object[,]
                {
                    { new Guid("29a6ab07-f28a-44ba-ab07-1517aefccbac"), new DateTime(2018, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222222" },
                    { new Guid("478f6dd6-7d98-4923-bbe2-d47c55dffa30"), new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "111111" },
                    { new Guid("bc61fc4c-d2db-4a31-90f3-63e91dbe3c70"), new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "333333" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b4141da-1da8-46b1-b086-c39b3e46e9f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b47b6078-2ca1-4640-a622-67ef069b9a35");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("a4b03c7d-fd8d-45d5-b423-8f297317d664"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("bc711d17-f5d7-4184-b450-df1fd58cd050"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "EntityCourseID",
                keyValue: new Guid("d6a1da7d-6e31-46ab-af42-0be53a2fd1d4"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("043a6545-2ea8-494d-85af-32ffc2db6b31"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("7586dc7f-1539-428b-99f3-d3fa5729c8ce"));

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bf9320e4-8403-4bae-b647-a7e290aa050a"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("29a6ab07-f28a-44ba-ab07-1517aefccbac"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("478f6dd6-7d98-4923-bbe2-d47c55dffa30"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bc61fc4c-d2db-4a31-90f3-63e91dbe3c70"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("043a6545-2ea8-494d-85af-32ffc2db6b31"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("29a6ab07-f28a-44ba-ab07-1517aefccbac"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("478f6dd6-7d98-4923-bbe2-d47c55dffa30"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("7586dc7f-1539-428b-99f3-d3fa5729c8ce"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bc61fc4c-d2db-4a31-90f3-63e91dbe3c70"));

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "EntityPersonID",
                keyValue: new Guid("bf9320e4-8403-4bae-b647-a7e290aa050a"));

            migrationBuilder.DropColumn(
                name: "PersonStatus",
                table: "People");

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
        }
    }
}
