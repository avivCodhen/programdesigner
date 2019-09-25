using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class WorkoutChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_Workout_WorkoutId",
                table: "MuscleExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistory_Workout_WorkoutId",
                table: "WorkoutHistory");

            migrationBuilder.DropIndex(
                name: "IX_MuscleExercises_WorkoutId",
                table: "MuscleExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "MuscleExercises");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "Workouts");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_TemplateId",
                table: "Workouts",
                newName: "IX_Workouts_TemplateId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "WorkoutHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistory_Workouts_WorkoutId",
                table: "WorkoutHistory",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistory_Workouts_WorkoutId",
                table: "WorkoutHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Template_TemplateId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "WorkoutHistory");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "Workout");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_TemplateId",
                table: "Workout",
                newName: "IX_Workout_TemplateId");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "MuscleExercises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_WorkoutId",
                table: "MuscleExercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_Workout_WorkoutId",
                table: "MuscleExercises",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistory_Workout_WorkoutId",
                table: "WorkoutHistory",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
