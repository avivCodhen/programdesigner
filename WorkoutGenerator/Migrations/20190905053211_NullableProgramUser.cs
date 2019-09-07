using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutGenerator.Migrations
{
    public partial class NullableProgramUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Programs",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Programs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
