using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStreakFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastCompletedDate",
                table: "Habits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreakCount",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCompletedDate",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "StreakCount",
                table: "Habits");
        }
    }
}
