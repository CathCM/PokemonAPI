using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using PokemonAPI.Utils;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly IMapper _mapper;
    private readonly PokemonDb _dbContext;
    private readonly PokemonUtils _helper;
    private readonly TransactionService _transactionService;

    public PokemonService(IMapper _mapper, PokemonDb _dbContext, PokemonUtils _helper, TransactionService _transactionService)
    {
        this._mapper = _mapper;
        this._dbContext = _dbContext;
        this._helper = _helper;
        this._transactionService = _transactionService;
    }

    private Pokemon MapToPokemon(PokemonDao pokemon) => _mapper.Map<Pokemon>(pokemon);

    //··········GET············
    public async Task<List<Pokemon>> GetAll(CancellationToken token)
    {
        List<PokemonDao> pokemonDao = await _dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(p => p.Types)
            .ToListAsync(token);

        List<Pokemon> pokemonList = pokemonDao.Select(pokemon => MapToPokemon(pokemon)).ToList();
        return pokemonList;
    }

    public async Task<Pokemon> GetById(int id, CancellationToken token)
    {
        PokemonDao? pokemon = await _dbContext.Pokemon
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
        PokemonDao? pokemon = await _dbContext.Pokemon
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
        await _transactionService.BeginTransaction();
        try
        {
            await _helper.CheckExistingPokemon(pokemon, token);

            var newTypes = await _helper.CheckPokemonTypes(pokemon, token);

            await _helper.CheckPokemonAbilities(pokemon, token);

            pokemon.Types = newTypes;

            _dbContext.Pokemon.Add(pokemon);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception ex)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error creating pokemon: {ex.Message}");
        }
    }

   
}