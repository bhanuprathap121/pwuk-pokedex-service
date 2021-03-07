using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Pokeworld.Pokedex.Clients.PokeApi;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Clients.PokeApi.Queries
{
    public class PokeApiQueriesTests
    {
        private readonly Mock<IPokeApiClient> _apiClient = new Mock<IPokeApiClient>();
        private readonly IPokeApiQueries _sutApiQueries;
        private readonly Fixture _fixture  = new Fixture();
        private const string Name = "randomPokemon";
        private const string SpeciesUrl = "https://not-important.com";

        public PokeApiQueriesTests()
        {
            _sutApiQueries = new PokeApiQueries(_apiClient.Object);
        }

        [Fact]
        public async Task GetPokemonResponse_Should_Return_Correct_Response()
        {
            var expectedResponse = _fixture.Create<PokemonResponse>();
            _apiClient.Setup(m => m.GetPokemonDetailsAsync(Name)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutApiQueries.GetPokemonResponse(Name);

            response.ShouldBe(expectedResponse);
            _apiClient.Verify(m => m.GetPokemonDetailsAsync(Name), Times.Once);
        }

        [Fact]
        public async Task GetPokemonSpeciesDetails_Should_Return_Correct_Response()
        {
            var expectedResponse = _fixture.Create<PokemonSpeciesResponse>();
            _apiClient.Setup(m => m.GetPokemonSpeciesDetailsAsync(SpeciesUrl)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutApiQueries.GetPokemonSpeciesDetails(SpeciesUrl);

            response.ShouldBe(expectedResponse);
            _apiClient.Verify(m => m.GetPokemonSpeciesDetailsAsync(SpeciesUrl), Times.Once);
        }
    }
}
