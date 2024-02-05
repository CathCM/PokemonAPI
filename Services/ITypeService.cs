using PokemonAPI.Models;

namespace PokemonAPI.Services;

public interface ITypeService
{
    Task<List<string>> GetAll(CancellationToken token);
    Task<List<Pokemon>> GetAllByTypes(string type, CancellationToken token);
     Task Create(TypeDao name, CancellationToken token);
     Task Delete(string type, CancellationToken token);
}