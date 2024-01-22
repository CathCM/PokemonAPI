using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class AbilityService : IAbilityService
{
    private readonly IMapper mapper;
    private readonly PokemonDb dbContext;

    public AbilityService(IMapper mapper, PokemonDb dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    private Ability MappingToAbility(AbilityDao ability) => mapper.Map<Ability>(ability);
    private AbilityDao MappingToAbility(Ability ability) => mapper.Map<AbilityDao>(ability);

    public async Task<List<string>> GetAll(CancellationToken token)
    {
        var abilities = await dbContext.Ability.Select(x => x.Name).ToListAsync(token);
        return abilities;
    }
    
}