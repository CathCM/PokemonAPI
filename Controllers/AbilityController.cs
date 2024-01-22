using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;
[ApiController]
[Route("ability")]

public class AbilityController : ControllerBase
{
    private readonly PokemonDb dbContext;
    private readonly AbilityService abilityService;

    public AbilityController(PokemonDb dbContext, AbilityService abilityService)
    {
        this.dbContext = dbContext;
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
    public ActionResult Create([FromBody] Ability ability) => Ok();

    [HttpDelete("{ability}")]
    public ActionResult Delete(string ability) => Ok();
}
