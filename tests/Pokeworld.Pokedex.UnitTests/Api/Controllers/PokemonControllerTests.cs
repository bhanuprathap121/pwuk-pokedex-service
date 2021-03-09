using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pokeworld.Pokedex.Api.Controllers;
using Pokeworld.Pokedex.Clients;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.Domain.Exceptions;
using Pokeworld.Pokedex.Domain.Handlers;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Api.Controllers
{
    public class PokemonControllerTests
    {
        private readonly PokemonController _sutController;
        private readonly Mock<ILogger<PokemonController>> _loggerMock = new Mock<ILogger<PokemonController>>();
        private readonly Mock<IPokemonQueryHandler> _queryHandlerMock = new Mock<IPokemonQueryHandler>();
        private readonly Fixture _fixture = new Fixture();
        private const string PokemonName = "not-important";

        public PokemonControllerTests()
        {
            _sutController = new PokemonController(_loggerMock.Object, _queryHandlerMock.Object);
        }

        [Fact]
        public async Task GetAsync_Should_Return_200_When_Request_Is_Successful()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            _queryHandlerMock.Setup(m => m.GetAsync(PokemonName)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutController.GetAsync(PokemonName);

            _queryHandlerMock.Verify(m => m.GetAsync(PokemonName), Times.Once);
            var result = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task GetAsync_Should_Return_404_When_The_Pokemon_Not_Exists()
        {
            _queryHandlerMock.Setup(m => m.GetAsync(It.IsAny<string>())).ThrowsAsync(new PokemonNotExistException());

            var response = await _sutController.GetAsync(PokemonName);

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAsync_Should_Return_Error()
        {
            _queryHandlerMock.Setup(m => m.GetAsync(It.IsAny<string>())).ThrowsAsync(new ServiceErrorException("unknown", HttpStatusCode.BadGateway));

            var response = await _sutController.GetAsync(PokemonName);

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<ObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.BadGateway);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Error_For_WhiteSpace()
        {
            var response = await _sutController.GetAsync(" ");

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<BadRequestResult>().StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetTranslatedAsync_Should_Return_200_When_Request_Is_Successful()
        {
            var expectedResponse = _fixture.Create<BasicPokemonResponse>();
            _queryHandlerMock.Setup(m => m.GetTranslatedAsync(PokemonName)).ReturnsAsync(expectedResponse).Verifiable();

            var response = await _sutController.GetTranslatedPokemonAsync(PokemonName);

            _queryHandlerMock.Verify(m => m.GetTranslatedAsync(PokemonName), Times.Once);
            var result = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task GetTranslatedAsync_Should_Return_404_When_The_Pokemon_Not_Exists()
        {
            _queryHandlerMock.Setup(m => m.GetTranslatedAsync(It.IsAny<string>())).ThrowsAsync(new PokemonNotExistException());

            var response = await _sutController.GetTranslatedPokemonAsync(PokemonName);

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetTranslatedAsync_Should_Return_Error()
        {
            _queryHandlerMock.Setup(m => m.GetTranslatedAsync(It.IsAny<string>())).ThrowsAsync(new ServiceErrorException("unknown", HttpStatusCode.BadGateway));

            var response = await _sutController.GetTranslatedPokemonAsync(PokemonName);

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<ObjectResult>().StatusCode.Should().Be((int)HttpStatusCode.BadGateway);
        }

        [Fact]
        public async Task GetTranslatedAsync_Should_Return_Error_For_WhiteSpace()
        {
            var response = await _sutController.GetTranslatedPokemonAsync(" ");

            response.Result.ShouldNotBeNull();
            response.Result.ShouldBeOfType<BadRequestResult>().StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
