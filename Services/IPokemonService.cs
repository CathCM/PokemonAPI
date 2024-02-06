using PokemonAPI.Models;

namespace PokemonAPI.Services;

public interface IPokemonService
{
    //··········Create············
    Task Create(PokemonDao pokemon, CancellationToken token);
    Task AddAbility(int id, PokemonAbility newAbility, CancellationToken token);
    Task AddType(int id, TypeDao pokemon, CancellationToken token);
    //
    // //··········Read············
    Task<List<Pokemon>> GetAll(CancellationToken token);
    Task<Pokemon> GetById(int id, CancellationToken token);
    Task<List<string>> GetNames(CancellationToken token);
    Task<Pokemon> GetByName(string name, CancellationToken token);
    Task<List<PokemonAbility>> GetAbilities(int id, CancellationToken token);
    Task<List<PokemonStat>> GetStats(int id, CancellationToken token);
    Task<List<string>> GetType(int id, CancellationToken token);
    //
    // //··········Update············
    // Task Update<T>(int id, string property, T value, PokemonDao pokemon, CancellationToken token);
    Task UpdateName(int id, PokemonDao pokemon, CancellationToken token);
    Task UpdateStats(int id, string statName, int baseStat, PokemonDao pokemon, CancellationToken token);

    // //··········Delete············
    Task Delete(int id, CancellationToken token);
    Task DeleteAbilities(int id, CancellationToken token);
    Task DeleteAbility(int id, string ability, CancellationToken token);
    Task DeleteType(int id, string type, CancellationToken token);
}