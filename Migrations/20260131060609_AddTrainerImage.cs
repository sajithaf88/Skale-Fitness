using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkaleFitnessMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Trainers");
        }
    }
}
