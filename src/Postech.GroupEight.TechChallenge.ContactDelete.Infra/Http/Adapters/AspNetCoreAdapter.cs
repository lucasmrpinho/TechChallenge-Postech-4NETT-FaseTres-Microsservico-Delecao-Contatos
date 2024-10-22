using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Deserializers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Deserializers.Exceptions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Extensions;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Adapters
{
    [ExcludeFromCodeCoverage]
    public class AspNetCoreAdapter(WebApplication app) : IHttp
    {
        private readonly WebApplication _app = app;

        public void On<TRequest, TResponse>(
            string method,
            string url,
            Func<TRequest?, IDictionary<string, object?>, IServiceProvider, Task<GenericResponseCommand<TResponse>>> callback,
            IEndpointOpenApiDocumentation openApiDocumentation,
            int successfulStatusCode = 202,
            int failureStatusCode = 400)
        {
            _app.MapMethods(url, [method.ToUpper()], async (HttpContext context, CancellationToken token) =>
            {
                try
                {
                    string? requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync(token);
                    TRequest? body = HttpRequestDeserializer.Deserialize<TRequest>(requestBody, context.Request.Headers.ContentType);
                    IDictionary<string, object?> routeValues = context.Request.GetRouteValues();
                    GenericResponseCommand<TResponse> responseContent = await callback(body, routeValues, context.RequestServices);
                    HttpResponseSerializeResult serializeResult = HttpResponseSerializer.Serialize(responseContent, context.Request.Headers.Accept);
                    context.Response.ContentType = serializeResult.ContentType;
                    context.Response.StatusCode = responseContent.IfProcessedSuccessfully(successfulStatusCode).IfProcessedWithError(failureStatusCode).GetResponseStatusCode();
                    await context.Response.WriteAsync(serializeResult.Data, token);
                }
                catch (HttpResponseDeserializerException ex)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    await context.Response.WriteAsync(ex.Message, token);
                }
            })
            .WithOpenApi(operation => openApiDocumentation.GetOpenApiDocumentation());
        }

        public void Run()
        {
            _app.Run();
        }
    }
}