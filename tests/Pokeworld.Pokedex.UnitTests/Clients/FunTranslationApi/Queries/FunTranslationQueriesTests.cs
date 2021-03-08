using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Pokeworld.Pokedex.Clients.FunTranslationsApi;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Queries;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Clients.FunTranslationApi.Queries
{
    public class FunTranslationQueriesTests
    {
        private readonly Mock<IFunTranslationsApiClient> _apiClientMock = new Mock<IFunTranslationsApiClient>();
        private readonly IFunTranslationsApiQueries _sutApiQueries;
        private readonly Fixture _fixture = new Fixture();
        private const string Description = "Random Pokemon Description";

        public FunTranslationQueriesTests()
        {
            _sutApiQueries = new FunTranslationsApiQueries(_apiClientMock.Object);
        }

        [Fact]
        public async Task GetPokemonResponse_Should_Return_Correct_Response()
        {
            var expectedResponse = _fixture.Create<TranslationResponse>();
            var pokemonResponse = _fixture.Create<BasicPokemonResponse>();
            pokemonResponse.Description = Description;
            _apiClientMock.Setup(m => m.GetShakespeareTranslatedText(Description)).ReturnsAsync(expectedResponse).Verifiable();
            _apiClientMock.Setup(m => m.GetYodaTranslatedText(Description)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutApiQueries.GetTranslation(pokemonResponse);

            response.ShouldBe(expectedResponse);
            _apiClientMock.Verify(m => m.GetShakespeareTranslatedText(Description), Times.AtMostOnce);
            _apiClientMock.Verify(m => m.GetYodaTranslatedText(Description), Times.AtMostOnce);
        }

        [Theory]
        [InlineData("cave", false)]
        [InlineData("random", true)]
        public async Task GetPokemonResponse_Should_Return_Yoda_Translation(string habitat, bool isLegendary)
        {
            var expectedResponse = _fixture.Create<TranslationResponse>();
            var pokemonResponse = _fixture.Create<BasicPokemonResponse>();
            pokemonResponse.Habitat = habitat;
            pokemonResponse.IsLegendary = isLegendary;
            pokemonResponse.Description = Description;

            _apiClientMock.Setup(m => m.GetShakespeareTranslatedText(Description)).ReturnsAsync(expectedResponse).Verifiable();
            _apiClientMock.Setup(m => m.GetYodaTranslatedText(Description)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutApiQueries.GetTranslation(pokemonResponse);

            response.ShouldBe(expectedResponse);
            _apiClientMock.Verify(m => m.GetShakespeareTranslatedText(Description), Times.Never);
            _apiClientMock.Verify(m => m.GetYodaTranslatedText(Description), Times.Once);
        }
    }
}
