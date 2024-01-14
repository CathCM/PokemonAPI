using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("/testcontroller")]
public class TestController : ControllerBase
{
    private readonly PokemonDb dbContext;

    public TestController(PokemonDb dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<List<PokemonDao>> GetAllPokemonDao() => dbContext.Pokemon.Include(pokemon => pokemon.Abilities)
        .Include(pokemon => pokemon.Types).ToList();

    [HttpGet("{id}")]
    public ActionResult<PokemonDao> GetPokemonByIdDao(int id) => dbContext.Pokemon.FirstOrDefault(x => x.Id == id);

}