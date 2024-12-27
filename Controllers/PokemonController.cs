using Microsoft.AspNetCore.Mvc;
using PokemonTeamManager.Models;
using PokemonTeamManager.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeamManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokeApiService _pokeApiService;
        private readonly AppDbContext _context;

        public PokemonController(PokeApiService pokeApiService, AppDbContext context)
        {
            _pokeApiService = pokeApiService;
            _context = context;
        }

        [HttpGet("{nameOrId}")]
        public async Task<IActionResult> GetPokemon(string nameOrId)
        {
            var pokemon = await _pokeApiService.GetPokemonAsync(nameOrId);
            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        [HttpPost("team/create")]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();

            return Ok(team);
        }

        [HttpGet("team")]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.Include(t => t.Pokemons);
            return Ok(teams);
        }

        [HttpPost("team/{teamId}/pokemon")]
        public async Task<IActionResult> AddPokemonToTeam(int teamId, [FromBody] string pokemonName)
        {
            var team = _context.Teams.Include(t => t.Pokemons).SingleOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound("Team not found");
            }

            if (team.Pokemons.Count >= 6)
            {
                return BadRequest("Team already has the maximum number of 6 Pokémon.");
            }

            var pokemonFromApi = await _pokeApiService.GetPokemonAsync(pokemonName);
            if (pokemonFromApi == null)
            {
                return NotFound("Pokemon not found");
            }

            var newPokemon = new Pokemon
            {
                Name = pokemonFromApi.Name,
                Type = pokemonFromApi.Types?[0].Type?.Name,
                TeamId = team.Id,
                Team = team
            };

            _context.Pokemons.Add(newPokemon);
            _context.SaveChanges();

            return Ok(newPokemon);
        }

        [HttpPost("save/{nameOrId}")]
        public async Task<IActionResult> SavePokemon(string nameOrId)
        {
            var pokemonFromApi = await _pokeApiService.GetPokemonAsync(nameOrId);
            if (pokemonFromApi == null)
            {
                return NotFound("Pokemon not found");
            }

            var newPokemon = new Pokemon
            {
                Name = pokemonFromApi.Name,
                Type = pokemonFromApi.Types?[0].Type?.Name
            };

            _context.Pokemons.Add(newPokemon);
            _context.SaveChanges();

            return Ok(newPokemon);
        }

        [HttpGet("available")]
        public IActionResult GetAllPokemons()
        {
            var pokemons = _context.Pokemons.ToList();
            return Ok(pokemons);
        }

        [HttpGet("team/{teamId}/pokemons")]
        public IActionResult GetPokemonsByTeam(int teamId)
        {
            var team = _context.Teams.Include(t => t.Pokemons).SingleOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound("Team not found");
            }

            var pokemons = team.Pokemons.Select(p => new
            {
                p.Id,
                p.Name,
                p.Type
            }).ToList();

            return Ok(pokemons);
        }

        [HttpDelete("team/{teamId}/pokemon/{order}")]
        public IActionResult RemovePokemonFromTeamByOrder(int teamId, int order)
        {
            var team = _context.Teams.Include(t => t.Pokemons).SingleOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound("Team not found");
            }

            var pokemonsList = team.Pokemons.ToList();

            // Ensure the order is valid
            if (order < 1 || order > pokemonsList.Count)
            {
                return BadRequest("Invalid order number.");
            }

            // Get the Pokémon based on the specified order
            var pokemonToRemove = pokemonsList[order - 1];

            // Remove the Pokémon from the team by setting its TeamId to null
            pokemonToRemove.TeamId = null;
            _context.SaveChanges();

            return Ok($"Pokemon '{pokemonToRemove.Name}' removed from the team.");
        }
    
    }
}