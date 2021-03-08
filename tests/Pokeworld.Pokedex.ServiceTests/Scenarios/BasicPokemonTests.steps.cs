using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.ServiceTests.Extensions;
using Pokeworld.Pokedex.ServiceTests.Infrastructure;

namespace Pokeworld.Pokedex.ServiceTests.Scenarios
{
    public partial class BasicPokemonTests : BaseFixture
    {
        private HttpResponseMessage _viewBasicPokemonResponse;

        private static Task As_a_Pokeworld_user_who_has_access_to_the_api()
        {
            return Task.CompletedTask;
        }

        private async Task I_am_requesting_the_basic_pokemon_details(string name)
        {
            _viewBasicPokemonResponse = await PokeApiServiceClient.GetBasicPokemonDetails(Client, name);
            await Task.CompletedTask;
        }

        private async Task I_should_see_the_basic_pokemon_details()
        {
            _viewBasicPokemonResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            var response = await _viewBasicPokemonResponse.DeserializeResponseContent<BasicPokemonResponse>();
            AssertBasicPokemonResponse(response, _mewtwoPokemonResponse);
        }
        private static void AssertBasicPokemonResponse(BasicPokemonResponse response, BasicPokemonResponse expectedPokemonResponse)
        {
            response.Name.Should().Be(expectedPokemonResponse.Name);
            response.Habitat.Should().Be(expectedPokemonResponse.Habitat);
            response.IsLegendary.Should().Be(expectedPokemonResponse.IsLegendary);
            response.Description.Should().Be(expectedPokemonResponse.Description);
        }
    }
}