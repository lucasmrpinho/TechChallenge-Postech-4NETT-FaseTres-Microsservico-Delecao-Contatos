using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class NotifierSetup
    {
        internal static void AddDependencyNotifier(this IServiceCollection services)
        {
            services.AddScoped<INotifier, DefaultNotifier>();
        }
    }
}