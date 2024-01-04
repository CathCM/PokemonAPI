using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
namespace PokemonAPI.Controllers;
[ApiController]
[Route("ability")]

public class AbilityController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<string>> GetAllAbilities() => new List<string>();

    [HttpGet("ability/{abilities}")]
    public ActionResult<List<Pokemon>> GetPokemonsByAbility([FromRoute] List<string> abilities) => new List<Pokemon>();
    // GET /ability/{ab1}/{ab2}

    [HttpPost]
    public ActionResult Create([FromBody] Ability ability) => Ok();

    [HttpDelete("{ability}")]
    public ActionResult Delete(string ability) => Ok();
}
