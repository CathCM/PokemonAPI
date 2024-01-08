using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;
public class AbilityDao
{
    [Key]
    public string Name { get; set; }
    // public List<PokemonDao> PokemonsByAbility { get; } = new();
    // public List<PokemonAbilityDao> PokemonAbilities { get; } = new();
}