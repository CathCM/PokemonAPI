using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

[ApiController]
[Route("type")]
public class TypeController : ControllerBase
{
    private readonly TypeService typeService;
    public TypeController(TypeService typeService)
    {
        this.typeService = typeService;
    }
    [HttpGet]
    public async Task<ActionResult<List<string>>> GetPokemonsTypes(CancellationToken token) => await typeService.GetAll(token);

    [HttpGet("{type}")]
    public async Task<ActionResult<List<Pokemon>>> GetPokemonsByType(string type, CancellationToken token)
    {
        List<Pokemon> pokemons = await typeService.GetAllByTypes(type, token);

        return pokemons == null ? NoContent() : Ok(pokemons);
    }
    
    // [HttpPost]
    // public ActionResult Create([FromBody] Types createType) => Ok();
    //
    // [HttpDelete("{type}")]
    // public ActionResult Delete(string type) => Ok();

} 
// public async Task<List<string>> GetTypes(CancellationToken token)
// {
//     var pokemon = await GetAll(token);
//     List<string> types = pokemon.SelectMany(x => x.Types).Distinct().ToList();
//     return types;
// }