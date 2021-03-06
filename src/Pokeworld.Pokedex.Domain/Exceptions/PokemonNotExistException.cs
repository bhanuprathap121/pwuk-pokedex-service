using System;
using System.Collections.Generic;
using System.Text;

namespace Pokeworld.Pokedex.Domain.Exceptions
{
    public class PokemonNotExistException : Exception
    {
        public PokemonNotExistException()
        {
        }

        public PokemonNotExistException(string message) : base(message)
        {
        }
    }
}
