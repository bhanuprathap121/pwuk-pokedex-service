using LightBDD.XUnit2;
using Pokeworld.Pokedex.ServiceTests.Infrastructure;

[assembly: ConfiguredLightBddScope]
[assembly: ClassCollectionBehavior(AllowTestParallelization = true)]

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
    }
}