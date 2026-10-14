using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArenaLegendsRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropToBattleLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BattleFinished",
                table: "BattleLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleFinished",
                table: "BattleLogs");
        }
    }
}
