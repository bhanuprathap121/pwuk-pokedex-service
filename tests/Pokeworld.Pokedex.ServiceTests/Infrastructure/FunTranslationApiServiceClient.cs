using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public class FunTranslationApiServiceClient
    {
        public static async Task<HttpResponseMessage> GetTranslatedPokemonResponse(HttpClient httpClient, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon/translated/{name}");
            return await httpClient.SendAsync(request);
        }
    }
}
