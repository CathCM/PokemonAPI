using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilitiesName",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Pokemon_PokemonsId",
                table: "PokemonAbilities");

            migrationBuilder.RenameColumn(
                name: "PokemonsId",
                table: "PokemonAbilities",
                newName: "PokemonId");

            migrationBuilder.RenameColumn(
                name: "AbilitiesName",
                table: "PokemonAbilities",
                newName: "AbilityName");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbilities_PokemonsId",
                table: "PokemonAbilities",
                newName: "IX_PokemonAbilities_PokemonId");

            migrationBuilder.InsertData(
                table: "Ability",
                column: "Name",
                value: "Ability Test");

            migrationBuilder.InsertData(
                table: "Pokemon",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pokemon Test" });

            migrationBuilder.InsertData(
                table: "PokemonAbilities",
                columns: new[] { "AbilityName", "PokemonId" },
                values: new object[] { "Ability Test", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityName",
                table: "PokemonAbilities",
                column: "AbilityName",
                principalTable: "Ability",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Pokemon_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilityName",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Pokemon_PokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DeleteData(
                table: "PokemonAbilities",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Ability Test", 1 });

            migrationBuilder.DeleteData(
                table: "Ability",
                keyColumn: "Name",
                keyValue: "Ability Test");

            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "PokemonAbilities",
                newName: "PokemonsId");

            migrationBuilder.RenameColumn(
                name: "AbilityName",
                table: "PokemonAbilities",
                newName: "AbilitiesName");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonAbilities_PokemonId",
                table: "PokemonAbilities",
                newName: "IX_PokemonAbilities_PokemonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Ability_AbilitiesName",
                table: "PokemonAbilities",
                column: "AbilitiesName",
                principalTable: "Ability",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Pokemon_PokemonsId",
                table: "PokemonAbilities",
                column: "PokemonsId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
