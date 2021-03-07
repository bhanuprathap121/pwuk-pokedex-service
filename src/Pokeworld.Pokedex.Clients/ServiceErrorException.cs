using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Pokeworld.Pokedex.Clients
{
    [ExcludeFromCodeCoverage]
    public class ServiceErrorException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ServiceErrorException() : base()
        {
        }

        public ServiceErrorException(string message) : base(message)
        {
        }

        public ServiceErrorException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ServiceErrorException(string message, Exception inner, HttpStatusCode statusCode) : base(message, inner)
        {
            StatusCode = statusCode;
        }

    }
}