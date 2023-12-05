using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class create24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessType_TypeId",
                table: "Processes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessType",
                table: "ProcessType");

            migrationBuilder.RenameTable(
                name: "ProcessType",
                newName: "Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Types_TypeId",
                table: "Processes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Types_TypeId",
                table: "Processes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "ProcessType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessType",
                table: "ProcessType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessType_TypeId",
                table: "Processes",
                column: "TypeId",
                principalTable: "ProcessType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
