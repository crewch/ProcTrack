using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class create1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RightHoldMappers_HoldId",
                table: "RightHoldMappers",
                column: "HoldId");

            migrationBuilder.CreateIndex(
                name: "IX_RightHoldMappers_RightId",
                table: "RightHoldMappers",
                column: "RightId");

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers",
                column: "RightId",
                principalTable: "Rights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_RightHoldMappers_HoldId",
                table: "RightHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_RightHoldMappers_RightId",
                table: "RightHoldMappers");
        }
    }
}
