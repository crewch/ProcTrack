using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthService.Migrations
{
    public partial class create5 : Migration
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
                name: "FK_Holds_Types_TypeId",
                table: "Holds");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "UserHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGroupMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBoss",
                table: "UserGroupMappers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "UserGroupMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RightId",
                table: "RightHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "RightHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Holds",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                name: "FK_Holds_Types_TypeId",
                table: "Holds",
                column: "TypeId",
                principalTable: "Types",
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
                name: "FK_Holds_Types_TypeId",
                table: "Holds");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "UserHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserGroupMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsBoss",
                table: "UserGroupMappers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "UserGroupMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RightId",
                table: "RightHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "RightHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Holds",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoldId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupHoldMappers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Groups_GroupId",
                table: "GroupHoldMappers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupHoldMappers_Holds_HoldId",
                table: "GroupHoldMappers",
                column: "HoldId",
                principalTable: "Holds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_Types_TypeId",
                table: "Holds",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }
    }
}
