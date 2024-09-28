using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.OpenApi;

[ExcludeFromCodeCoverage]
public record ContactDeleteEndpointOpenApiDocumentation : IEndpointOpenApiDocumentation
{
    private static readonly Guid _contactId = Guid.NewGuid();

    public OpenApiOperation GetOpenApiDocumentation()
    {
        return new()
        {
            Summary = "Requests an delete from a specific contact.",
            Description = "This endpoint requests the delete of data for a previously registered contact for the persistence microservice.",
            Parameters = GetParametersDocumentation(),
            RequestBody = GetRequestBodyDocumentation(),
            Responses = GetResponsesDocumentation(),
            Tags = [new() { Name = "/contacts"}]
        };
    }

    private static OpenApiResponses GetResponsesDocumentation()
    {
        return new OpenApiResponses
        {
            ["202"] = new OpenApiResponse
            {
                Description = "Contact delete request successfully sent to persistence microservice.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Description = "Data describing the outcome of a contact delete request.",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["messages"] = new OpenApiSchema
                                {
                                    Type = "array",
                                    Description = "Notifications about the contact delete request process.",
                                    Nullable = true,
                                    Items = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Description = "Notification about the contact delete request process.",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["message"] = new OpenApiSchema
                                            {
                                                Type = "string",
                                                Description = "The notification content.",
                                                Example = new OpenApiString("Contact delete request sent successfully.") 
                                            },
                                            ["type"] = new OpenApiSchema
                                            {
                                                Type = "integer",
                                                Description = "The identification of the type of notification.\n0: Info\n1: Warning\n2: Error\n3: Critical",
                                                Example = new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                Enum = [
                                                    new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Warning.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Critical.GetHashCode())
                                                ]
                                            }
                                        }
                                    },                                  
                                },
                                ["data"] = new OpenApiSchema
                                {
                                    Type = "object",
                                    Description = "Data from the contact delete request.",
                                    Nullable = true,
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["contactId"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "uuid",
                                            Description = "The unique identifier of the contact sent for delete.",
                                            Example = new OpenApiString(_contactId.ToString()) 
                                        },
                                        ["isContactNotifiedForDelete"] = new OpenApiSchema
                                        {
                                            Type = "boolean",
                                            Description = "Indicates whether the contact was sent for delete.",
                                            Example = new OpenApiBoolean(true)
                                        },
                                        ["contactNotifiedForDeleteAt"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "date-time",
                                            Description = "Date and time the contact was sent for delete.",
                                            Example = new OpenApiString(DateTime.UtcNow.ToString())
                                        }
                                    }
                                },
                                ["isSuccessfullyProcessed"] = new OpenApiSchema
                                {
                                    Type = "boolean",
                                    Description = "Indicates whether the contact delete request was processed successfully.",
                                    Example = new OpenApiBoolean(true)
                                }
                            }
                        }
                    }
                }
            },
            ["400"] = new OpenApiResponse
            {
                Description = "The request was sent with invalid data that did not allow the contact delete request.",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Description = "Data describing the outcome of a contact delete request.",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["messages"] = new OpenApiSchema
                                {
                                    Type = "array",
                                    Description = "Notifications about errors identified during the contact delete request.",
                                    Nullable = true,
                                    Items = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Description = "Notification about errors identified during the contact delete request.",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["message"] = new OpenApiSchema
                                            {
                                                Type = "string",
                                                Description = "The notification content.",
                                                Example = new OpenApiString("The contact delete request was not sent.") 
                                            },
                                            ["type"] = new OpenApiSchema
                                            {
                                                Type = "integer",
                                                Description = "The identification of the type of notification.\n0: Info\n1: Warning\n2: Error\n3: Critical",
                                                Example = new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                Enum = [
                                                    new OpenApiInteger(NotificationType.Info.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Warning.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Error.GetHashCode()),
                                                    new OpenApiInteger(NotificationType.Critical.GetHashCode())
                                                ]
                                            }
                                        }
                                    },                                  
                                },
                                ["data"] = new OpenApiSchema
                                {
                                    Type = "object",
                                    Description = "Data from the contact delete request.",
                                    Nullable = true,
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["contactId"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "uuid",
                                            Description = "The unique identifier of the contact sent for delete.",
                                            Example = new OpenApiString(null)
                                        },
                                        ["isContactNotifiedForDelete"] = new OpenApiSchema
                                        {
                                            Type = "boolean",
                                            Description = "Indicates whether the contact was sent for delete.",
                                            Example = new OpenApiString(null)
                                        },
                                        ["contactNotifiedForDeleteAt"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "date-time",
                                            Description = "Date and time the contact was sent for delete.",
                                            Example = new OpenApiBoolean(false)
                                        }
                                    },
                                    Example = null
                                },
                                ["isSuccessfullyProcessed"] = new OpenApiSchema
                                {
                                    Type = "boolean",
                                    Description = "Indicates whether the contact delete request was processed successfully.",
                                    Example = new OpenApiBoolean(false)
                                }
                            }
                        }
                    }
                }
            },
        };
    }

    private static OpenApiRequestBody GetRequestBodyDocumentation()
    {
        return new OpenApiRequestBody
        {
            Required = true,
            Description = "Data required to request a contact delete.",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Required = new HashSet<string> { "contactId" },
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            ["contactId"] = new OpenApiSchema()
                            {
                                Type = "string",
                                Format = "uuid",
                                Description = "Identifier of the contact whose delete request will be sent to.",
                                Example = new OpenApiString(_contactId.ToString()),
                            },
                        }
                    }
                }
            }
        };
    }

    private static IList<OpenApiParameter> GetParametersDocumentation()
    {
        return 
        [
            new()
            {
                Name = "contactId",
                In = ParameterLocation.Path,
                Required = true,
                Description = "Identifier of the contact whose delete request will be sent to.",
                Schema = new OpenApiSchema { Type = "string", Format = "uuid"  }
            }
        ];
    }
}