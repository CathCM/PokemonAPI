using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PokemonAPI.Models
{
    public class PokemonAbilityDao
    {
        [Key]
        [JsonIgnore]
        public int PokemonId { get; set; }
        public string AbilityName { get; set; }
        public bool IsHidden { get; set; }
    }
}