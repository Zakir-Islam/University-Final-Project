using Microsoft.EntityFrameworkCore.Migrations;

namespace University_Final_Project.Migrations
{
    public partial class departmentadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DegreeFid",
                table: "Students",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DegreeId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    departmentId = table.Column<string>(nullable: false),
                    departmentName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.departmentId);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    DegreeId = table.Column<string>(nullable: false),
                    departmentFid = table.Column<string>(nullable: false),
                    DegreeName = table.Column<string>(nullable: false),
                    departmentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.DegreeId);
                    table.ForeignKey(
                        name: "FK_Degrees_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "departmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DegreeId",
                table: "Students",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_departmentId",
                table: "Degrees",
                column: "departmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "DegreeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Degrees_DegreeId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Students_DegreeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DegreeFid",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Students");
        }
    }
}
