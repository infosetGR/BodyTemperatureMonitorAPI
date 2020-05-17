using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemperatureMonitorAPI.Migrations
{
    public partial class addImageToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "PatientDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "PatientDetails");
        }
    }
}
