using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class AddedNameToWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workout",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workout");
        }
    }
}
