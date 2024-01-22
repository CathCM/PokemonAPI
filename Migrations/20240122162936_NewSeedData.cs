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
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 1, "Type Test" });

            migrationBuilder.DeleteData(
                table: "Ability",
                keyColumn: "Name",
                keyValue: "Ability Test");

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Type Test");

            migrationBuilder.InsertData(
                table: "Ability",
                column: "Name",
                values: new object[]
                {
                    "Blaze",
                    "Keen Eye",
                    "Torrent"
                });

            migrationBuilder.UpdateData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attack", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[] { 48, 65, 44, "Squirtle", 50, 64, 43 });

            migrationBuilder.InsertData(
                table: "Pokemon",
                columns: new[] { "Id", "Attack", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[,]
                {
                    { 2, 52, 43, 39, "Charmander", 60, 50, 65 },
                    { 3, 45, 40, 40, "Pidgey", 35, 35, 56 }
                });

            migrationBuilder.InsertData(
                table: "Type",
                column: "Name",
                values: new object[]
                {
                    "Fire",
                    "Flying",
                    "Water"
                });

            migrationBuilder.InsertData(
                table: "PokemonAbility",
                columns: new[] { "AbilityName", "PokemonId", "IsHidden" },
                values: new object[,]
                {
                    { "Torrent", 1, false },
                    { "Blaze", 2, false },
                    { "Keen Eye", 3, true }
                });

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[,]
                {
                    { 1, "Flying" },
                    { 1, "Water" },
                    { 2, "Fire" },
                    { 3, "Flying" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonAbility",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Torrent", 1 });

            migrationBuilder.DeleteData(
                table: "PokemonAbility",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Blaze", 2 });

            migrationBuilder.DeleteData(
                table: "PokemonAbility",
                keyColumns: new[] { "AbilityName", "PokemonId" },
                keyValues: new object[] { "Keen Eye", 3 });

            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 1, "Flying" });

            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 1, "Water" });

            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 2, "Fire" });

            migrationBuilder.DeleteData(
                table: "Ability",
                keyColumn: "Name",
                keyValue: "Blaze");

            migrationBuilder.DeleteData(
                table: "Ability",
                keyColumn: "Name",
                keyValue: "Keen Eye");

            migrationBuilder.DeleteData(
                table: "Ability",
                keyColumn: "Name",
                keyValue: "Torrent");

            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Fire");

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Flying");

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Water");

            migrationBuilder.InsertData(
                table: "Ability",
                column: "Name",
                value: "Ability Test");

            migrationBuilder.UpdateData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attack", "Defense", "Hp", "Name", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[] { 32, 35, 20, "Pokemon Test", 51, 40, 15 });

            migrationBuilder.InsertData(
                table: "Type",
                column: "Name",
                value: "Type Test");

            migrationBuilder.InsertData(
                table: "PokemonAbility",
                columns: new[] { "AbilityName", "PokemonId", "IsHidden" },
                values: new object[] { "Ability Test", 1, false });

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[] { 1, "Type Test" });
        }
    }
}
