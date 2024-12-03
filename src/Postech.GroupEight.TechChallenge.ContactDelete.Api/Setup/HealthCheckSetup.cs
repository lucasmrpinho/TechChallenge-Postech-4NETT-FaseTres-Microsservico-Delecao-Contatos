using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Api.HealthChecks;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Integrations.RabbitMQ.Configurations;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class HealthCheckSetup
    {
        internal static void AddRabbitMQHealthCheck(this IHealthChecksBuilder healthChecks)
        {
            healthChecks.AddCheck<MassTransitRabbitMqHealthCheck>(nameof(RabbitMQConnectionConfiguration));
        }
    }
}