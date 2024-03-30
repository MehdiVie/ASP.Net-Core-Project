using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarayeAzadi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatementDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brief",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brief",
                table: "Statements");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Statements");
        }
    }
}
