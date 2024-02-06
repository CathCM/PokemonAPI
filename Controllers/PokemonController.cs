using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("pokemon")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;
    // private IActionResult CheckResponseNoContent<T>(List<T> response)
    // {
    //     return (response == null || response.Count == 0) ? (IActionResult) NoContent() : Ok(response);
    // }
    // private IActionResult CheckResponseNotFound<T>(List<T> response)
    // {
    //     return (response == null || response.Count == 0) ? (IActionResult) NotFound() : Ok(response);
    // }

    public PokemonController(PokemonService _pokemonService)
    {
        this._pokemonService = _pokemonService;
    }

    //··········GET············
    [HttpGet]
    public async Task<ActionResult<List<Pokemon>>> GetAllPokemon(CancellationToken token) =>
        await _pokemonService.GetAll(token);

    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemonById(int id, CancellationToken token)
    {
        var pokemon = await _pokemonService.GetById(id, token);
        return (pokemon == null) ? NotFound() : Ok(pokemon);
    }

    [HttpGet("name")]
    public async Task<ActionResult<List<string>>> GetPokemonNames(CancellationToken token)
    {
        List<string> pokemonNames = await _pokemonService.GetNames(token);
        return (pokemonNames == null || pokemonNames.Count == 0) ? NoContent() : Ok(pokemonNames);
    }


    [HttpGet("name/{name}")]
    public async Task<ActionResult<Pokemon>> GetPokemonByName(string name, CancellationToken token)
    {
        var pokemon = await _pokemonService.GetByName(name, token);

        return (pokemon == null) ? NoContent() : Ok(pokemon);
    }

    [HttpGet("{id}/ability")]
    public async Task<ActionResult<List<PokemonAbility>>> GetPokemonAbilities(int id, CancellationToken token)
    {
        List<PokemonAbility> abilities = await _pokemonService.GetAbilities(id, token);
        return (abilities == null || abilities.Count == 0) ? NoContent() : Ok(abilities);
    }


    [HttpGet("{id}/stats")]
    public async Task<ActionResult<List<PokemonStat>>> GetPokemonStats(int id, CancellationToken token)
    {
        List<PokemonStat> stats = await _pokemonService.GetStats(id, token);
        return (stats == null || stats.Count == 0) ? NoContent() : Ok(stats);
    }

    [HttpGet("{id}/type")]
    public async Task<ActionResult<List<string>>> GetPokemonType(int id, CancellationToken token)
    {
        List<string> types = await _pokemonService.GetType(id, token);
        return (types == null || types.Count == 0) ? NoContent() : Ok(types);
    }

//     //··········POST············
    [HttpPost]
    public async Task<ActionResult> CreatePokemon([FromBody] PokemonDao pokemon, CancellationToken token)
    {
        await _pokemonService.Create(pokemon, token);
        return Ok();
    }

    [HttpPost("{id}/ability")]
    public async Task<ActionResult> AddAbility(int id, PokemonAbility ability, CancellationToken token)
    {
        await _pokemonService.AddAbility(id, ability, token);
        return Ok();
    }

//
    [HttpPost("{id}/type")]
    public async Task<ActionResult> AddType(int id, TypeDao newTypePokemon, CancellationToken token)
    {
        await _pokemonService.AddType(id, newTypePokemon, token);
        return Ok();
    }


//
//     //··········PUT············
//
    // [HttpPut("{id:int}")]
    // public async Task<ActionResult> Update(int id, string property, [FromBody] PokemonDao pokemonUpdate, CancellationToken token)
    // {
    //     await _pokemonService.Update(id, property, value, pokemonUpdate, token);
    //     return Ok();
    // }
//
    [HttpPut("{id}/name")]
    public async Task<ActionResult> UpdateName(int id, [FromBody] PokemonDao pokemon, CancellationToken token)
    {
        await _pokemonService.UpdateName(id, pokemon, token);
        return Ok();
    }

//
//     [HttpPut("{id}/ability/{ability}/{isHidden}")]
//     public ActionResult UpdateAbility(int id, [FromBody] Pokemon pokemonUpdateAbility) => Ok();
//
    [HttpPut("{id:int}/stats/{statName}/{baseStat:int}")]
    public async Task<ActionResult> UpdateStat(int id, string statName, int baseStat, [FromBody] PokemonDao pokemon,
        CancellationToken token)
    {
        await _pokemonService.UpdateStats(id, statName, baseStat, pokemon, token);
        return Ok();
    }

//
//
//     //··········DELETE············
//
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken token)
    {
        await _pokemonService.Delete(id, token);
        return Ok();
    }
//
    [HttpDelete("{id}/ability")]
    public async Task<ActionResult> DeleteAllAbilities(int id, CancellationToken token)
    {
        await _pokemonService.DeleteAbilities(id, token);
        return Ok();
    }

    [HttpDelete("{id}/ability/{ability}")]
    public async Task<ActionResult> DeleteAbility(int id, string ability, CancellationToken token)
    {
        await _pokemonService.DeleteAbility(id, ability, token);
        return Ok();
    }

    [HttpDelete("{id}/type/{type}")]
    public async Task<ActionResult> DeleteType(int id, string type, CancellationToken token)
    {
        await _pokemonService.DeleteType(id, type, token);
        return Ok();
    }




// {
// "id": 0,
// "name": "string",
// "pokemonAbility": [
// {
//     "abilityName": "string",
//     "isHidden": true
// }
// ],
// "hp": 0,
// "defense": 0,
// "attack": 0,
// "specialAttack": 0,
// "specialDefense": 0,
// "speed": 0,
// "types": [
// {
//     "name": "string",
//    }
// ]
}