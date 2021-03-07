using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;

namespace Pokeworld.Pokedex.Clients.PokeApi
{
    public interface IPokeApiClient
    {
        Task<PokemonResponse> GetPokemonDetailsAsync(string name);
        Task<PokemonSpeciesResponse> GetPokemonSpeciesDetailsAsync(string speciesUrl);
    }
}