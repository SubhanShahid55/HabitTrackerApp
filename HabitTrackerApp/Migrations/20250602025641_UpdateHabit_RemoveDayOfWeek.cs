using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHabit_RemoveDayOfWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Habits");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Habits",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Habits");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
