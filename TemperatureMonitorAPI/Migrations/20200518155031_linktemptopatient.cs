using Microsoft.EntityFrameworkCore.Migrations;

namespace TemperatureMonitorAPI.Migrations
{
    public partial class linktemptopatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureLogEntries_Users_UserId",
                table: "TemperatureLogEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureLogEntries_PatientDetails_UserId",
                table: "TemperatureLogEntries",
                column: "UserId",
                principalTable: "PatientDetails",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureLogEntries_PatientDetails_UserId",
                table: "TemperatureLogEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureLogEntries_Users_UserId",
                table: "TemperatureLogEntries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
