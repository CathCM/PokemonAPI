using Microsoft.EntityFrameworkCore.Migrations;
using PokemonAPI.Models;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPokemonId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Pokemon",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER").Annotation("Sqlite:Autoincrement", false);
            

              migrationBuilder.DropPrimaryKey(
                  name: "PK_Pokemon",
                  table: "Pokemon");

              migrationBuilder.AddPrimaryKey(
                  name: "Id",
                  table: "Pokemon",
                  column:"Id");
        }
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
