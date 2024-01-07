using PokemonAPI.Models;
using Microsoft.EntityFrameworkCore;
public class PokemonDb : DbContext
{
    public PokemonDb(DbContextOptions <PokemonDb> options) : base(options) {
        
     }
    public DbSet<PokemonDao> Pokemons { get; set; } = null!;
    public DbSet<AbilityDao> Ability{ get; set; } = null!;
    public DbSet<TypeDao> Type { get; set; } = null!;
}
