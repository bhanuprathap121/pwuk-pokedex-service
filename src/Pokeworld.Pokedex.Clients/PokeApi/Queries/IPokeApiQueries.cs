using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;

namespace Pokeworld.Pokedex.Clients.PokeApi.Queries
{
    public interface IPokeApiQueries
    {
        Task<PokemonResponse> GetPokemonResponse(string name);
        Task<PokemonSpeciesResponse> GetPokemonSpeciesDetails(string url);
    }
}
