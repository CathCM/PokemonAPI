using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDBStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Type_TypesName",
                table: "PokemonTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Types_TypesName",
                table: "PokemonTypes",
                column: "TypesName",
                principalTable: "Types",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Types_TypesName",
                table: "PokemonTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Type_TypesName",
                table: "PokemonTypes",
                column: "TypesName",
                principalTable: "Type",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
