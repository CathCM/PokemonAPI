using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonsId",
                table: "PokemonTypes");

            migrationBuilder.RenameColumn(
                name: "PokemonsId",
                table: "PokemonTypes",
                newName: "PokemonId");

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hp",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialAttack",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialDefense",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Pokemon",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attack", "Defense", "Hp", "SpecialAttack", "SpecialDefense", "Speed" },
                values: new object[] { 32, 35, 20, 51, 40, 15 });

            migrationBuilder.InsertData(
                table: "Type",
                column: "Name",
                value: "Type Test");

            migrationBuilder.InsertData(
                table: "PokemonTypes",
                columns: new[] { "PokemonId", "TypesName" },
                values: new object[] { 1, "Type Test" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonId",
                table: "PokemonTypes",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonId",
                table: "PokemonTypes");

            migrationBuilder.DeleteData(
                table: "PokemonTypes",
                keyColumns: new[] { "PokemonId", "TypesName" },
                keyValues: new object[] { 1, "Type Test" });

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Name",
                keyValue: "Type Test");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Hp",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "SpecialAttack",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "SpecialDefense",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Pokemon");

            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: "PokemonTypes",
                newName: "PokemonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Pokemon_PokemonsId",
                table: "PokemonTypes",
                column: "PokemonsId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
