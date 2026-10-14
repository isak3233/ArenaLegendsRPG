using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArenaLegendsRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBattleLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterName = table.Column<string>(type: "TEXT", nullable: false),
                    MonsterHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterAttackResist = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterMagicResist = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterAttackDamage = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterMagicDamage = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageDoneByPlayer = table.Column<int>(type: "INTEGER", nullable: false),
                    DamageDoneByMonster = table.Column<int>(type: "INTEGER", nullable: false),
                    IsPlayerWinner = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protections_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Protections_CharacterId",
                table: "Protections",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Protections");
        }
    }
}
