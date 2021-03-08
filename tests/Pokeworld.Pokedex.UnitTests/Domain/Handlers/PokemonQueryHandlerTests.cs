using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Contracts;
using Pokeworld.Pokedex.Clients.FunTranslationsApi.Queries;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Extensions;
using Pokeworld.Pokedex.Domain.Handlers;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Pokeworld.Pokedex.UnitTests.Domain.Handlers
{
    public class PokemonQueryHandlerTests
    {
        private readonly IPokemonQueryHandler _sutHandler;
        private readonly Mock<IPokeApiQueries> _pokeApiQueriesMock = new Mock<IPokeApiQueries>();
        private readonly Mock<IFunTranslationsApiQueries> _translationApiQueriesMock = new Mock<IFunTranslationsApiQueries>();
        private readonly Mock<ILogger<PokemonQueryHandler>> _loggerMock = new Mock<ILogger<PokemonQueryHandler>>();
        private readonly Fixture _fixture = new Fixture();
        private const string PokemonName = "not-important";


        public PokemonQueryHandlerTests()
        {
            _sutHandler = new PokemonQueryHandler(_pokeApiQueriesMock.Object, _translationApiQueriesMock.Object, _loggerMock.Object);
        }

        #region GetBasicPokemonTests

        [Fact]
        public async Task GetAsync_Should_Return_BasicPokemonResponse()
        {
            var pokemonResponse = _fixture.Create<PokemonResponse>();
            var speciesResponse = _fixture.Create<PokemonSpeciesResponse>();
            var expectedResponse = pokemonResponse.ToBasicPokemonResponse(speciesResponse);


            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ReturnsAsync(speciesResponse).Verifiable();

            var response = await _sutHandler.GetAsync(PokemonName);

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueriesMock.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once); 
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void GetAsync_Throws_When_The_Pokemon_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
        }

        [Fact]
        public void GetAsync_Throws_When_The_Pokemon_Species_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            var pokemonResponse = _fixture.Create<PokemonResponse>();

            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueriesMock.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once);
        }

        #endregion

        #region GetTranslatedPokemonTests

        [Fact]
        public async Task GetTranslatedAsync_Should_Return_TranslatedPokemonResponse()
        {
            var pokemonResponse = _fixture.Create<PokemonResponse>();
            var speciesResponse = _fixture.Create<PokemonSpeciesResponse>();
            var translatedResponse = _fixture.Create<TranslationResponse>();

            var expectedResponse = pokemonResponse.ToBasicPokemonResponse(speciesResponse);
            expectedResponse.Description = translatedResponse.Contents.Translated;

            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ReturnsAsync(speciesResponse).Verifiable();
            _translationApiQueriesMock.Setup(m => m.GetTranslation(It.IsAny<BasicPokemonResponse>())).ReturnsAsync(translatedResponse);

            var response = await _sutHandler.GetTranslatedAsync(PokemonName);

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueriesMock.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once);
            _translationApiQueriesMock.Verify(m => m.GetTranslation(It.IsAny<BasicPokemonResponse>()), Times.Once);
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetTranslatedAsync_Should_LogError_And_Return_StandardPokemonResponse()
        {
            var pokemonResponse = _fixture.Create<PokemonResponse>();
            var speciesResponse = _fixture.Create<PokemonSpeciesResponse>();
            var translatedResponse = _fixture.Create<TranslationResponse>();

            var standardExpectedResponse = pokemonResponse.ToBasicPokemonResponse(speciesResponse);

            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ReturnsAsync(speciesResponse).Verifiable();
            _translationApiQueriesMock.Setup(m => m.GetTranslation(It.IsAny<BasicPokemonResponse>())).Throws<ServiceErrorException>();
            

            var response = await _sutHandler.GetTranslatedAsync(PokemonName);

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueriesMock.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once);
            _translationApiQueriesMock.Verify(m => m.GetTranslation(It.IsAny<BasicPokemonResponse>()), Times.Once);
            _loggerMock.Verify(
                m => m.Log(LogLevel.Error, It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Error in GetTranslatedAsync")), null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);

            response.Should().BeEquivalentTo(standardExpectedResponse);
        }

        [Fact]
        public void GetTranslatedAsync_Throws_When_The_Pokemon_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
        }

        [Fact]
        public void GetTranslatedAsync_Throws_When_The_Pokemon_Species_Not_Exists()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            var pokemonResponse = _fixture.Create<PokemonResponse>();

            _pokeApiQueriesMock.Setup(m => m.GetPokemonResponse(PokemonName)).ReturnsAsync(pokemonResponse).Verifiable();
            _pokeApiQueriesMock.Setup(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url)).ThrowsAsync(new ServiceErrorException("not-found")).Verifiable();

            _sutHandler.GetAsync(PokemonName).ShouldThrowAsync<PokemonNotExistException>().Result.Message.ShouldContain("not-found");

            _pokeApiQueriesMock.Verify(m => m.GetPokemonResponse(PokemonName), Times.Once);
            _pokeApiQueriesMock.Verify(m => m.GetPokemonSpeciesDetails(pokemonResponse.Species.Url), Times.Once);
        }

        #endregion

    }
}
