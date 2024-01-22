using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class AbilityService : IAbilityService
{
    private readonly IMapper mapper;
    private readonly PokemonDb dbContext;
    private readonly PokemonService pokemonService;

    public AbilityService(IMapper mapper, PokemonDb dbContext, PokemonService pokemonService)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
        this.pokemonService = pokemonService;
    }

    private Ability MappingToAbility(AbilityDao ability) => mapper.Map<Ability>(ability);
    private AbilityDao MappingToAbility(Ability ability) => mapper.Map<AbilityDao>(ability);

    public async Task<List<string>> GetAll(CancellationToken token)
    {
        var abilities = await dbContext.Ability.Select(x => x.Name).ToListAsync(token);
        return abilities;
    }

    public async Task<List<Pokemon>> GetAllByAbility(List<string> abilities, CancellationToken token)
    {
        var pokemons = await dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(p => p.Types)
            .Where(p => p.PokemonAbility.Any(a => abilities.Contains(a.AbilityName)))
            .ToListAsync(token);

        var pokemonsByAbilities = pokemons.Select(pokemon => mapper.Map<Pokemon>(pokemon)).ToList();
    
        return pokemonsByAbilities;
    }
    
    
}