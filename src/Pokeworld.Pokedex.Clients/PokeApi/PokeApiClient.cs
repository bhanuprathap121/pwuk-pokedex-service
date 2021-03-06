using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;

namespace Pokeworld.Pokedex.Clients.PokeApi
{
    public class PokeApiClient : IPokeApiClient
    {
        private readonly IClient _client;
        private readonly IUrlProvider _urlProvider;
        public PokeApiClient(IClient client, IUrlProvider urlProvider)
        {
            _client = client;
            _urlProvider = urlProvider;
        }

        public async Task<PokemonResponse> GetPokemonDetailsAsync(string name) => await _client.GetAsync<PokemonResponse>(_urlProvider.GetPokemonUrl(name));

        public async Task<PokemonSpeciesResponse> GetPokemonSpeciesDetailsAsync(string speciesUrl) => await _client.GetAsync<PokemonSpeciesResponse>(speciesUrl);
    }

    public interface IPokeApiClient
    {
        Task<PokemonResponse> GetPokemonDetailsAsync(string name);
        Task<PokemonSpeciesResponse> GetPokemonSpeciesDetailsAsync(string speciesUrl);
    }
}
