using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Pokeworld.Pokedex.IntegrationTests.Infrastructure
{
    public class InitialiseSettings
    {
        public IConfiguration Configuration { get; set; }
        public InitialiseSettings()
        {
            var path = AppContext.BaseDirectory;
            var appSettingsGeneral = Path.Combine(path, "appsettings.integrationtests.json");

            var config = new ConfigurationBuilder();
            config.AddJsonFile(appSettingsGeneral, optional: true, reloadOnChange: true);
            Configuration = config.Build();
        }


        public string PokeApiUrl()
        {
            return Configuration["Services:PokeApiUrl"];
        }

        public IOptions<T> GetSettings<T>(string sectionName) where T : class, new()
        {
            var section = Configuration.GetSection(sectionName);
            var sectionOptions = Activator.CreateInstance<T>();
            section.Bind(sectionOptions);
            var options = Options.Create(sectionOptions);
            return options;
        }
    }
}
