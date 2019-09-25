using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class WorkoutHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_WorkoutHistory_WorkoutHistoryId",
                table: "MuscleExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistory_Workouts_WorkoutId",
                table: "WorkoutHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutHistory",
                table: "WorkoutHistory");

            migrationBuilder.RenameTable(
                name: "WorkoutHistory",
                newName: "WorkoutHistories");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutHistory_WorkoutId",
                table: "WorkoutHistories",
                newName: "IX_WorkoutHistories_WorkoutId");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "WorkoutHistories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutHistories",
                table: "WorkoutHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_WorkoutHistories_WorkoutHistoryId",
                table: "MuscleExercises",
                column: "WorkoutHistoryId",
                principalTable: "WorkoutHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_Workouts_WorkoutId",
                table: "WorkoutHistories",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_WorkoutHistories_WorkoutHistoryId",
                table: "MuscleExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_Workouts_WorkoutId",
                table: "WorkoutHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutHistories",
                table: "WorkoutHistories");

            migrationBuilder.RenameTable(
                name: "WorkoutHistories",
                newName: "WorkoutHistory");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutHistories_WorkoutId",
                table: "WorkoutHistory",
                newName: "IX_WorkoutHistory_WorkoutId");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "WorkoutHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutHistory",
                table: "WorkoutHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_WorkoutHistory_WorkoutHistoryId",
                table: "MuscleExercises",
                column: "WorkoutHistoryId",
                principalTable: "WorkoutHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistory_Workouts_WorkoutId",
                table: "WorkoutHistory",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
