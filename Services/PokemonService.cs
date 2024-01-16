using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Services;

public class PokemonService
{
    private readonly IMapper mapper;
    private readonly PokemonDb dbContext;

    public PokemonService(IMapper mapper, PokemonDb dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;

    }

    // public List<PokemonDao> GetAllPokemonDao() => dbContext.Pokemon.Include(p => p.PokemonAbility)
    //     .Include(pokemon => pokemon.Types).ToList();
    public Pokemon MappingToPokemon(PokemonDao pokemon) => mapper.Map<Pokemon>(pokemon);
    
    public List<Pokemon> GetAllPokemon()
    {
        List<PokemonDao> pokemonDao = dbContext.Pokemon.Include(p => p.PokemonAbility)
            .Include(pokemon => pokemon.Types).ToList();

        List<Pokemon> pokemonList = pokemonDao.Select(pokemon => MappingToPokemon(pokemon)).ToList();
        return pokemonList;
        
    }
    
    
    
    
}