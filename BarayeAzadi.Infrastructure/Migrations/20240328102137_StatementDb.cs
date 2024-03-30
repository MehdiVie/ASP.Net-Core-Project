using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarayeAzadi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatementDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Statements");

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Statements");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Statements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
