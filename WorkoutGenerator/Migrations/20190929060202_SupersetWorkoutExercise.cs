using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class SupersetWorkoutExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupersetName",
                table: "WorkoutExercises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutExerciseId1",
                table: "Set",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Set_WorkoutExerciseId1",
                table: "Set",
                column: "WorkoutExerciseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_WorkoutExercises_WorkoutExerciseId1",
                table: "Set",
                column: "WorkoutExerciseId1",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_WorkoutExercises_WorkoutExerciseId1",
                table: "Set");

            migrationBuilder.DropIndex(
                name: "IX_Set_WorkoutExerciseId1",
                table: "Set");

            migrationBuilder.DropColumn(
                name: "SupersetName",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutExerciseId1",
                table: "Set");
        }
    }
}
