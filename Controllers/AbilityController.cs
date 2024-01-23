using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;
[ApiController]
[Route("ability")]

public class AbilityController : ControllerBase
{
    private readonly AbilityService abilityService;

    public AbilityController(AbilityService abilityService)
    {
        this.abilityService = abilityService;
    }
    [HttpGet]
    public async Task<ActionResult<List<string>>> GetAllAbilities(CancellationToken token) => await abilityService.GetAll(token);

    [HttpGet("ability/{abilities}")]
    public async Task<ActionResult<List<Pokemon>>> GetPokemonsByAbility([FromRoute] List<string> abilities, CancellationToken token)
    {
        List<Pokemon> pokemons = await abilityService.GetAllByAbility(abilities, token);
        return (pokemons == null || pokemons.Count == 0) ? NoContent() : Ok(pokemons);
    }
    
    // GET /ability/{ab1}/{ab2}

    [HttpPost]
    public async Task<ActionResult> CreateAbility([FromBody] Ability ability, CancellationToken token)
    {
        await abilityService.Create(ability, token); 
        return Ok(ability);
    }
    
    [HttpDelete("{ability}")]
    public async Task<ActionResult> Delete(string ability, CancellationToken token)
    {
        await abilityService.Delete(ability, token);
        return Ok();
    }
}
