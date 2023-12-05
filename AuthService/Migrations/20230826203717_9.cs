using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuthService.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RightHoldMappers");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "UserHoldMappers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RightId",
                table: "Holds",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusBossId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusMemberId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RightStatusMappers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RightId = table.Column<int>(type: "integer", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightStatusMappers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RightStatusMappers_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RightStatusMappers_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHoldMappers_StatusId",
                table: "UserHoldMappers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_RightId",
                table: "Holds",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupHoldMappers_StatusBossId",
                table: "GroupHoldMappers",
                column: "StatusBossId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupHoldMappers_StatusMemberId",
                table: "GroupHoldMappers",
                column: "StatusMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RightStatusMappers_RightId",
                table: "RightStatusMappers",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_RightStatusMappers_StatusId",
                table: "RightStatusMappers",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Statuses_StatusBossId",
                table: "GroupHoldMappers",
                column: "StatusBossId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Statuses_StatusMemberId",
                table: "GroupHoldMappers",
                column: "StatusMemberId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_Rights_RightId",
                table: "Holds",
                column: "RightId",
                principalTable: "Rights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Statuses_StatusId",
                table: "UserHoldMappers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Statuses_StatusBossId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Statuses_StatusMemberId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_Rights_RightId",
                table: "Holds");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHoldMappers_Statuses_StatusId",
                table: "UserHoldMappers");

            migrationBuilder.DropTable(
                name: "RightStatusMappers");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_UserHoldMappers_StatusId",
                table: "UserHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_Holds_RightId",
                table: "Holds");

            migrationBuilder.DropIndex(
                name: "IX_GroupHoldMappers_StatusBossId",
                table: "GroupHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_GroupHoldMappers_StatusMemberId",
                table: "GroupHoldMappers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "UserHoldMappers");

            migrationBuilder.DropColumn(
                name: "RightId",
                table: "Holds");

            migrationBuilder.DropColumn(
                name: "StatusBossId",
                table: "GroupHoldMappers");

            migrationBuilder.DropColumn(
                name: "StatusMemberId",
                table: "GroupHoldMappers");

            migrationBuilder.CreateTable(
                name: "RightHoldMappers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoldId = table.Column<int>(type: "integer", nullable: true),
                    RightId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightHoldMappers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RightHoldMappers_Holds_HoldId",
                        column: x => x.HoldId,
                        principalTable: "Holds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RightHoldMappers_Rights_RightId",
                        column: x => x.RightId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RightHoldMappers_HoldId",
                table: "RightHoldMappers",
                column: "HoldId");

            migrationBuilder.CreateIndex(
                name: "IX_RightHoldMappers_RightId",
                table: "RightHoldMappers",
                column: "RightId");
        }
    }
}
