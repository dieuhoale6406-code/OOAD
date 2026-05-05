using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOAD.Migrations
{
    /// <inheritdoc />
    public partial class updateReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupMeetingId",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId_GroupMeetingId",
                table: "Reminders",
                columns: new[] { "UserId", "GroupMeetingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_UserGroupMeetings_UserId_GroupMeetingId",
                table: "Reminders",
                columns: new[] { "UserId", "GroupMeetingId" },
                principalTable: "UserGroupMeetings",
                principalColumns: new[] { "UserId", "GroupMeetingId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_UserGroupMeetings_UserId_GroupMeetingId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserId_GroupMeetingId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "GroupMeetingId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reminders");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointmentId",
                table: "Reminders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
