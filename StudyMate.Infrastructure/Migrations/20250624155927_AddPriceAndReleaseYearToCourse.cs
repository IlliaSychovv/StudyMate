using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceAndReleaseYearToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Courses");
        }
    }
}
