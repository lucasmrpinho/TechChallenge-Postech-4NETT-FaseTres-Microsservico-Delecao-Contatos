using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Publishers;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class EventPublisherSetup
    {
        internal static void AddDependencyEventPublisher(this IServiceCollection services)
        {
            services.AddScoped<IEventPublisher<ContactDeletedEvent>, ContactDeletedEventPublisher>();
        }
    }
}