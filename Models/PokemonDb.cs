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
    public DbSet<PokemonAbilityDao> PokemonAbility { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<PokemonAbilityDao>().HasKey(p => new {p.PokemonId, p.AbilityName});
        modelBuilder.Entity<PokemonDao>()
            .HasMany(e => e.PokemonAbility)
            .WithOne()
            .HasForeignKey(k => k.PokemonId);
        modelBuilder.Entity<AbilityDao>()
            .HasMany(e => e.PokemonAbility)
            .WithOne()
            .HasForeignKey(k => k.AbilityName);

        modelBuilder.Entity<PokemonDao>()
            .HasMany(e => e.Types)
            .WithMany(e => e.Pokemons)
            .UsingEntity<Dictionary<string, object>>("PokemonTypes",
                r => r.HasOne<TypeDao>().WithMany().HasForeignKey("TypesName"),
                l => l.HasOne<PokemonDao>().WithMany().HasForeignKey("PokemonId"),
                je =>
                {
                    je.HasKey("PokemonId", "TypesName");
                    je.HasData(
                        new { PokemonId = 1, TypesName = "Type Test" }
                    );
                });

        modelBuilder.Entity<PokemonDao>().HasData(
            new PokemonDao
            {
                Id = 1,
                Name = "Pokemon Test",
                Hp = 20,
                Defense = 35,
                Attack = 32,
                SpecialAttack = 51,
                SpecialDefense = 40,
                Speed = 15,
            }
        );
        modelBuilder.Entity<AbilityDao>().HasData(
            new AbilityDao
            {
                Name = "Ability Test"
            }
        );
        modelBuilder.Entity<TypeDao>().HasData(
            new TypeDao()
            {
                Name = "Type Test"
            }
        );
        modelBuilder.Entity<PokemonAbilityDao>().HasData(
            new PokemonAbilityDao()
            {
                PokemonId = 1,
                AbilityName = "Ability Test",
                IsHidden = false
            }
        );
    }
}