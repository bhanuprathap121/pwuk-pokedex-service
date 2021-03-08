using System;
using System.Collections.Generic;
using System.Net.Http;
using LightBDD.XUnit2;
using Pokeworld.Pokedex.Api;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Pokeworld.Pokedex.ServiceTests.Infrastructure
{
    public class BaseFixture : FeatureFixture, IDisposable
    {
        protected HttpClient Client { get; }
        private readonly List<Action> _actionsOnDispose = new List<Action>();

        protected BaseFixture()
        {
            Client = ConfigHttpClient();
            _actionsOnDispose.Add(() => Client?.Dispose());
        }

        private HttpClient ConfigHttpClient()
        {
            var factory = new WebApplicationFactory<Startup>();
            _actionsOnDispose.Add(() => factory?.Dispose());
            return factory.CreateClient();
        }

        public void Dispose()
        {
            foreach (var action in _actionsOnDispose)
            {
                action();
            }
        }
    }
}