using System.Net;
using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.PokeApi;
using Pokeworld.Pokedex.IntegrationTests.Infrastructure;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.IntegrationTests
{
    public class PokeApiIntegrationTests
    {
        private readonly IPokeApiClient _sutApiClient;
        private readonly IClient _client = new Client();


        public PokeApiIntegrationTests()
        {
            var settings = new InitialiseSettings();
            IUrlProvider urlProvider = new UrlProvider(settings.GetSettings<ServiceUrls>(ServiceUrls.Services));
            _sutApiClient = new PokeApiClient(_client, urlProvider);
        }

        [Fact]
        public async Task GetPokemonDetailsAsync_Should_Return_Successful_Response()
        {
            var response = await _sutApiClient.GetPokemonDetailsAsync("ditto");
            response.ShouldNotBeNull();
        }

        [Fact]
        public void GetPokemonDetailsAsync_Should_Throw_When_NotFound()
        {
            _sutApiClient.GetPokemonDetailsAsync("randomPokemon").ShouldThrow<ServiceErrorException>().StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPokemonSpeciesDetailsAsync_Should_Return_Successful_Response()
        {
            var response = await _sutApiClient.GetPokemonSpeciesDetailsAsync("https://pokeapi.co/api/v2/pokemon-species/132/");
            response.ShouldNotBeNull();
        }

        [Fact]
        public void GetPokemonSpeciesDetailsAsync_Should_Throw_When_NotFound()
        {
            _sutApiClient.GetPokemonSpeciesDetailsAsync("https://pokeapi.co/api/v2/pokemon-species/0/").ShouldThrow<ServiceErrorException>().StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public void GetPokemonSpeciesDetailsAsync_Should_Throw_When_Url_Is_Invalid()
        {
            _sutApiClient.GetPokemonSpeciesDetailsAsync("randomString").ShouldThrow<ServiceErrorException>().Message.ShouldContain("Invalid Url");
        }
    }
}
