using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class create16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignId",
                table: "Stages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamcenterRef",
                table: "Processes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "TeamcenterRef",
                table: "Processes");
        }
    }
}
