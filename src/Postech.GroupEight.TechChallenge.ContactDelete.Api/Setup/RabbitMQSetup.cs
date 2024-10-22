using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Integrations.RabbitMQ.Adapters;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Integrations.RabbitMQ.Configurations;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;


namespace Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup
{
    internal static class RabbitMQSetup
    {
        [ExcludeFromCodeCoverage]
        internal static void AddDependencyRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConnectionConfiguration>(configuration.GetSection(nameof(RabbitMQConnectionConfiguration)));
            RabbitMQConnectionConfiguration? connectionConfiguration = configuration.GetSection(nameof(RabbitMQConnectionConfiguration)).Get<RabbitMQConnectionConfiguration>();
            ArgumentNullException.ThrowIfNull(connectionConfiguration);
            services.AddMassTransit(m =>
            {
                m.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(connectionConfiguration.HostAddress, h =>
                    {
                        h.Username(connectionConfiguration.HostUsername);
                        h.Password(connectionConfiguration.HostPassword);
                    });
                    cfg.Message<ContactDeletedEvent>(m =>
                    {
                        m.SetEntityName(connectionConfiguration.MessageConfiguration.EntityName);
                    });
                    cfg.Publish<ContactDeletedEvent>(p =>
                    {
                        p.ExchangeType = connectionConfiguration.PublishConfiguration.ExchangeType;
                    });
                    cfg.Send<ContactDeletedEvent>(s =>
                    {
                        s.UseRoutingKeyFormatter(context => context.Message.EventType);
                    });
                    cfg.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(connectionConfiguration.CircuitBreakerConfiguration.TrackingPeriodInMinutes);
                        cb.TripThreshold = connectionConfiguration.CircuitBreakerConfiguration.TripThreshold;
                        cb.ActiveThreshold = connectionConfiguration.CircuitBreakerConfiguration.ActiveThreshold;
                        cb.ResetInterval = TimeSpan.FromMinutes(connectionConfiguration.CircuitBreakerConfiguration.ResetIntervalInMinutes);
                    });
                });
            });
            services.AddScoped<IQueue, RabbitMQAdapter>();
        }
    }
}