using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class RequestSetup
    {
        internal static void AddDependencyRequestCorrelationId(this IServiceCollection services)
        {
            services.AddScoped<IRequestCorrelationId, DefaultRequestCorrelationId>();
        }
    }
}