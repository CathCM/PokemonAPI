using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonDao
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<AbilityDao> Abilities { get;set; } = new();
    // public List<PokemonStat> Stats { get; set; }
    public List<TypeDao> Types { get; set;} = new();
}


