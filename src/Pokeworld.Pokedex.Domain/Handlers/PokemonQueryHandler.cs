using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Queries;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Extensions;

namespace Pokeworld.Pokedex.Domain.Handlers
{
    public class PokemonQueryHandler : IPokemonQueryHandler
    {
        private readonly IPokeApiQueries _pokeApiQueries;
        private readonly IFunTranslationsApiQueries _funTranslationsApiQueries;
        private readonly ILogger<PokemonQueryHandler> _logger;

        public PokemonQueryHandler(IPokeApiQueries pokeApiQueries, IFunTranslationsApiQueries funTranslationsApiQueries, ILogger<PokemonQueryHandler> logger)
        {
            _pokeApiQueries = pokeApiQueries;
            _funTranslationsApiQueries = funTranslationsApiQueries;
            _logger = logger;
        }
        public async Task<BasicPokemonResponse> GetAsync(string name)
        {
            try
            {
                var pokemonDetails = await _pokeApiQueries.GetPokemonResponse(name);
                var pokemonSpeciesDetails = await _pokeApiQueries.GetPokemonSpeciesDetails(pokemonDetails.Species.Url);
                return pokemonDetails.ToBasicPokemonResponse(pokemonSpeciesDetails);
            }
            catch (ServiceErrorException ex)
            {
               throw new PokemonNotExistException(ex.Message);
            }
        }

        public async Task<BasicPokemonResponse> GetTranslatedAsync(string name)
        {
            var pokemonDetails = await GetAsync(name);

            try
            {
                var translationResponse = await _funTranslationsApiQueries.GetTranslation(pokemonDetails);

                pokemonDetails.Description = translationResponse?.Contents?.Translated;
            }
            catch (ServiceErrorException ex)
            {
                _logger.LogError("Error in GetTranslatedAsync", ex.Message, ex.StatusCode);
            }

            return pokemonDetails;
        }
    }
}
