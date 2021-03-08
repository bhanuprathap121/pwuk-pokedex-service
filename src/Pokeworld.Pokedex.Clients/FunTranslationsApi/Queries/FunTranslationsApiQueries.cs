using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Contracts.Api.Responses;

namespace Pokeworld.Pokedex.Clients.FunTranslationsApi.Queries
{
    public class FunTranslationsApiQueries : IFunTranslationsApiQueries
    {
        private readonly IFunTranslationsApiClient _apiClient;
        public FunTranslationsApiQueries(IFunTranslationsApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TranslationResponse> GetTranslation(BasicPokemonResponse pokemonDetails)
        {
            TranslationResponse translationResponse;

            if (pokemonDetails.Habitat.ToLowerInvariant() == "cave" || pokemonDetails.IsLegendary)
            {
                translationResponse = await _apiClient.GetYodaTranslatedText(pokemonDetails.Description);
            }
            else
            {
                translationResponse = await _apiClient.GetShakespeareTranslatedText(pokemonDetails.Description);
            }

            return translationResponse;
        }
    }
}