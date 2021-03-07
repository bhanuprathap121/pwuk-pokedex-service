using System;
using System.Net.Http;
using LightBDD.XUnit2;
using Pokeworld.Pokedex.Api;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public class BaseFixture : FeatureFixture, IDisposable
    {
        protected static HttpClient DevHttpClient = ConfigHttpClient();
        protected HttpClient Client { get; }

        public BaseFixture()
        {
            Client = DevHttpClient;
        }

        private static HttpClient ConfigHttpClient()
        {
            var factory = new WebApplicationFactory<Startup>();
            return factory.CreateClient();
        }
        public void Dispose()
        {
        }
    }
}
