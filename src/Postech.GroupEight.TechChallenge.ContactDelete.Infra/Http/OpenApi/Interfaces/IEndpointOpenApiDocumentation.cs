using Microsoft.OpenApi.Models;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi.Interfaces;

public interface IEndpointOpenApiDocumentation
{
    OpenApiOperation GetOpenApiDocumentation();
}