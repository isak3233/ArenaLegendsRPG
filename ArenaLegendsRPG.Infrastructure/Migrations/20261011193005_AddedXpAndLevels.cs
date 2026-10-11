using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArenaLegendsRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedXpAndLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Xp",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Xp",
                table: "Characters");
        }
    }
}
