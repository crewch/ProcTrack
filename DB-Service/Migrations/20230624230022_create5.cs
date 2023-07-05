using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class create5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Process_ProcessId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_Priorities_PriorityId",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_Stages_Head",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_Stages_Tail",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Process_Types_TypeId",
                table: "Process");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Process_ProcessId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Status_StatusId",
                table: "Stages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Process",
                table: "Process");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Process",
                newName: "Processes");

            migrationBuilder.RenameColumn(
                name: "IdTemplate",
                table: "Processes",
                newName: "IsTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_Process_TypeId",
                table: "Processes",
                newName: "IX_Processes_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Process_Tail",
                table: "Processes",
                newName: "IX_Processes_Tail");

            migrationBuilder.RenameIndex(
                name: "IX_Process_PriorityId",
                table: "Processes",
                newName: "IX_Processes_PriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_Process_Head",
                table: "Processes",
                newName: "IX_Processes_Head");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndVerificationDate",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedAt",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignedAt",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "CustomField",
                table: "Stages",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcessId",
                table: "Passports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Processes_ProcessId",
                table: "Passports",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Priorities_PriorityId",
                table: "Processes",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Stages_Head",
                table: "Processes",
                column: "Head",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Stages_Tail",
                table: "Processes",
                column: "Tail",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Types_TypeId",
                table: "Processes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Processes_ProcessId",
                table: "Stages",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Statuses_StatusId",
                table: "Stages",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Processes_ProcessId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Priorities_PriorityId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Stages_Head",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Stages_Tail",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Types_TypeId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Processes_ProcessId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Statuses_StatusId",
                table: "Stages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CustomField",
                table: "Stages");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Processes",
                newName: "Process");

            migrationBuilder.RenameColumn(
                name: "IsTemplate",
                table: "Process",
                newName: "IdTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_TypeId",
                table: "Process",
                newName: "IX_Process_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_Tail",
                table: "Process",
                newName: "IX_Process_Tail");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_PriorityId",
                table: "Process",
                newName: "IX_Process_PriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_Head",
                table: "Process",
                newName: "IX_Process_Head");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndVerificationDate",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovedAt",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SignedAt",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcessId",
                table: "Passports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Process",
                table: "Process",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Process_ProcessId",
                table: "Passports",
                column: "ProcessId",
                principalTable: "Process",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Priorities_PriorityId",
                table: "Process",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Stages_Head",
                table: "Process",
                column: "Head",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Stages_Tail",
                table: "Process",
                column: "Tail",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Types_TypeId",
                table: "Process",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Process_ProcessId",
                table: "Stages",
                column: "ProcessId",
                principalTable: "Process",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Status_StatusId",
                table: "Stages",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
