namespace PokemonAPI.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PokemonAbility> Abilities { get; set; }
    public List<PokemonStat> Stats { get; set; }
    public List<Types> Types { get; set; }
}


