using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Pokeworld.Pokedex.Contracts.Api.Responses;
using Pokeworld.Pokedex.ServiceTests.Extensions;
using Pokeworld.Pokedex.ServiceTests.Infrastructure;

namespace Pokeworld.Pokedex.ServiceTests.Scenarios
{
    public partial class TranslatedPokemonTests : BaseFixture
    {
        private HttpResponseMessage _viewTranslatedResponse;

        private static Task As_a_Pokeworld_user_who_has_access_to_the_api()
        {
            return Task.CompletedTask;
        }

        private async Task I_am_requesting_the_translated_pokemon_details(string name)
        {
            _viewTranslatedResponse = await FunTranslationApiServiceClient.GetTranslatedPokemonResponse(Client, name);
            await Task.CompletedTask;
        }

        private async Task I_should_see_the_translated_pokemon_details(string[] acceptedTranslations, BasicPokemonResponse expectedPokemon)
        {
            _viewTranslatedResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            var response = await _viewTranslatedResponse.DeserializeResponseContent<BasicPokemonResponse>();
            AssertBasicPokemonResponse(response, acceptedTranslations, expectedPokemon);
        }
        private static void AssertBasicPokemonResponse(BasicPokemonResponse response, string[] acceptedTranslations, BasicPokemonResponse expectedPokemon)
        {
            response.Name.Should().Be(expectedPokemon.Name);
            response.Habitat.Should().Be(expectedPokemon.Habitat);
            response.IsLegendary.Should().Be(expectedPokemon.IsLegendary);
            response.Description.Should().BeOneOf(acceptedTranslations);
        }
    }
}