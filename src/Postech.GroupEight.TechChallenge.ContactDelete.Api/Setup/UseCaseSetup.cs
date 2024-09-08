using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class UseCaseSetup
    {
        internal static void AddDependencyUseCase(this IServiceCollection services)
        {
            services.AddScoped<IDeleteContactUseCase, DeleteContactUseCase>();
        }
    }
}