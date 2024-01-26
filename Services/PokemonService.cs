using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly IMapper mapper;
    private readonly PokemonDb dbContext;
    private readonly AbilityService abilityService;
    private readonly TypeService typeService;

    public PokemonService(IMapper mapper, PokemonDb dbContext, AbilityService abilityService, TypeService typeService)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        this.typeService = typeService;
        this.abilityService = abilityService;
    }

    private Pokemon MapToPokemon(PokemonDao pokemon) => mapper.Map<Pokemon>(pokemon);
    private PokemonDao MapToPokemonDao(Pokemon pokemon) => mapper.Map<PokemonDao>(pokemon);

    //··········GET············
    public async Task<List<Pokemon>> GetAll(CancellationToken token)
    {
        List<PokemonDao> pokemonDao = await dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(p => p.Types)
            .ToListAsync(token);

        List<Pokemon> pokemonList = pokemonDao.Select(pokemon => MapToPokemon(pokemon)).ToList();
        return pokemonList;
    }

    public async Task<Pokemon> GetById(int id, CancellationToken token)
    {
        PokemonDao? pokemon = await dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Id == id, token);
        Pokemon mappedPokemon = MapToPokemon(pokemon);
        return mappedPokemon;
    }

    public async Task<List<string>> GetNames(CancellationToken token)
    {
        var pokemons = await GetAll(token);
        List<string> pokemonNames = pokemons.Select(x => x.Name).ToList();
        return pokemonNames;
    }

    public async Task<Pokemon> GetByName(string name, CancellationToken token)
    {
        PokemonDao? pokemon = await dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower(), token);
        Pokemon mappedPokemon = MapToPokemon(pokemon);
        return mappedPokemon;
    }

    public async Task<List<PokemonAbility>> GetAbilities(int id, CancellationToken token)
    {
        Pokemon pokemon = await GetById(id, token);
        var pokemonAbilities = pokemon.Abilities.ToList();
        return pokemonAbilities;
    }

    public async Task<List<PokemonStat>> GetStats(int id, CancellationToken token)
    {
        Pokemon pokemon = await GetById(id, token);
        List<PokemonStat> pokemonStats = pokemon.Stats.ToList();
        return pokemonStats;
    }

    public async Task<List<string>> GetType(int id, CancellationToken token)
    {
        var pokemon = await GetById(id, token);
        List<string> types = pokemon.Types.Select(x => x.Name).ToList();
        return types;
    }

    //··········POST············

   public async Task Create(PokemonDao pokemon, CancellationToken token)
{
    try
    {
        await CheckExistingPokemon(pokemon, token);

        var newTypes = await CheckPokemonTypes(pokemon, token);

        await CheckPokemonAbilities(pokemon, token);

        pokemon.Types = newTypes;

        dbContext.Pokemon.Add(pokemon);
        await dbContext.SaveChangesAsync(token);
    }
    catch (Exception ex)
    {
        throw new Exception($"Error creating pokemon: {ex.Message}");
    }
}

private async Task CheckExistingPokemon(PokemonDao pokemon, CancellationToken token)
{
    var existingPokemon = await dbContext.Pokemon.FirstOrDefaultAsync(x => x.Name.ToLower() == pokemon.Name.ToLower(), token);
    if (existingPokemon != null)
    {
        throw new Exception("This pokemon already exists.");
    }
}

private async Task<List<TypeDao>> CheckPokemonTypes(PokemonDao pokemon, CancellationToken token)
{
    var newTypes = new List<TypeDao>();

    foreach (var type in pokemon.Types)
    {
        var existingType = await dbContext.Types.FirstOrDefaultAsync(x => x.Name.ToLower() == type.Name.ToLower(), token);
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

private async Task CheckPokemonAbilities(PokemonDao pokemon, CancellationToken token)
{
    var pokemonAbilities = pokemon.PokemonAbility.Select(pa => pa.AbilityName.ToLower()).ToList();

    foreach (var pokemonAbility in pokemon.PokemonAbility)
    {
        var existingAbility = await dbContext.Ability.FirstOrDefaultAsync(a => a.Name.ToLower() == pokemonAbility.AbilityName.ToLower(), token);

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