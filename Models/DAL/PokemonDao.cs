using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonDao
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    // public List<AbilityDao> Abilities { get; } = new();
    // public List<PokemonAbilityDao> PokemonAbilities { get; } = new();
    // // public List<PokemonStat> Stats { get; set; }
    // public List<TypeDao> Types { get; } = new();
    // public List <>
}


