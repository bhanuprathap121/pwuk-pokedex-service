using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Pokeworld.Pokedex.Clients.PokeApi;
using Pokeworld.Pokedex.Clients.PokeApi.Queries;

namespace Pokeworld.Pokedex.Clients.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddClientDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IClient, Client>();
            services.AddSingleton<IUrlProvider, UrlProvider>();
            services.AddScoped<IPokeApiClient, PokeApiClient>();
            services.AddScoped<IPokeApiQueries, PokeApiQueries>();
        }
    }
}
