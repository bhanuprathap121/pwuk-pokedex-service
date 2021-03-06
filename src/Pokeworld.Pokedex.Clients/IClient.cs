using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokeworld.Pokedex.Clients
{
    public interface IClient
    {
        public Task<T> GetAsync<T>(string path);
    }
}
