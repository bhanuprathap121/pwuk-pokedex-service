using System;
using System.Threading;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using Pokeworld.Pokedex.ServiceTests.Infrastructure;

[assembly: ConfiguredLightBddScope]
[assembly: ClassCollectionBehavior(AllowTestParallelization = true)]

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        private ServiceSettings _settings;

        protected override void OnSetUp()
        {
            _settings = ServiceSettingsFactory.GetServiceSettings();
        }

        protected override void OnTearDown()
        {
        }
    }
}
