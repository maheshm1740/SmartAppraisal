using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLSmartAppraisal.Migrations.Assessment
{
    /// <inheritdoc />
    public partial class results1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "TestResults");

            migrationBuilder.AddColumn<int>(
                name: "Hike",
                table: "TestResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hike",
                table: "TestResults");

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "TestResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
