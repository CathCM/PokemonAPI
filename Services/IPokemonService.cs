using PokemonAPI.Models;

namespace PokemonAPI.Services;

public interface IPokemonService
{
    //··········Create············
    // Task Create(PokemonDao pokemon, CancellationToken token);
    // Task AddAbility(int id, Pokemon pokemon, CancellationToken token);
    // Task AddType(int id, Pokemon pokemon, CancellationToken token);
    //
    // //··········Read············
    Task<List<Pokemon>> GetAll(CancellationToken token);
    Task<Pokemon> GetById(int id, CancellationToken token);
    // Task<List<string>> GetNames(string name, CancellationToken token);
    Task<Pokemon> GetByName(string name, CancellationToken token);
    // Task<List<PokemonAbility>> GetAbilities(int id, CancellationToken token);
    // Task<List<PokemonStat>> GetStats(int id, CancellationToken token);
    // Task<List<string>> GetType(int id, CancellationToken token);
    //
    // //··········Update············
    // Task Update(int id, Pokemon pokemon, CancellationToken token);
    // Task UpdateName(int id, Pokemon pokemon, CancellationToken token);
    // Task UpdateAbility(int id, Pokemon pokemon, CancellationToken token);
    // Task UpdateStats(int id, Pokemon pokemon, CancellationToken token);
    //
    // //··········Delete············
    // Task Delete(int id, CancellationToken token);
    // Task DeleteAbilities(int id, CancellationToken token);
    // Task DeleteAbility(int id, string ability, CancellationToken token);
    // Task DeleteType(int id, string type, CancellationToken token);
    //
}