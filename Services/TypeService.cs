using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using AutoMapper;

namespace PokemonAPI.Services;

public class TypeService:ITypeService
{
    private readonly PokemonDb _dbContext;
    private readonly IMapper _mapper;
    // private Types MapToType(TypeDao type) => mapper.Map<Types>(type);
    private TypeDao MapToTypeDao(Types type) => _mapper.Map<TypeDao>(type);
    public TypeService(PokemonDb _dbContext, IMapper _mapper)
    {
        this._dbContext = _dbContext;
        this._mapper = _mapper;
    }
    public async Task<List<string>> GetAll(CancellationToken token)
    {
        List<TypeDao> typesDao = await _dbContext.Types.ToListAsync(token);
        List<string> types = typesDao.Select(x => x.Name).ToList();
        return types;
    }
    public async Task<List<Pokemon>> GetAllByTypes(string type, CancellationToken token)
    {
        List<PokemonDao> pokemonsDao = await _dbContext.Types
            .Where(t => t.Name.ToLower() == type.ToLower())
            .SelectMany(t => t.Pokemons) 
            .Include(p => p.PokemonAbility)
            .Include(t => t.Types)
            .ToListAsync(token);
    
        List<Pokemon> pokemons = pokemonsDao.Select(p => _mapper.Map<Pokemon>(p)).ToList();
        return pokemons;
    }
    public async Task Create(TypeDao type, CancellationToken token)
    {
        var existingType = _dbContext.Types.FirstOrDefault(x => x.Name.ToLower() == type.Name.ToLower());

        if (existingType == null)
        {
            _dbContext.Types.Add(type);
            await _dbContext.SaveChangesAsync(token);
        }
    }
    public async Task Delete(string name, CancellationToken token)
    {
        TypeDao type = await _dbContext.Types.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        if (type is not null)
        {
            _dbContext.Types.Remove(type);
            await _dbContext.SaveChangesAsync(token);
        }
        else
        {
            throw new InvalidOperationException($"{type} type doesn't exists in database");
        }
    }
}