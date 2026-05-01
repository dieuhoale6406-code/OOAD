using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OOAD.Migrations
{
    /// <inheritdoc />
    public partial class SeedFiveUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "PasswordHash", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "nnguyenlinh33662222@gmail.com", "Nguyễn Đỗ Khánh Linh", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "0900000001" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "ntctuyen06@gmail.com", "Nguyễn Thị Cẩm Tuyền", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "0900000002" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "dieuhoale6406@gmail.com", "Lê Thị Diệu Hòa", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "0900000003" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "vanngocnhuy30032006@gmail.com", "Văn Ngọc Như Ý", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "0900000004" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "nguyenvanan@gmail.com", "Nguyễn Văn An", "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92", "0900000005" }
                });

            migrationBuilder.InsertData(
                table: "Calendars",
                columns: new[] { "CalendarId", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("55555555-5555-5555-5555-555555555555") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calendars",
                keyColumn: "CalendarId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Calendars",
                keyColumn: "CalendarId",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Calendars",
                keyColumn: "CalendarId",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Calendars",
                keyColumn: "CalendarId",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Calendars",
                keyColumn: "CalendarId",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));
        }
    }
}
