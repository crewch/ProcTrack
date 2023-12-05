using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DB_Service.Migrations
{
    public partial class _38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Statuses");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Processes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ProgramId",
                table: "Processes",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Programs_ProgramId",
                table: "Processes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Programs_ProgramId",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ProgramId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Statuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
