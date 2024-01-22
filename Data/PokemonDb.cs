using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

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
        ConfigureEntities(modelBuilder);
        SeedData(modelBuilder);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonAbilityDao>().HasKey(p => new { p.PokemonId, p.AbilityName });
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
                        new { PokemonId = 1, TypesName = "Water" },
                        new { PokemonId = 1, TypesName = "Flying" },
                        new { PokemonId = 2, TypesName = "Fire" }
                    );
                });
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonDao>().HasData(
            new PokemonDao
            {
                Id = 1,
                Name = "Squirtle",
                Hp = 44,
                Defense = 65,
                Attack = 48,
                SpecialAttack = 50,
                SpecialDefense = 64,
                Speed = 43,
            },
            new PokemonDao
            {
                Id = 2,
                Name = "Charmander",
                Hp = 39,
                Defense = 43,
                Attack = 52,
                SpecialAttack = 60,
                SpecialDefense = 50,
                Speed = 65,
            },
            new PokemonDao
            {
                Id = 3,
                Name = "Pidgey",
                Hp = 40,
                Defense = 40,
                Attack = 45,
                SpecialAttack = 35,
                SpecialDefense = 35,
                Speed = 56,
            }
        );
        modelBuilder.Entity<AbilityDao>().HasData(
            new AbilityDao
            {
                Name = "Torrent"
            },
            new AbilityDao
            {
                Name = "Blaze"
            },
            new AbilityDao
            {
                Name = "Keen Eye"
            }
        );
        modelBuilder.Entity<TypeDao>().HasData(
            new TypeDao()
            {
                Name = "Water"
            },
            new TypeDao()
            {
                Name = "Fire"
            },
            new TypeDao()
            {
                Name = "Flying"
            }
        );
        modelBuilder.Entity<PokemonAbilityDao>().HasData(
            new PokemonAbilityDao()
            {
                PokemonId = 1,
                AbilityName = "Torrent",
                IsHidden = false
            },
            new PokemonAbilityDao()
            {
                PokemonId = 2,
                AbilityName = "Blaze",
                IsHidden = false
            },
            new PokemonAbilityDao()
            {
                PokemonId = 3,
                AbilityName = "Keen Eye",
                IsHidden = true
            }
        );
    }
}
