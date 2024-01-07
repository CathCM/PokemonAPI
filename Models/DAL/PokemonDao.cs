using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonDao
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    // public List<PokemonAbility> Abilities { get;K set; }
    // public List<PokemonStat> Stats { get; set; }
    // public List<Types> Types { get; set; }
}


