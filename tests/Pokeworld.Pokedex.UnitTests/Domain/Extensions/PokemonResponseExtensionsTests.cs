using System.Linq;
using AutoFixture;
using Pokeworld.Pokedex.Clients.PokeApi.Contracts;
using Pokeworld.Pokedex.Domain.Extensions;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Domain.Extensions
{
    public class PokemonResponseExtensionsTests
    {
        private readonly Fixture _fixture = new Fixture();
        [Fact]
        public void ToBasicPokemonResponse_Should_Return_Correct_Response()
        {
            var pokemonResponse = _fixture.Create<PokemonResponse>();
            var pokemonSpeciesResponse = _fixture.Create<PokemonSpeciesResponse>();

            var result = pokemonResponse.ToBasicPokemonResponse(pokemonSpeciesResponse);

            result.Name.ShouldBe(pokemonResponse.Name);
            result.Habitat.ShouldBe(pokemonSpeciesResponse.Habitat.Name);
            result.IsLegendary.ShouldBe(pokemonSpeciesResponse.IsLegendary);
            result.Description.ShouldBe(pokemonSpeciesResponse.FlavorTextEntries.FirstOrDefault()?.FlavorText);
        }

        // cases where flavor text is empty
        // where pokemonSpecies is empty
    }
}
