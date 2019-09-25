using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class ProgressChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Set");

            migrationBuilder.DropColumn(
                name: "NumberOfSets",
                table: "Set");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "Set",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Workouts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Reps",
                table: "Set",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Set",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSets",
                table: "Set",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
