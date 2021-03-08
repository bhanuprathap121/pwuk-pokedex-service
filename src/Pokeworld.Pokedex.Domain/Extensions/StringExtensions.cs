using System;

namespace Pokeworld.Pokedex.Domain.Extensions
{
    public static class StringExtensions
    {
        private static readonly char[] EscapeCharacters = { '\a', '\f', '\n', '\r', '\t' };
        private const string NewSeparator = " ";

        public static string RemoveEscapeCharacters(this string input)
        {
            var temp = input.Split(EscapeCharacters, StringSplitOptions.RemoveEmptyEntries);
            var result = string.Join(NewSeparator, temp);
            return result;
        }
    }
}
