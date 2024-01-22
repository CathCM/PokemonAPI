using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class PokemonDbModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 1, "Type Test" });

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[] { 123, "Type Test" });

            migrationBuilder.InsertData(
                table: "Type",
                column: "Name",
                value: "Fire");

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[] { 13, "Fire" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 13, "Fire" });

            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 123, "Type Test" });

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Fire");

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[] { 1, "Type Test" });
        }
    }
}
