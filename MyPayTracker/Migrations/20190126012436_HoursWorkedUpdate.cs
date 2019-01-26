using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPayTracker.Migrations
{
    public partial class HoursWorkedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "HoursWorked",
                table: "TimeSheets",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HoursWorked",
                table: "TimeSheets",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
