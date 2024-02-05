using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class AbilityService : IAbilityService
{
    private readonly IMapper _mapper;
    private readonly PokemonDb _dbContext;

    public AbilityService(IMapper _mapper, PokemonDb _dbContext)
    {
        this._mapper = _mapper;
        this._dbContext = _dbContext;
    }

    public async Task<List<string>> GetAll(CancellationToken token)
    {
        var abilities = await _dbContext.Ability.Select(x => x.Name).ToListAsync(token);
        return abilities;
    }

    public async Task<List<Pokemon>> GetAllByAbility(List<string> abilities, CancellationToken token)
    {
        var pokemons = await _dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(p => p.Types)
            .Where(p => p.PokemonAbility.Any(a => abilities.Contains(a.AbilityName)))
            .ToListAsync(token);

        var pokemonsByAbilities = pokemons.Select(pokemon => _mapper.Map<Pokemon>(pokemon)).ToList();
    
        return pokemonsByAbilities;
    }
    public async Task Create(AbilityDao ability, CancellationToken token)
    {
        _dbContext.Ability.Add(ability);
        await _dbContext.SaveChangesAsync(token);
    }

    // public async Task Update(string name, Ability ability, CancellationToken token)
    // {
    //     var existingAbility = await dbContext.Ability.FirstOrDefaultAsync(a => a.Name == name, token);
    //
    //     if (existingAbility != null)
    //     {
    //         existingAbility.Name = ability.Name;
    //         await dbContext.SaveChangesAsync(token);
    //     }
    //     else
    //     {
    //         throw new InvalidOperationException($"No se encontró ninguna habilidad con el nombre {name}");
    //     }
    // }

    public async Task Delete(string name, CancellationToken token)
    {
        AbilityDao ability = await _dbContext.Ability.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower(), token);
        if (ability != null)
        {
            _dbContext.Ability.Remove(ability);
            await _dbContext.SaveChangesAsync(token);
        }
        else
        {
            throw new InvalidOperationException($"{name} ability doesn't exists in database.");
        }
    }
}