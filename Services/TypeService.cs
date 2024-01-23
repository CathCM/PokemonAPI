using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using AutoMapper;

namespace PokemonAPI.Services;

public class TypeService:ITypeService
{
    private readonly PokemonDb dbContext;
    private readonly IMapper mapper;

    // private Types MapToType(TypeDao type) => mapper.Map<Types>(type);
    private Pokemon MapToPokemon(PokemonDao pokemon) => mapper.Map<Pokemon>(pokemon);

    public TypeService(PokemonDb dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<string>> GetAll(CancellationToken token)
    {
        List<TypeDao> typesDao = await dbContext.Types.ToListAsync(token);
        List<string> types = typesDao.Select(x => x.Name).ToList();
        return types;
    }
    public async Task<List<Pokemon>> GetAllByTypes(string type, CancellationToken token)
    {
        List<PokemonDao> pokemonsDao = await dbContext.Types
            .Where(t => t.Name.ToLower() == type.ToLower())
            .SelectMany(t => t.Pokemons) 
            .Include(p => p.PokemonAbility)
            .Include(t => t.Types)
            .ToListAsync(token);
    
        List<Pokemon> pokemons = pokemonsDao.Select(p => mapper.Map<Pokemon>(p)).ToList();
        return pokemons;
    }
    // public async Task<List<Pokemon>> GetAllByTypes(string type, CancellationToken token)
    // {
    //     var pokemonsDao = await dbContext.Pokemon
    //         .Include(p => p.PokemonAbility)
    //         .Include(p => p.Types)
    //         .Where(p => p.Types.Any(t => t.Name.ToLower() == type.ToLower())).ToListAsync(token);
    //     var pokemons = pokemonsDao.Select(p => mapper.Map<Pokemon>(p)).ToList();
    //     return pokemons;
    // }    
}