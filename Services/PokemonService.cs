using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Migrations;
using PokemonAPI.Models;
using PokemonAPI.Utils;
using PokemonAbilityDao = PokemonAPI.Models.PokemonAbilityDao;

namespace PokemonAPI.Services;

public class PokemonService : IPokemonService
{
    private readonly IMapper _mapper;
    private readonly PokemonDb _dbContext;
    private readonly PokemonUtils _helper;
    private readonly TransactionService _transactionService;

    public PokemonService(IMapper _mapper, PokemonDb _dbContext, PokemonUtils _helper,
        TransactionService _transactionService)
    {
        this._mapper = _mapper;
        this._dbContext = _dbContext;
        this._helper = _helper;
        this._transactionService = _transactionService;
    }

    private Pokemon MapToPokemon(PokemonDao pokemon) => _mapper.Map<Pokemon>(pokemon);
    private PokemonDao MapToPokemonDao(Pokemon pokemon) => _mapper.Map<PokemonDao>(pokemon);

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

        if (pokemon == null)
        {
            throw new Exception($"Pokemon {id} doesn't exists.");
        }

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
        // await _transactionService.BeginTransaction();
        try
        {
            await _helper.CheckExistingPokemon(pokemon, token);

            var newTypes = await _helper.CheckPokemonTypes(pokemon, token);

            await _helper.CheckPokemonAbility(pokemon, token);

            pokemon.Types = newTypes;

            _dbContext.Pokemon.Add(pokemon);
            await _dbContext.SaveChangesAsync(token);
            // await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
        //     await _transactionService.RollbackTransaction();
        throw new Exception($"Error creating pokemon: {e.Message}");
        }
    }

    public async Task AddAbility(int id, PokemonAbility newAbility, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            Pokemon pokemonById = await GetById(id, token);
            PokemonDao pokemon = MapToPokemonDao(pokemonById);
            await _helper.CheckAbility(newAbility.Name, token);
    
            var pokemonAbilities = pokemon.PokemonAbility.Select(pa => pa.AbilityName.ToLower()).ToList();
            if (!pokemonAbilities.Contains(newAbility.Name.ToLower()))
            {
                var pokemonAbilityDao = new PokemonAbilityDao()
                {
                    PokemonId = pokemon.Id,
                    AbilityName = newAbility.Name,
                    IsHidden = newAbility.IsHidden,
                };
                _dbContext.PokemonAbility.Add(pokemonAbilityDao);
            }
    
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error adding ability to pokemon: {e.Message}");
        }
    }

    public async Task AddType(int id, TypeDao type, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            Pokemon pokemonById = await GetById(id, token);
            PokemonDao pokemon = MapToPokemonDao(pokemonById);

            var existingTypeInDb = pokemon.Types.Any(t => t.Name.ToLower() == type.Name.ToLower());
            if (existingTypeInDb == false)
            {
                var newType = new TypeDao()
                {
                    Name = type.Name,
                };
                _dbContext.Types.Add(newType);
            }
            
            var pokemonTypes = pokemon.Types.Select(t => t.Name.ToLower()).ToList();
            if (!pokemonTypes.Contains(type.Name.ToLower()))
            {
                var pokemonTypeDao = new TypeDao() // aqui debe ir la tabla de asociacion 
                {
                    Name = type.Name
                };
                _dbContext.Types.Add(pokemonTypeDao);
            }
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();            
            throw new Exception($"Error adding type to pokemon: {e.Message}");
        }
    }


}