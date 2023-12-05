using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class create6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Groups_GroupId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Holds_HoldId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Groups_GroupId",
                table: "GroupHoldMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Holds_HoldId",
                table: "GroupHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers",
                column: "RightId",
                principalTable: "Rights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Groups_GroupId",
                table: "UserGroupMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Users_UserId",
                table: "UserGroupMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Holds_HoldId",
                table: "UserHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Users_UserId",
                table: "UserHoldMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappers_Roles_RoleId",
                table: "UserRoleMappers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMappers_Users_UserId",
                table: "UserRoleMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Groups_GroupId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupHoldMappers_Holds_HoldId",
                table: "GroupHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers");

            migrationBuilder.DropForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Groups_GroupId",
                table: "GroupHoldMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Holds_HoldId",
                table: "GroupHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Holds_HoldId",
                table: "RightHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RightHoldMappers_Rights_RightId",
                table: "RightHoldMappers",
                column: "RightId",
                principalTable: "Rights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Groups_GroupId",
                table: "UserGroupMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMappers_Users_UserId",
                table: "UserGroupMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Holds_HoldId",
                table: "UserHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHoldMappers_Users_UserId",
                table: "UserHoldMappers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
