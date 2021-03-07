using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public static class PokeApiServiceClient
    {
        public static async Task<HttpResponseMessage> GetBasicPokemonDetails (HttpClient httpClient, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pokemon/{name}");
            return await httpClient.SendAsync(request);
        }
    }
}
