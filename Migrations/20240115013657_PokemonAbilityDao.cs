using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class PokemonAbilityDao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityName = table.Column<string>(type: "TEXT", nullable: false),
                    IsHidden = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbility", x => new { x.PokemonId, x.AbilityName });
                    table.ForeignKey(
                        name: "FK_PokemonAbility_Ability_AbilityName",
                        column: x => x.AbilityName,
                        principalTable: "Ability",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbility_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PokemonAbility",
                columns: new[] { "AbilityName", "PokemonId", "IsHidden" },
                values: new object[] { "Ability Test", 1, false });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbility_AbilityName",
                table: "PokemonAbility",
                column: "AbilityName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbility");

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    AbilityName = table.Column<string>(type: "TEXT", nullable: false),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => new { x.AbilityName, x.PokemonId });
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Ability_AbilityName",
                        column: x => x.AbilityName,
                        principalTable: "Ability",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PokemonAbilities",
                columns: new[] { "AbilityName", "PokemonId" },
                values: new object[] { "Ability Test", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId");
        }
    }
}
