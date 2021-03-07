using System;
using Microsoft.Extensions.Configuration;
using Pokeworld.Pokedex.Clients;

namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public class ServiceSettingsFactory
    {
        public static ServiceSettings GetServiceSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Development.json", false, true)
                .Build();

            var serviceUrls = new ServiceUrls();
            config.GetSection(ServiceUrls.Services).Bind(serviceUrls);

            return new ServiceSettings
            {
                PokeAPI =  serviceUrls.PokeApiUrl
            };
        }
    }
}
