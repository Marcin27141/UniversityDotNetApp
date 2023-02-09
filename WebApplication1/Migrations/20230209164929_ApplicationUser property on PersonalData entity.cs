using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserpropertyonPersonalDataentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PersonalData",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_ApplicationUserId",
                table: "PersonalData",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_AspNetUsers_ApplicationUserId",
                table: "PersonalData",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_AspNetUsers_ApplicationUserId",
                table: "PersonalData");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_ApplicationUserId",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PersonalData");
        }
    }
}
