using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models;

public class TypeDao
{
    [Key]
    public string Name { get; set; }
    [JsonIgnore]
    public List<PokemonDao> Pokemons { get; set; } = new();
}
