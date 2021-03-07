using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.ServiceTests.Extensions
{
    public static class HttpResponseExtensions
    {
        private static readonly JsonSerializerOptions CamelCaseOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        public static async Task<T> DeserializeResponseContent<T>(this HttpResponseMessage httpResponseMessage)
        {
            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonContent, CamelCaseOptions);
        }
    }
}
