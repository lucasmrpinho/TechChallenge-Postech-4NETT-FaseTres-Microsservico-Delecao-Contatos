using Bogus;
using FluentAssertions;
using Moq;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Exceptions.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactDelete.UnitTests.Suite.Application.UseCases 
{
    public class DeleteContactUseCaseTests
    {   
        
        private readonly Faker _faker = new("pt_BR");
        
        [Fact(DisplayName = "Delete contact")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_DeleteDataForValidContact_ShouldNotifyAboutContactDeletion()
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();
         
            DeleteContactInput deleteContactInput = new() 
            { 
                ContactId = contactId,
            };
            Mock<IEventPublisher<ContactDeletedEvent>> eventPublisher = new();    
            DateTime contactEventPublishedAt = new(2024, 8, 19, 12, 0, 0, DateTimeKind.Local);   
            eventPublisher
                .Setup(e => e.PublishEventAsync(new ContactDeletedEvent()
                {
                    ContactId = contactId,
                }))
                .ReturnsAsync(() => new PublishedEventResult()
                {
                    EventId = contactId,
                    PublishedAt = contactEventPublishedAt,
                    Description = "Event successfully published to integration queue"
                });
            DeleteContactUseCase useCase = new(eventPublisher.Object);

            // Act
            DeleteContactOutput deleteContactOutput = await useCase.ExecuteAsync(deleteContactInput);

            // Assert
            deleteContactOutput.ContactId.Should().Be(contactId);
            deleteContactOutput.IsContactNotifiedForDelete.Should().BeTrue();
            deleteContactOutput.ContactNotifiedForDeleteAt.Should().Be(contactEventPublishedAt);
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactDeletedEvent>(c => c.ContactId.Equals(contactId))), Times.Once());
        }

        [Fact(DisplayName = "Failure to notify contact deletion")]
        [Trait("Action", "ExecuteAsync")]
        public async Task ExecuteAsync_FailureToNotifyContactDeletion_ShouldReturnOutputResultIndicatingContactDeleteNotificationFailure()
        {
            // Arrange              
            Guid contactId = Guid.NewGuid();

            DeleteContactInput deleteContactInput = new() 
            { 
                ContactId = contactId
            };
            Mock<IEventPublisher<ContactDeletedEvent>> eventPublisher = new();    
            eventPublisher
                .Setup(e => e.PublishEventAsync(new ContactDeletedEvent()
                {
                    ContactId = contactId,
                }))
                .ReturnsAsync(() => new PublishedEventResult()
                {
                    EventId = contactId,
                    Status = PublishEventStatus.Error,
                    Description = "Failed to publish event to integration queue"
                });
            DeleteContactUseCase useCase = new(eventPublisher.Object);

            // Act
            DeleteContactOutput deleteContactOutput = await useCase.ExecuteAsync(deleteContactInput);

            // Assert
            deleteContactOutput.ContactId.Should().Be(contactId);
            deleteContactOutput.IsContactNotifiedForDelete.Should().BeFalse();
            deleteContactOutput.ContactNotifiedForDeleteAt.Should().BeNull();
            eventPublisher.Verify(e => e.PublishEventAsync(It.Is<ContactDeletedEvent>(c => c.ContactId.Equals(contactId))), Times.Once());
        }
        
    }
}