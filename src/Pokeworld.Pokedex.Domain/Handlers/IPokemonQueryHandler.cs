using System.Threading.Tasks;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.Domain.Handlers
{
    public interface IPokemonQueryHandler
    {
        Task<BasicPokemonResponse> GetAsync(string name);
        Task<BasicPokemonResponse> GetTranslatedAsync(string name);
    }
}