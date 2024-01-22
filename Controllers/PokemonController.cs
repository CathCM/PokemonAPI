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
}

//     [HttpPost]
//     public async Task<ActionResult<PokemonDao>> CreatePokemon(PokemonDao pokemon)
//     {
//         dbContext.Pokemon.Add(pokemon);
//         await dbContext.SaveChangesAsync();
//
//         return CreatedAtAction("GetPokemonById", new { id = pokemon.Id }, pokemon);
//     }
//
//     [HttpGet("name")]
//     public ActionResult<List<string>> GetNames() => new List<string>();
//
//     [HttpGet("name/{name}")]
//     public ActionResult<Pokemon> GetPokemonByName(string name) => new Pokemon();
//
//     [HttpGet("{id}/ability")]
//     public ActionResult<List<PokemonAbility>> GetAbilities(int id) => new List<PokemonAbility>();
//
//     [HttpGet("{id}/stats")]
//     public ActionResult<List<PokemonStat>> GetStats(int id) => new List<PokemonStat>();
//
//     [HttpGet("{id}/type")]
//     public ActionResult<List<string>> GetTypes() => new List<string>();
//
//     //··········POST············
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