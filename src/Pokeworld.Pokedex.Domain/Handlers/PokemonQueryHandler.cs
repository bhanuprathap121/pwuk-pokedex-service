using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Extensions;

namespace Pokeworld.Pokedex.Domain.Handlers
{
    public class PokemonQueryHandler : IPokemonQueryHandler
    {
        private readonly IPokeApiQueries _pokeApiQueries;

        public PokemonQueryHandler(IPokeApiQueries pokeApiQueries)
        {
            _pokeApiQueries = pokeApiQueries;
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

        public async Task<TranslatedPokemonResponse> GetTranslatedAsync(string name)
        {
            return new TranslatedPokemonResponse {Name = name};
        }
    }
}
