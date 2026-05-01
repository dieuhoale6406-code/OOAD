using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOAD.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelAfterInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Calendars_CalendarId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Calendars_CalendarId",
                table: "Appointments",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "CalendarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Calendars_CalendarId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Calendars_CalendarId",
                table: "Appointments",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "CalendarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
