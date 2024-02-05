namespace PokemonAPI.Models;

public class PokemonTypesDao
{
        public int PokemonId { get; set; }
        public PokemonDao Pokemon { get; set; }
    
        public string TypesName { get; set; }
        public TypeDao Type { get; set; }
    
}