using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DB_Service.Migrations
{
    public partial class create4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependences_Stages_First",
                table: "Dependences");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependences_Stages_Second",
                table: "Dependences");

            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Stages_End",
                table: "Edges");

            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Stages_Start",
                table: "Edges");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Stages_StageId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessId",
                table: "Passports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Old",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "New",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LogId",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Field",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Logs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Start",
                table: "Edges",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "End",
                table: "Edges",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Second",
                table: "Dependences",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "First",
                table: "Dependences",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependences_Stages_First",
                table: "Dependences",
                column: "First",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependences_Stages_Second",
                table: "Dependences",
                column: "Second",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Stages_End",
                table: "Edges",
                column: "End",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Stages_Start",
                table: "Edges",
                column: "Start",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Stages_StageId",
                table: "Tasks",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependences_Stages_First",
                table: "Dependences");

            migrationBuilder.DropForeignKey(
                name: "FK_Dependences_Stages_Second",
                table: "Dependences");

            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Stages_End",
                table: "Edges");

            migrationBuilder.DropForeignKey(
                name: "FK_Edges_Stages_Start",
                table: "Edges");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Stages_StageId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessId",
                table: "Passports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Table",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Old",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "New",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogId",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Field",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Start",
                table: "Edges",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "End",
                table: "Edges",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Second",
                table: "Dependences",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "First",
                table: "Dependences",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_TaskId",
                table: "Comments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependences_Stages_First",
                table: "Dependences",
                column: "First",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependences_Stages_Second",
                table: "Dependences",
                column: "Second",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Stages_End",
                table: "Edges",
                column: "End",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Edges_Stages_Start",
                table: "Edges",
                column: "Start",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Process_ProcessId",
                table: "Passports",
                column: "ProcessId",
                principalTable: "Process",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Priorities_PriorityId",
                table: "Process",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Stages_Head",
                table: "Process",
                column: "Head",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Stages_Tail",
                table: "Process",
                column: "Tail",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Types_TypeId",
                table: "Process",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Process_ProcessId",
                table: "Stages",
                column: "ProcessId",
                principalTable: "Process",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Status_StatusId",
                table: "Stages",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Stages_StageId",
                table: "Tasks",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
