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

    public PokemonService(IMapper mapper, PokemonDb dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;

    }
    private Pokemon MappingToPokemon(PokemonDao pokemon) => mapper.Map<Pokemon>(pokemon);
    private PokemonDao MappingToPokemonDao(Pokemon pokemon) => mapper.Map<PokemonDao>(pokemon);

    //··········GET············
    public async Task<List<Pokemon>> GetAll(CancellationToken token)
    {
        List<PokemonDao> pokemonDao = await dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(pokemon => pokemon.Types)
            .ToListAsync(token);

        List<Pokemon> pokemonList = pokemonDao.Select(pokemon => MappingToPokemon(pokemon)).ToList();
        return pokemonList;
    }

    public async Task<Pokemon> GetById(int id, CancellationToken token)
    {
        PokemonDao? pokemon = await dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Id == id, token);
        Pokemon mappedPokemon = MappingToPokemon(pokemon);
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
        Pokemon mappedPokemon = MappingToPokemon(pokemon);
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
    
    
}