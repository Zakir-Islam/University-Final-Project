using Microsoft.EntityFrameworkCore.Migrations;

namespace University_Final_Project.Migrations
{
    public partial class admintableadded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f4e65c50-97e8-45cc-8f9c-c2576746efd3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentRoll_Number", "TeacherEmployee_Id", "TwoFactorEnabled", "UserName", "admin_Fid", "student_Fid", "teacher_Fid" },
                values: new object[] { "9d2536cc-36fb-42cc-be41-275289c8e38a", 0, "6f1a49f6-735c-4d43-95b2-917d3fb63185", null, false, false, null, null, null, "Admin1", null, false, "31620bb7-c589-4394-81b4-58bf8ef23fcf", null, null, false, "admin", null, "admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d2536cc-36fb-42cc-be41-275289c8e38a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentRoll_Number", "TeacherEmployee_Id", "TwoFactorEnabled", "UserName", "admin_Fid", "student_Fid", "teacher_Fid" },
                values: new object[] { "f4e65c50-97e8-45cc-8f9c-c2576746efd3", 0, "3669b926-3030-4fe7-8d13-ba8e366cc06e", null, false, false, null, null, null, "Admin1", null, false, "bee663c2-2b4e-4368-9cf2-a866b9dc77b7", null, null, false, "admin", null, "admin", null });
        }
    }
}
