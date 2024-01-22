using AutoMapper;
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
    public Pokemon MappingToPokemon(PokemonDao pokemon) => mapper.Map<Pokemon>(pokemon);
    
    public async Task<List<Pokemon>> GetAll(CancellationToken token)
    {
        List<PokemonDao> pokemonDao = await dbContext.Pokemon
            .Include(p => p.PokemonAbility)
            .Include(pokemon => pokemon.Types)
            .ToListAsync(token);

        List<Pokemon> pokemonList = pokemonDao.Select(pokemon => MappingToPokemon(pokemon)).ToList();
        return pokemonList;
    }
}