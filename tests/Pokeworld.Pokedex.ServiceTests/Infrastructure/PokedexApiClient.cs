using System.Net.Http;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public static class PokedexApiClient
    {
        public static async Task<HttpResponseMessage> GetBasicPokemonDetails(HttpClient httpClient, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon/{name}");
            return await httpClient.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> GetTranslatedPokemonResponse(HttpClient httpClient, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon/translated/{name}");
            return await httpClient.SendAsync(request);
        }
    }
}