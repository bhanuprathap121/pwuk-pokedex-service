using System.Linq;
using AutoFixture;
using FluentAssertions;
using Pokeworld.Pokedex.Domain.Extensions;
using Shouldly;
using Xunit;

namespace Pokeworld.Pokedex.UnitTests.Domain.Extensions
{
    public class StringExtensionsTests
    {

        [Fact]
        public void ToBasicPokemonResponse_Should_Return_Correct_Response()
        {
            var inputString = "For some time\fafter its birth, it\ngrows by gaining nourishment from\nthe seed on its back.";

            var result = inputString.RemoveEscapeCharacters();

            result.Should().Be("For some time after its birth, it grows by gaining nourishment from the seed on its back.");
        }

    }
}
