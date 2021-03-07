using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.Clients
{
    [ExcludeFromCodeCoverage]
    public class Client : IClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public Client()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                //Encoder = JavaScriptEncoder.Create(Encoding.UTF8),
                WriteIndented = true
            };
        }

        public async Task<T> GetAsync<T>(string path)
        {
            return await SendAsync<T>(HttpMethod.Get, path);
        }

        private async Task<T> SendAsync<T>(HttpMethod httpMethod, string path)
        {
            using var request = new HttpRequestMessage(httpMethod, path);
            using var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new ServiceErrorException(errorMessage, response.StatusCode);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _serializerOptions );
        }
    }
}