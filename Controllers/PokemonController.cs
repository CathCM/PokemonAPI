using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("pokemon")]

public class PokemonController : ControllerBase
{
    private readonly PokemonService pokemonService;
    private readonly PokemonDb dbContext;

    public PokemonController(PokemonDb dbContext, PokemonService pokemonService)
    {
        this.pokemonService = pokemonService;
        this.dbContext = dbContext;
    }

    //··········GET············
    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> GetAllPokemon(CancellationToken token) =>
        await pokemonService.GetAll(token);

    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemonById(int id, CancellationToken token)
    {
        var pokemon = await pokemonService.GetById(id, token);

        if (pokemon == null)
        {
            return NotFound();
        }

        return Ok(pokemon);
    }

    [HttpGet("name")]
    public async Task<ActionResult<List<string>>> GetNames(CancellationToken token)
    {
        List<string> pokemonNames = await pokemonService.GetNames(token);
        return (pokemonNames == null || pokemonNames.Count == 0) ? NoContent() : Ok(pokemonNames);
    }


    [HttpGet("name/{name}")]
    public async Task<ActionResult<Pokemon>> GetPokemonByName(string name, CancellationToken token)
    {
        var pokemon = await pokemonService.GetByName(name, token);

        return (pokemon == null) ? NoContent() : Ok(pokemon);
      
    }

    [HttpGet("{id}/ability")]
    public async Task<ActionResult<List<PokemonAbility>>> GetAbilities(int id, CancellationToken token)
    {
        List<PokemonAbility> abilities = await pokemonService.GetAbilities(id, token);
        return (abilities == null || abilities.Count == 0) ? NoContent() : Ok(abilities);
    }


    [HttpGet("{id}/stats")]
    public async Task<ActionResult<List<PokemonStat>>> GetStats(int id, CancellationToken token)
    {
        List<PokemonStat> stats = await pokemonService.GetStats(id, token);
        return (stats == null || stats.Count == 0) ? NoContent() : Ok(stats);
    }

} //
//     [HttpGet("{id}/type")]
//     public ActionResult<List<string>> GetTypes() => new List<string>();
//
//     //··········POST············
//
//     [HttpPost]
//     public async Task<ActionResult<PokemonDao>> CreatePokemon(PokemonDao pokemon)
//     {
//         dbContext.Pokemon.Add(pokemon);
//         await dbContext.SaveChangesAsync();
//
//         return CreatedAtAction("GetPokemonById", new { id = pokemon.Id }, pokemon);
//     }
//
     
//
//     [HttpPost]
//     public ActionResult Create([FromBody] Pokemon pokemonCreate) => Ok();
//
//     [HttpPost("{id}/ability")]
//     public ActionResult AddAbility(int id, [FromBody] Pokemon pokemonCreateAbility) => Ok();
//
//     [HttpPost("{id}/type")]
//     public ActionResult AddType(int id, [FromBody] Types pokemonCreateType) => Ok();
//
//     //··········PUT············
//
//     [HttpPut("{id}")]
//     public ActionResult Update(int id, [FromBody] Pokemon pokemonUpdate) => Ok();
//
//     [HttpPut("{id}/name")]
//     public ActionResult UpdateName(int id, [FromBody] Pokemon pokemonUpdateName) => Ok();
//
//     [HttpPut("{id}/ability/{ability}/{isHidden}")]
//     public ActionResult UpdateAbility(int id, [FromBody] Pokemon pokemonUpdateAbility) => Ok();
//
//     [HttpPut("{id}/stats/{statName}/{baseStat}")]
//     public ActionResult UpdateStat(int id, [FromBody] Pokemon pokemonUpdateStat) => Ok();
//
//
//     //··········DELETE············
//
//     [HttpDelete("{id}")]
//     public ActionResult Delete(int id) => Ok();
//
//     [HttpDelete("{id}/ability")]
//     public ActionResult DeleteAllAbilities(int id) => Ok();
//
//     [HttpDelete("{id}/ability/{ability}")]
//     public ActionResult DeleteAbility(int id, string ability) => Ok();
//
//     [HttpDelete("{id}/type/{type}")]
//     public ActionResult DeleteType(int id, string type) => Ok();
//
//
// }