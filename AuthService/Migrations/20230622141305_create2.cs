using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappers_RoleId",
                table: "UserRoleMappers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappers_UserId",
                table: "UserRoleMappers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHoldMappers_HoldId",
                table: "UserHoldMappers",
                column: "HoldId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHoldMappers_UserId",
                table: "UserHoldMappers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMappers_GroupId",
                table: "UserGroupMappers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMappers_UserId",
                table: "UserGroupMappers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Groups_GroupId",
                table: "UserGroupMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Users_UserId",
                table: "UserGroupMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Holds_HoldId",
                table: "UserHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Users_UserId",
                table: "UserHoldMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappers_Roles_RoleId",
                table: "UserRoleMappers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappers_Users_UserId",
                table: "UserRoleMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMappers_Groups_GroupId",
                table: "UserGroupMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMappers_Users_UserId",
                table: "UserGroupMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHoldMappers_Holds_HoldId",
                table: "UserHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHoldMappers_Users_UserId",
                table: "UserHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMappers_Roles_RoleId",
                table: "UserRoleMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMappers_Users_UserId",
                table: "UserRoleMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMappers_RoleId",
                table: "UserRoleMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMappers_UserId",
                table: "UserRoleMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserHoldMappers_HoldId",
                table: "UserHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserHoldMappers_UserId",
                table: "UserHoldMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupMappers_GroupId",
                table: "UserGroupMappers");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupMappers_UserId",
                table: "UserGroupMappers");
        }
    }
}
