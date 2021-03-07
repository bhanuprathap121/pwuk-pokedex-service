using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Microsoft.AspNetCore.Http;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.ServiceTests.Extensions;
using Pokeworld.Pokedex.ServiceTests.Infrastructure;

namespace Pokeworld.Pokedex.ServiceTests.Scenarios
{
    public partial class BasicPokemonTests
    {
        private const string PokemonName = "mewtwo";

        [Scenario]
        public async Task Get_basic_pokemon_success()
        {
            await Runner.RunScenarioAsync(
                given => As_a_Pokeworld_user_who_has_access_to_the_api(),
                when => I_am_requesting_the_basic_pokemon_details(PokemonName),
                then => I_should_see_the_basic_pokemon_details());
        }
    }

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
            AssertBasicPokemonResponse(response);
        }
        private static void AssertBasicPokemonResponse(BasicPokemonResponse response)
        {
            response.Name.Should().Be(PokemonName);
            response.Habitat.Should().Be("rare");
            response.IsLegendary.Should().BeTrue();
            response.Description.Should().NotBeEmpty();
        }
    }
}
