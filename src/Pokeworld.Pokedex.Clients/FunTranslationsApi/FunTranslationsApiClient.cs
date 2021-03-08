using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;

namespace Pokeworld.Pokedex.Clients.FunTranslationsApi
{
    public class FunTranslationsApiClient : IFunTranslationsApiClient
    {
        private readonly IClient _client;
        private readonly IUrlProvider _urlProvider;
        public FunTranslationsApiClient(IClient client, IUrlProvider urlProvider)
        {
            _client = client;
            _urlProvider = urlProvider;
        }


        public async Task<TranslationResponse> GetYodaTranslatedText(string text)
        {
            return await _client.GetAsync<TranslationResponse>(_urlProvider.GetYodaTranslationUrl(text));
        }

        public async Task<TranslationResponse> GetShakespeareTranslatedText(string text)
        {
            return await _client.GetAsync<TranslationResponse>(_urlProvider.GetShakespeareTranslationUrl(text));
        }
    }
}
