using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Utils;

public class PokemonUtils
{
    private readonly PokemonDb dbContext;
    
    public PokemonUtils(PokemonDb dbContext)
    {
        this.dbContext = dbContext;
    }
     public async Task CheckExistingPokemon(PokemonDao pokemon, CancellationToken token)
    {
        var existingPokemon =
            await dbContext.Pokemon.FirstOrDefaultAsync(x => x.Name.ToLower() == pokemon.Name.ToLower(), token);
        if (existingPokemon != null)
        {
            throw new Exception("This pokemon already exists.");
        }
    }

    public async Task<List<TypeDao>> CheckPokemonTypes(PokemonDao pokemon, CancellationToken token)
    {
        var newTypes = new List<TypeDao>();

        foreach (var type in pokemon.Types)
        {
            var existingType =
                await dbContext.Types.FirstOrDefaultAsync(x => x.Name.ToLower() == type.Name.ToLower(), token);
            if (existingType == null)
            {
                var newType = new TypeDao { Name = type.Name };
                dbContext.Types.Add(newType);
                newTypes.Add(newType);
            }
            else
            {
                newTypes.Add(existingType);
            }
        }

        return newTypes;
    }

    public async Task CheckPokemonAbilities(PokemonDao pokemon, CancellationToken token)
    {
        var pokemonAbilities = pokemon.PokemonAbility.Select(pa => pa.AbilityName.ToLower()).ToList();

        foreach (var pokemonAbility in pokemon.PokemonAbility)
        {
            var existingAbility =
                await dbContext.Ability.FirstOrDefaultAsync(
                    a => a.Name.ToLower() == pokemonAbility.AbilityName.ToLower(), token);

            if (existingAbility == null)
            {
                var newAbility = new AbilityDao()
                {
                    Name = pokemonAbility.AbilityName,
                };
                dbContext.Ability.Add(newAbility);
            }

            if (!pokemonAbilities.Contains(pokemonAbility.AbilityName.ToLower()))
            {
                var pokemonAbilityDao = new PokemonAbilityDao()
                {
                    AbilityName = pokemonAbility.AbilityName,
                    IsHidden = pokemonAbility.IsHidden,
                };
                pokemon.PokemonAbility.Add(pokemonAbilityDao);
            }
        }
    }
}