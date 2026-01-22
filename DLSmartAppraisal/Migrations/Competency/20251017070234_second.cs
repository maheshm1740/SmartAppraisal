using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLSmartAppraisal.Migrations.Competency
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CeatedDate",
                table: "Competency",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Competency",
                newName: "CeatedDate");
        }
    }
}
