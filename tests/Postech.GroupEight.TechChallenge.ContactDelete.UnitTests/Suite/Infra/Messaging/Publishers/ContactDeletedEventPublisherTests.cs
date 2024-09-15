using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Headers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Publishers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context;

namespace Postech.GroupEight.TechChallenge.ContactDelete.UnitTests.Suite.Infra.Messaging.Publishers 
{
    public class ContactDeletedEventPublisherTests
    {  
        
        private readonly Faker _faker = new("pt_BR");
        private readonly DefaultRequestCorrelationId _requestCorrelationId = new();  
        
        [Fact(DisplayName = "Event successfully published to integration queue")]
        [Trait("Action", "PublishEventAsync")]
        public async Task PublishEventAsync_EventSuccessfullyPublishedToIntegrationQueue_ShouldReturnResultIndicatingSuccess()
        {
            // Arrange
            ContactDeletedEvent contactDeletedEvent = new()
            {
                ContactId = Guid.NewGuid(),
            };
            ContactDeletedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), contactDeletedEvent.ContactId);
            Mock<IQueue> queue = new();
            queue.Setup(q => q.PublishMessageAsync(contactDeletedEvent, header));
            ContactDeletedEventPublisher publisher = new(queue.Object, _requestCorrelationId);

            // Act
            PublishedEventResult result = await publisher.PublishEventAsync(contactDeletedEvent);

            // Assert
            result.EventId.Should().Be(contactDeletedEvent.ContactId);
            result.PublishedAt.Should().NotBeNull().And.BeOnOrBefore(DateTime.UtcNow);
            result.Status.Should().Be(PublishEventStatus.Success);
            result.Description.Should().Be("Event successfully published to integration queue");
        }

        [Fact(DisplayName = "Failed to publish event to integration queue")]
        [Trait("Action", "PublishEventAsync")]
        public async Task PublishEventAsync_FailedToPublishEventToIntegrationQueue_ShouldReturnResultIndicatingError()
        {
            // Arrange
            ContactDeletedEvent contactDeletedEvent = new()
            {
                ContactId = Guid.NewGuid(),
            };
            ContactDeletedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), contactDeletedEvent.ContactId);
            Mock<IQueue> queue = new();
            queue
                .Setup(q => q.PublishMessageAsync(contactDeletedEvent, header))
                .ThrowsAsync(new Exception("Failed to publish event to integration queue"));
            ContactDeletedEventPublisher publisher = new(queue.Object, _requestCorrelationId);

            // Assert
            PublishedEventResult result = await publisher.PublishEventAsync(contactDeletedEvent);

            // Assert
            result.EventId.Should().Be(contactDeletedEvent.ContactId);
            result.PublishedAt.Should().BeNull();
            result.Status.Should().Be(PublishEventStatus.Error);
            result.Description.Should().Be("Failed to publish event to integration queue");
        }
    }
}