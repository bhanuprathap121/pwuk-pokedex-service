using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.FunTranslationsApi;
using Pokeworld.Pokedex.IntegrationTests.Infrastructure;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.IntegrationTests
{
    public class FunTranslationApiClientIntegrationTests
    {
        private readonly IFunTranslationsApiClient _sutApiClient;
        private readonly IClient _client = new Client();

        public FunTranslationApiClientIntegrationTests()
        {
            var settings = new InitialiseSettings();
            IUrlProvider urlProvider = new UrlProvider(settings.GetSettings<ServiceUrls>(ServiceUrls.Services));
            _sutApiClient = new FunTranslationsApiClient(_client, urlProvider);
        }


        [Fact]
        public async Task GetShakespeareTranslationAsync_Should_Return_Successful_Response()
        {
            var response = await _sutApiClient.GetShakespeareTranslatedText("You gave Mr. Tim a hearty meal");
            response.ShouldNotBeNull();
        }


        [Fact]
        public async Task GetYodaTranslationAsync_Should_Return_Successful_Response()
        {
            var response = await _sutApiClient.GetYodaTranslatedText("You gave Mr. Tim a hearty meal");
            response.ShouldNotBeNull();
        }
    }
}
