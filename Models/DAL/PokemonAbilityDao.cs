using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonAbilityDao
{
    public int PokemonId { get; set; }
    public string AbilityName { get; set; }
    public bool IsHidden { get; set; }
    
}