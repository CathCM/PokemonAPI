using PokemonAPI.Models;
using Microsoft.EntityFrameworkCore;
public class PokemonDb : DbContext
{
    public PokemonDb(DbContextOptions <PokemonDb> options) : base(options) {
        
     }
    public DbSet<PokemonDao> PokemonDao { get; set; } = null!;
    public DbSet<AbilityDao> AbilityDao { get; set; } = null!;
    public DbSet<TypeDao> TypeDao { get; set; } = null!;
}
