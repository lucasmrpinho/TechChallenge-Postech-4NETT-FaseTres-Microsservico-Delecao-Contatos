using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Validators;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class FluentValidationSetup
    {
        internal static void AddDependencyFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<DeleteContactRequestCommandValidator>();
        }
    }
}