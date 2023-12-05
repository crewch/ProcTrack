using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "CanCreate",
                table: "Stages",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Mark",
                table: "Stages",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Pass",
                table: "Stages",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanCreate",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "Pass",
                table: "Stages");
        }
    }
}
