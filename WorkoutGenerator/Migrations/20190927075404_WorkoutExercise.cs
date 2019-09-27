using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class WorkoutExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_WorkoutExercise_WorkoutExerciseId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_MuscleExercises_MuscleExercisesId",
                table: "WorkoutExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise");

            migrationBuilder.RenameTable(
                name: "WorkoutExercise",
                newName: "WorkoutExercises");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercise_MuscleExercisesId",
                table: "WorkoutExercises",
                newName: "IX_WorkoutExercises_MuscleExercisesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_WorkoutExercises_WorkoutExerciseId",
                table: "Set",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_MuscleExercises_MuscleExercisesId",
                table: "WorkoutExercises",
                column: "MuscleExercisesId",
                principalTable: "MuscleExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_WorkoutExercises_WorkoutExerciseId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_MuscleExercises_MuscleExercisesId",
                table: "WorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.RenameTable(
                name: "WorkoutExercises",
                newName: "WorkoutExercise");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercises_MuscleExercisesId",
                table: "WorkoutExercise",
                newName: "IX_WorkoutExercise_MuscleExercisesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_WorkoutExercise_WorkoutExerciseId",
                table: "Set",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_MuscleExercises_MuscleExercisesId",
                table: "WorkoutExercise",
                column: "MuscleExercisesId",
                principalTable: "MuscleExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
