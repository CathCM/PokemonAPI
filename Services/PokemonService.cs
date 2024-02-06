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

    public async Task<PokemonDao> GetByIdFromDb(int id, CancellationToken token)
    {
        PokemonDao? pokemon = await _dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Id == id, token);
        return pokemon;
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

            await _helper.CheckPokemonAbility(pokemon, token);

            pokemon.Types = newTypes;

            _dbContext.Pokemon.Add(pokemon);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
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
                var pokemonTypeDao = new TypeDao()
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


    //··········PUT············
    // public async Task Update<T>(int id, string property, T value, PokemonDao pokemon, CancellationToken token)
    // {
    //     await _transactionService.BeginTransaction();
    //     try
    //     {
    //         var existingPokemon = await GetByIdFromDb(id, token);
    //         if (existingPokemon is null)
    //         {
    //             throw new Exception($"Pokemon id {id} doesn't exists");
    //         }
    //
    //         switch (property)
    //         {
    //             case "Name":
    //                 try
    //                 {
    //                     existingPokemon.Name = value.ToString();
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in string value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "Defense":
    //                 try
    //                 {
    //                     int baseStat = Convert.ToInt32(value);
    //                     existingPokemon.Defense = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "Attack":
    //                 try
    //                 {
    //                     var baseStat = Convert.ToInt32(value);
    //                     existingPokemon.Attack = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "Hp":
    //                 try
    //                 {
    //                     var baseStat = Convert.ToInt32(value);
    //                     existingPokemon.Hp = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "SpecialDefense":
    //                 try
    //                 {
    //                     var baseStat = Convert.ToInt32(value);
    //                     existingPokemon.SpecialDefense = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "SpecialAttack":
    //                 try
    //                 {
    //                     var baseStat = Convert.ToInt32(value);
    //                     existingPokemon.SpecialAttack = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //             case "Speed":
    //                 try
    //                 {
    //                     var baseStat = Convert.ToInt32(value);
    //                     existingPokemon.Defense = baseStat;
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     throw new Exception($"The value cannot be converted in int value. {e.Message}");
    //                 }
    //
    //                 break;
    //         }
    //
    //         // existingPokemon.Name = pokemon.Name;
    //         // existingPokemon.PokemonAbility = existingPokemon.PokemonAbility;
    //         // existingPokemon.Attack = pokemon.Attack;
    //         // existingPokemon.Defense = pokemon.Defense;
    //         // existingPokemon.Hp = pokemon.Hp;
    //         // existingPokemon.SpecialDefense = pokemon.SpecialDefense;
    //         // existingPokemon.SpecialAttack = pokemon.SpecialAttack;
    //         // existingPokemon.Speed = pokemon.Speed;
    //         // existingPokemon.Types = pokemon.Types;
    //
    //         await _dbContext.SaveChangesAsync(token);
    //         await _transactionService.CommitTransaction();
    //     }
    //     catch
    //         (Exception e)
    //     {
    //         await _transactionService.RollbackTransaction();
    //         throw new Exception($"Error: {e.Message}");
    //     }
    // }
    // public async Task UpdateType(int id, string typeToChange, string newType, CancellationToken token)
    // {
    //     await _transactionService.BeginTransaction();
    //     try
    //     {
    //         var existingPokemon = await GetByIdFromDb(id, token);
    //         if (existingPokemon == null)
    //         {
    //             throw new Exception($"Pokemon id: {id} doesn't exists");
    //         }
    //
    //         var matchType = existingPokemon.Types.FirstOrDefault(t => t.Name.ToLower() == typeToChange.ToLower());
    //         if (matchType is null)
    //         {
    //             throw new Exception($"Pokemon type: {typeToChange} doesn't exists");
    //         }
    //
    //         matchType.Name = newType;
    //         await _dbContext.SaveChangesAsync(token);
    //         await _transactionService.CommitTransaction();
    //     }
    //     catch (Exception e)
    //     {
    //         await _transactionService.RollbackTransaction();
    //         throw new Exception($"Error updating stats. {e.Message}");
    //     }
    // }

    public async Task UpdateStats(int id, string statName, int baseStat, PokemonDao pokemon,
        CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var existingPokemon = await GetByIdFromDb(id, token);
            if (existingPokemon is null)
            {
                throw new Exception($"Pokemon id {id} doesn't exists");
            }

            switch (statName)
            {
                case "Attack":
                    existingPokemon.Attack = baseStat;
                    break;
                case "Defense":
                    existingPokemon.Defense = baseStat;
                    break;
                case "Hp":
                    existingPokemon.Hp = baseStat;
                    break;
                case "SpecialDefense":
                    existingPokemon.SpecialDefense = baseStat;
                    break;
                case "SpecialAttack":
                    existingPokemon.SpecialAttack = baseStat;
                    break;
                case "Speed":
                    existingPokemon.Speed = baseStat;
                    break;
            }

            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error updating stats. {e.Message}");
        }
    }

    public async Task UpdateName(int id, PokemonDao pokemon, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var updatePokemon = await GetByIdFromDb(id, token);
            updatePokemon.Name = pokemon.Name;
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error changing name: {e.Message}");
        }
    }
    //··········DELETE············

    public async Task Delete(int id, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var pokemonToDelete = await GetByIdFromDb(id, token);
            if (pokemonToDelete == null)
            {
                throw new Exception($"Pokemon id {id} doesn't exists.");
            }

            _dbContext.Pokemon.Remove(pokemonToDelete);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error deleting pokemon {e.Message}");
        }
    }

    public async Task DeleteAbilities(int id, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var pokemon = await GetByIdFromDb(id, token);
            if (pokemon is null)
            {
                throw new Exception($"Pokemon id {id} doesn't exists.");
            }

            _dbContext.PokemonAbility.RemoveRange(pokemon.PokemonAbility);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error deleting abilities {e.Message}");
        }
    }

    public async Task DeleteAbility(int id, string ability, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var pokemon = await GetByIdFromDb(id, token);
            if (pokemon is null)
            {
                throw new Exception($"Pokemon id {id} doesn't exists.");
            }

            var abilityToDelete =
                pokemon.PokemonAbility.FirstOrDefault(a => a.AbilityName.ToLower() == ability.ToLower());
            if (abilityToDelete is null)
            {
                throw new Exception($"Ability {ability} doesn't exists for pokemon {pokemon.Name}.");
            }

            _dbContext.PokemonAbility.Remove(abilityToDelete);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error deleting ability {ability}.");
        }
    }
    public async Task DeleteType(int id, string type, CancellationToken token)
    {
        await _transactionService.BeginTransaction();
        try
        {
            var pokemon = await GetByIdFromDb(id, token);
            if (pokemon is null)
            {
                throw new Exception($"Pokemon id {id} doesn't exists.");
            }

            if (pokemon.Types.Count == 1)
            {

                throw new Exception("Pokemon must belong to at least one type.");
            }

            var typeToDelete =
                pokemon.Types.FirstOrDefault(a => a.Name.ToLower() == type.ToLower());
            if (typeToDelete is null)
            {
                throw new Exception($"Type {type} doesn't exists for pokemon {pokemon.Name}.");
            }

            _dbContext.Types.Remove(typeToDelete);
            await _dbContext.SaveChangesAsync(token);
            await _transactionService.CommitTransaction();
        }
        catch (Exception e)
        {
            await _transactionService.RollbackTransaction();
            throw new Exception($"Error deleting type {type}. {e.Message}");
        }
    }
}