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
    //     modelBuilder.Entity<PokemonDao>()
    //         .HasMany(e => e.Abilities)
    //         .WithMany(e => e.PokemonsByAbility)
    //         .UsingEntity<PokemonAbilityDao>("PokemonAbilities",
    //         l => l.HasOne<AbilityDao>().WithMany(e => e.PokemonAbilities),
    //         r => r.HasOne<PokemonDao>().WithMany(e => e.PokemonAbilities));
            
            // modelBuilder.Entity<AbilityDao>()
            // .HasMany(e => e.Pokemons)
            // .WithMany(e => e.Abilities)
            // .UsingEntity<PokemonAbilityDao>("PokemonAbilities",
            // r => r.HasOne<PokemonDao>().WithMany().HasForeignKey(e => e.PokemonId)),
            // l => l.HasOne<AbilityDao>().WithMany().HasForeignKey(e => e.AbilityId);
            
    }
}
