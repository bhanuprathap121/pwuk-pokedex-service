using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.PokeApi;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Clients.PokeApi
{
    public class PokeApiClientTests
    {
        private readonly IPokeApiClient _sutApiClient;
        private readonly Mock<IClient> _clientMock = new Mock<IClient>();
        private readonly Mock<IUrlProvider> _urlProviderMock = new Mock<IUrlProvider>();
        private readonly Fixture _fixture = new Fixture();
        private const string Url = "http://not-important.com";
        private const string Name = "randomPokemon";

        public PokeApiClientTests()
        {
            _sutApiClient = new PokeApiClient(_clientMock.Object, _urlProviderMock.Object);
        }

        [Fact]
        public async Task GetPokemonDetailsAsync_Should_Return_Successful_Response()
        {
            var expectedResponse = _fixture.Create<PokemonResponse>();
            
            _clientMock.Setup(m => m.GetAsync<PokemonResponse>(Url)).ReturnsAsync(expectedResponse).Verifiable();
            _urlProviderMock.Setup(m => m.GetPokemonUrl(It.IsAny<string>())).Returns(Url).Verifiable();

            var response = await _sutApiClient.GetPokemonDetailsAsync(Name);

            _clientMock.Verify(c => c.GetAsync<PokemonResponse>(Url), Times.Once());
            _urlProviderMock.Verify(p => p.GetPokemonUrl(Name), Times.Once);
            response.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task GetPokemonSpeciesDetailsAsync_Should_Return_Successful_Response()
        {
            var expectedResponse = _fixture.Create<PokemonSpeciesResponse>();

            _clientMock.Setup(m => m.GetAsync<PokemonSpeciesResponse>(Url)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutApiClient.GetPokemonSpeciesDetailsAsync(Url);

            _clientMock.Verify(c => c.GetAsync<PokemonSpeciesResponse>(Url), Times.Once());
            response.Should().Be(expectedResponse);
        }
    }
}
