using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("/testcontroller")]
public class TestController : ControllerBase
{
    private readonly PokemonDb dbContext;
    private readonly PokemonService pokemonService;

    public TestController(PokemonDb dbContext, PokemonService pokemonService)
    {
        this.dbContext = dbContext;
        this.pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> GetAllPokemon(CancellationToken token) => await pokemonService.GetAll(token);

    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemonById(int id, CancellationToken token)
    {
        var pokemon = await dbContext.Pokemon
            .Include(p => p.Types)
            .Include(p => p.PokemonAbility)
            .FirstOrDefaultAsync(x => x.Id == id, token);

        if (pokemon == null)
        {
            return NotFound();
        }

        Pokemon mappedPokemon = pokemonService.MappingToPokemon(pokemon);

        return mappedPokemon;
    }
    
    [HttpPost]
    public async Task<ActionResult<PokemonDao>> CreatePokemon(PokemonDao pokemon)
    {
        dbContext.Pokemon.Add(pokemon);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction("GetPokemonById", new { id = pokemon.Id }, pokemon);
    }
}