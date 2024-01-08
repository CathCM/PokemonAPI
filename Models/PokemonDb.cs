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
            .UsingEntity<Dictionary<string, object>>("PokemonAbilities",
                r => r.HasOne<AbilityDao>().WithMany().HasForeignKey("AbilityName"),
                l => l.HasOne<PokemonDao>().WithMany().HasForeignKey("PokemonId"),
                je =>
                {
                    je.HasKey("AbilityName", "PokemonId");
                    je.HasData(
                        new { AbilityName = "Ability Test", PokemonId = 1 }
                    );
                });

        modelBuilder.Entity<PokemonDao>()
        .HasMany(e => e.Types)
        .WithMany(e => e.Pokemons)
        .UsingEntity("PokemonTypes");
        modelBuilder.Entity<PokemonDao>().HasData(
            new PokemonDao
            {
                Id = 1,
                Name = "Pokemon Test"
            }
            );
        modelBuilder.Entity<AbilityDao>().HasData(
                new AbilityDao
                {
                    Name = "Ability Test"
                }
        );

    }
}
