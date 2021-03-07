using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;

namespace Pokeworld.Pokedex.Clients.PokeApi.Queries
{
    public class PokeApiQueries : IPokeApiQueries
    {
        private readonly IPokeApiClient _pokeApiClient;
        public PokeApiQueries(IPokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }
        public async Task<PokemonResponse> GetPokemonResponse(string name)
        {
            return await _pokeApiClient.GetPokemonDetailsAsync(name);
        }

        public async Task<PokemonSpeciesResponse> GetPokemonSpeciesDetails(string url)
        {
            return await _pokeApiClient.GetPokemonSpeciesDetailsAsync(url);
        }
    }
}