using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Utils;

public class PokemonUtils
{
    private readonly PokemonDb _dbContext;
    public PokemonUtils(PokemonDb _dbContext)
    {
        this._dbContext = _dbContext;
    }
     public async Task CheckExistingPokemon(PokemonDao pokemon, CancellationToken token)
    {
        var existingPokemon = await _dbContext.Pokemon.FirstOrDefaultAsync(x => 
                x.Name.ToLower() == pokemon.Name.ToLower() && 
                x.Id == pokemon.Id, token);

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
                await _dbContext.Types.FirstOrDefaultAsync(x => 
                    x.Name.ToLower() == type.Name.ToLower(), token);
            if (existingType == null)
            {
                var newType = new TypeDao { Name = type.Name };
                _dbContext.Types.Add(newType);
                newTypes.Add(newType);
            }
            else
            {
                newTypes.Add(existingType);
            }
        }

        return newTypes;
    }

    public async Task CheckPokemonAbility(PokemonDao pokemon, CancellationToken token)
    {
        var pokemonAbilities = pokemon.PokemonAbility.Select(pa => pa.AbilityName.ToLower()).ToList();

        foreach (var pokemonAbility in pokemon.PokemonAbility)
        {
            await CheckAbility(pokemonAbility.AbilityName, token);

            if (!pokemonAbilities.Contains(pokemonAbility.AbilityName.ToLower()))
            {
                var pokemonAbilityDao = new PokemonAbilityDao()
                {
                    // PokemonId = pokemon.Id,
                    AbilityName = pokemonAbility.AbilityName,
                    IsHidden = pokemonAbility.IsHidden,
                };
                pokemon.PokemonAbility.Add(pokemonAbilityDao);
            }
        }
    }

    public async Task CheckAbility(string abilityName, CancellationToken token)
    {
        var existingAbility = await _dbContext.Ability
            .FirstOrDefaultAsync(a => a.Name.ToLower() == abilityName.ToLower(), token);

        if (existingAbility == null)
        {
            var newAbility = new AbilityDao()
            {
                Name = abilityName,
            };
            _dbContext.Ability.Add(newAbility);
        }
    }


}