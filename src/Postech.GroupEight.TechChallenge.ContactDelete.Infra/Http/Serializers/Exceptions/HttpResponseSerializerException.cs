using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Primitives;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class HttpResponseSerializerException(string? message, StringValues acceptHeaderValues) : Exception(message)
    {
        public StringValues AcceptHeaderValues { get; } = acceptHeaderValues;
    }
}