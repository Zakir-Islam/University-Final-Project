using Microsoft.EntityFrameworkCore.Migrations;

namespace University_Final_Project.Migrations
{
    public partial class admintableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "admin_Fid",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Employee_Id = table.Column<string>(nullable: false),
                    First_Name = table.Column<string>(nullable: false),
                    Last_Name = table.Column<string>(nullable: false),
                    Father_Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Employee_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropColumn(
                name: "admin_Fid",
                table: "AspNetUsers");
        }
    }
}
