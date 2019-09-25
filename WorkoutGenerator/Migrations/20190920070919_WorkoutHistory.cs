using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class WorkoutHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Workout",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WorkoutHistoryId",
                table: "MuscleExercises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutHistory_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_WorkoutHistoryId",
                table: "MuscleExercises",
                column: "WorkoutHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistory_WorkoutId",
                table: "WorkoutHistory",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_MuscleExercises_WorkoutHistory_WorkoutHistoryId",
                table: "MuscleExercises",
                column: "WorkoutHistoryId",
                principalTable: "WorkoutHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MuscleExercises_WorkoutHistory_WorkoutHistoryId",
                table: "MuscleExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "WorkoutHistory");

            migrationBuilder.DropIndex(
                name: "IX_MuscleExercises_WorkoutHistoryId",
                table: "MuscleExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutHistoryId",
                table: "MuscleExercises");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Workout",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Template_TemplateId",
                table: "Workout",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
