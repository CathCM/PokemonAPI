using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;

[ApiController]
[Route("type")]
public class TypeController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<string>> GetTypes() => new List<string>();

    [HttpGet("{type}")]
    public ActionResult<List<Pokemon>> GetPokemonsByType(string type) => new List<Pokemon>();

    [HttpPost]
    public ActionResult Create([FromBody] Types createType) => Ok();

    [HttpDelete("{type}")]
    public ActionResult Delete(string type) => Ok();

}