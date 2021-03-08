using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.FunTranslationsApi;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Clients.FunTranslationApi
{
    public class FunTranslationApiClientTests
    {
        private readonly IFunTranslationsApiClient _sutApiClient;
        private readonly Mock<IClient> _clientMock = new Mock<IClient>();
        private readonly Mock<IUrlProvider> _urlProviderMock = new Mock<IUrlProvider>();
        private readonly Fixture _fixture = new Fixture();
        private const string Url = "http://not-important.com";
        private const string Description = "Random Description";


        public FunTranslationApiClientTests()
        {
            _sutApiClient = new FunTranslationsApiClient(_clientMock.Object, _urlProviderMock.Object);
        }

        [Fact]
        public async Task GetShakespeareTranslatedText_Returns_Correct_Response()
        {
            var expectedResponse = _fixture.Create<TranslationResponse>();

            _clientMock.Setup(m => m.GetAsync<TranslationResponse>(Url)).ReturnsAsync(expectedResponse).Verifiable();
            _urlProviderMock.Setup(m => m.GetShakespeareTranslationUrl(It.IsAny<string>())).Returns(Url).Verifiable();

            var response = await _sutApiClient.GetShakespeareTranslatedText(Description);

            _clientMock.Verify(c => c.GetAsync<TranslationResponse>(Url), Times.Once());
            _urlProviderMock.Verify(p => p.GetShakespeareTranslationUrl(Description), Times.Once);
            response.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task GetYodaTranslatedText_Returns_Correct_Response()
        {
            var expectedResponse = _fixture.Create<TranslationResponse>();

            _clientMock.Setup(m => m.GetAsync<TranslationResponse>(Url)).ReturnsAsync(expectedResponse).Verifiable();
            _urlProviderMock.Setup(m => m.GetYodaTranslationUrl(It.IsAny<string>())).Returns(Url).Verifiable();

            var response = await _sutApiClient.GetYodaTranslatedText(Description);

            _clientMock.Verify(c => c.GetAsync<TranslationResponse>(Url), Times.Once());
            _urlProviderMock.Verify(p => p.GetYodaTranslationUrl(Description), Times.Once);
            response.Should().Be(expectedResponse);
        }
    }
}
