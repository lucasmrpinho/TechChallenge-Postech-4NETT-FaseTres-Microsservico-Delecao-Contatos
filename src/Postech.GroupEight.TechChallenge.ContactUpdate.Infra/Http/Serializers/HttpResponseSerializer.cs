using System.Text.Json;
using Microsoft.Extensions.Primitives;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Extensions.Common;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Exceptions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Results;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers
{
    public static class HttpResponseSerializer
    {
        public static HttpResponseSerializeResult Serialize<T>(T responseObject, StringValues acceptHeader)
        {
            if (StringValues.IsNullOrEmpty(acceptHeader) || acceptHeader.ContainsAny("application/*", "*/*", "application/json"))
            {
                return new() { ContentType = "application/json", Data = JsonSerializer.Serialize(responseObject) };
            }
            throw new HttpResponseSerializerException("The given media types are not supported", acceptHeader);
        }
    }
}