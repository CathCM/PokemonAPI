using PokemonAPI.Models;

namespace PokemonAPI.Services;

public interface IAbilityService
{
    
    //··········Create············
    // Task Create(AbilityDao ability, CancellationToken token);
    // //··········Read············
    Task<List<string>> GetAll(CancellationToken token);
    // Task<List<Pokemon>> GetAllByAbility(List<string> abilities, CancellationToken token);
    //
    // //··········Update············
    // Task Update(string name, Ability ability, CancellationToken token);
    // //··········Delete············
    // Task Delete(string name, CancellationToken token);
}