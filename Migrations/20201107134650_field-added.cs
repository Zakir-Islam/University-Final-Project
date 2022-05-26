using Microsoft.EntityFrameworkCore.Migrations;

namespace University_Final_Project.Migrations
{
    public partial class fieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Employee_Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d2536cc-36fb-42cc-be41-275289c8e38a");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Employee_Id", "Father_Name", "First_Name", "Last_Name" },
                values: new object[] { "admin", "Noname", "Super", "User" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentRoll_Number", "TeacherEmployee_Id", "TwoFactorEnabled", "UserName", "admin_Fid", "student_Fid", "teacher_Fid" },
                values: new object[] { "9d2536cc-36fb-42cc-be41-275289c8e38a", 0, "6f1a49f6-735c-4d43-95b2-917d3fb63185", null, false, false, null, null, null, "Admin1", null, false, "31620bb7-c589-4394-81b4-58bf8ef23fcf", null, null, false, "admin", null, "admin", null });
        }
    }
}
