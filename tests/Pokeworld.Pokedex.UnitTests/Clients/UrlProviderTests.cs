using AutoFixture;
using Microsoft.Extensions.Options;
using Pokeworld.Pokedex.Clients;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Clients
{
    public class UrlProviderTests
    {
        private readonly ServiceUrls _serviceUrls;
        private readonly IUrlProvider _sutUrlProvider;
        private const string Name = "randomPokemon";
        private readonly Fixture _fixture = new Fixture();


        public UrlProviderTests()
        {
            _serviceUrls = _fixture.Create<ServiceUrls>();
            _sutUrlProvider = new UrlProvider(Options.Create<ServiceUrls>(_serviceUrls));
        }

        [Fact]
        public void GetPokemonUrl_Should_Return_The_Correct_Url()
        {
            var url =_sutUrlProvider.GetPokemonUrl(Name);
            url.ShouldBeEquivalentTo($"{_serviceUrls.PokeApiUrl}/{Name}");
        }

    }
}
