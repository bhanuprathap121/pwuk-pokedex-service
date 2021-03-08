using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;

namespace Pokeworld.Pokedex.Clients.FunTranslationsApi
{
    public interface IFunTranslationsApiClient
    {
        Task<TranslationResponse> GetYodaTranslatedText(string text);
        Task<TranslationResponse> GetShakespeareTranslatedText(string text);
    }
}