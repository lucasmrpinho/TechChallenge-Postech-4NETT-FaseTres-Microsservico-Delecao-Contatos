using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi;

[ExcludeFromCodeCoverage]
public record ContactDeleteEndpointOpenApiDocumentation : IEndpointOpenApiDocumentation
{
    public string EndpointName => "Delete Contact";

    public string OperationSummary => "Delete an existing contact";

    public string OperationDescription => "Deletes an existing contact according to the Identifier provided";

    public string ParameterDescription => "Contact Identifier";

    public Tuple<int, string>[] ResponsesDetails => [new Tuple<int, string>(202, "Contact deleted"), new Tuple<int, string>(400, "The Identifier provided to delete the contact is invalid"), new Tuple<int, string>(500, "Unexpected error while deleting contact")];

    public SwaggerResponseAttribute[] GetResponsesDetails()
    {
        return ResponsesDetails.Select(responseDetail => new SwaggerResponseAttribute(responseDetail.Item1, responseDetail.Item2)).ToArray();
    }
}