using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonDao
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public List<PokemonAbilityDao> PokemonAbility { get; set; } = new();
    public int Hp { get; set; }
    public int Defense { get; set; }
    public int Attack { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
    public List<TypeDao> Types { get; set; } = new();
}