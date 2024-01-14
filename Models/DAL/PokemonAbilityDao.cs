using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class PokemonAbilityDao
{
    public AbilityDao AbilityName { get; set; }
    public bool IsHidden { get; set; }
    
}