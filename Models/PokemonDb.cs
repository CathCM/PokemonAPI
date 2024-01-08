using PokemonAPI.Models;
using Microsoft.EntityFrameworkCore;
public class PokemonDb : DbContext
{
    public PokemonDb(DbContextOptions<PokemonDb> options) : base(options)
    {

    }
    public DbSet<PokemonDao> Pokemon { get; set; } = null!;
    public DbSet<AbilityDao> Ability { get; set; } = null!;
    public DbSet<TypeDao> Type { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonDao>()
            .HasMany(e => e.Abilities)
            .WithMany(e => e.Pokemons)
            .UsingEntity("PokemonAbilities");
            
            modelBuilder.Entity<PokemonDao>()
            .HasMany(e => e.Types)
            .WithMany(e => e.Pokemons)
            .UsingEntity("PokemonTypes");
            
    }
}
