using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Extensions;
using Pokeworld.Pokedex.Domain.Handlers;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Domain.Handlers
{
    public class PokemonQueryHandlerTests
    {
        private readonly IPokemonQueryHandler _sutHandler;
        private readonly Mock<IPokeApiQueries> _pokeApiQueries = new Mock<IPokeApiQueries>();
        private readonly Fixture _fixture = new Fixture();
        private const string PokemonName = "not-important";


        public PokemonQueryHandlerTests()
        {
            _sutHandler = new PokemonQueryHandler(_pokeApiQueries.Object);
        }

        [Fact]
        public async Task GetAsync_Should_Return_BasicPokemonResponse()
        {
            var pokemonResponse = _fixture.Create<PokemonResponse>();
            var speciesResponse = _fixture.Create<PokemonSpeciesResponse>();
            var expectedResponse = pokemonResponse.ToBasicPokemonResponse(speciesResponse);


            _pokeApiQueries.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueries.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ReturnsAsync(speciesResponse).Verifiable();

            var response = await _sutHandler.GetAsync(PokemonName);

            _pokeApiQueries.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueries.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once); 
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void GetAsync_Throws_When_The_Pokemon_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            _pokeApiQueries.Setup(m => m.GetPokemonResponse(PokemonName)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueries.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
        }

        [Fact]
        public void GetAsync_Throws_When_The_Pokemon_Species_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            var pokemonResponse = _fixture.Create<PokemonResponse>();

            _pokeApiQueries.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueries.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueries.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueries.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once);
        }
    }
}
