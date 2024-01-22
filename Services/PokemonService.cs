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

    public async Task<Pokemon> GetById(int id, CancellationToken token)
    {
        PokemonDao? pokemon = await dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Id == id, token);
        Pokemon mappedPokemon = MappingToPokemon(pokemon);
        return mappedPokemon;
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
}