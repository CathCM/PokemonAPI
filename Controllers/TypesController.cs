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
    
    [HttpPost]
    public async Task<ActionResult> CreateType(TypeDao type, CancellationToken token)
    {
        await typeService.Create(type, token);
        return Ok();
    }
    [HttpDelete("{type}")]
    public async Task<ActionResult> DeleteType(string type, CancellationToken token)
    {
        await typeService.Delete(type, token);
        return Ok();
    }
} 
