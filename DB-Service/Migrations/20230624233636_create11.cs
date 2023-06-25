using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class create11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signed",
                table: "Tasks",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Signed",
                table: "Stages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signed",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "Signed",
                table: "Stages",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
