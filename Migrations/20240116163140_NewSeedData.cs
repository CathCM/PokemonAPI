using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonAbility",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Ability Test", 1 });

            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Pokemon",
                columns: new[] { "Id", "Attack", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[,]
                {
                    { 13, 26, 65, 63, "Pokemoncito", 31, 20, 200 },
                    { 123, 32, 35, 20, "Pokemon Test", 51, 40, 15 }
                });

            migrationBuilder.InsertData(
                table: "PokemonAbility",
                columns: new[] { "AbilityName", "PokemonId", "IsHidden" },
                values: new object[] { "Ability Test", 123, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PokemonAbility",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Ability Test", 123 });

            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.InsertData(
                table: "Pokemon",
                columns: new[] { "Id", "Attack", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[] { 1, 32, 35, 20, "Pokemon Test", 51, 40, 15 });

            migrationBuilder.InsertData(
                table: "PokemonAbility",
                columns: new[] { "AbilityName", "PokemonId", "IsHidden" },
                values: new object[] { "Ability Test", 1, false });
        }
    }
}
