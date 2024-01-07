using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models;

public class TypeDao
{
    [Key]
    public string Name { get; set; }
}
