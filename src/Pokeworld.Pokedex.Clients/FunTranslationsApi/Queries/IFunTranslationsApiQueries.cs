using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.Clients.FunTranslationsApi.Queries
{
    public interface IFunTranslationsApiQueries
    {
        public Task<TranslationResponse> GetTranslation(BasicPokemonResponse pokemonDetails);
    }
}
