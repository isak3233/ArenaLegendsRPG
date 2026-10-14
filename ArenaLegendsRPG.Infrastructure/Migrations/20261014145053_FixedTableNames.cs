using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArenaLegendsRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protections_Characters_CharacterId",
                table: "Protections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Protections",
                table: "Protections");

            migrationBuilder.RenameTable(
                name: "Protections",
                newName: "BattleLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Protections_CharacterId",
                table: "BattleLogs",
                newName: "IX_BattleLogs_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleLogs",
                table: "BattleLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BattleLogs_Characters_CharacterId",
                table: "BattleLogs",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleLogs_Characters_CharacterId",
                table: "BattleLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleLogs",
                table: "BattleLogs");

            migrationBuilder.RenameTable(
                name: "BattleLogs",
                newName: "Protections");

            migrationBuilder.RenameIndex(
                name: "IX_BattleLogs_CharacterId",
                table: "Protections",
                newName: "IX_Protections_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Protections",
                table: "Protections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Protections_Characters_CharacterId",
                table: "Protections",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
