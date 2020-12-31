using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTasks.Migrations
{
    public partial class AssignedMemberidToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_AssignedMemberId",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedMemberId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_AssignedMemberId",
                table: "Tasks",
                column: "AssignedMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_AssignedMemberId",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "AssignedMemberId",
                table: "Tasks",
                type: "uniqueidentifier",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_AssignedMemberId",
                table: "Tasks",
                column: "AssignedMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
