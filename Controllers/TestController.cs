using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("/testcontroller")]
public class TestController : ControllerBase
{
    private readonly PokemonService pokemonService;

    public TestController(PokemonService pokemonService)
    {
        this.pokemonService = pokemonService;
    }
    // [HttpGet]
    // public async Task<ActionResult<List<Pokemon>>> GetAllPokemon(CancellationToken token) => await pokemonService.GetAll(token);
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Pokemon>> GetPokemonById(int id, CancellationToken token)
    // {
    //     var pokemon = await pokemonService.GetById(id, token);
    //
    //     if (pokemon == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return pokemon;
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult<PokemonDao>> CreatePokemon(PokemonDao pokemon)
    // {
    //     dbContext.Pokemon.Add(pokemon);
    //     await dbContext.SaveChangesAsync();
    //
    //     return CreatedAtAction("GetPokemonById", new { id = pokemon.Id }, pokemon);
    // }
    
}