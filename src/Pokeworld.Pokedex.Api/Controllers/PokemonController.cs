using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Handlers;

namespace Pokeworld.Pokedex.Api.Controllers
{
    [Route("pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonQueryHandler _pokemonQueryHandler;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger, IPokemonQueryHandler pokemonQueryHandler)
        {
            _logger = logger;
            _pokemonQueryHandler = pokemonQueryHandler;
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicPokemonResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasicPokemonResponse>> GetAsync(string name)
        {
            try
            {
                var response = await _pokemonQueryHandler.GetAsync(name);

                return Ok(response);
            }
            catch (PokemonNotExistException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
