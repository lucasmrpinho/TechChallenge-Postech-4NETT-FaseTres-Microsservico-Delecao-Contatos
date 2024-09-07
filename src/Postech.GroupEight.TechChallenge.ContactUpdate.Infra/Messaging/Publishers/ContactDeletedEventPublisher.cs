using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Headers;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Publishers
{
    public class ContactDeletedEventPublisher(IQueue queue, IRequestCorrelationId requestCorrelationId) : IEventPublisher<ContactDeletedEvent>
    {
        private readonly IQueue _queue = queue;
        private readonly IRequestCorrelationId _requestCorrelationId = requestCorrelationId;

        public async Task<PublishedEventResult> PublishEventAsync(ContactDeletedEvent eventData)
        {
            try 
            {
                ContactDeletedQueueMessageHeader header = new(_requestCorrelationId.GetCorrelationId(), eventData.ContactId);
                await _queue.PublishMessageAsync(eventData, header);
                return new() {
                    EventId = eventData.ContactId,
                    PublishedAt = DateTime.UtcNow,
                    Status = PublishEventStatus.Success,
                    Description = "Event successfully published to integration queue"
                };
            }
            catch (Exception ex)
            {
                return new() {
                    EventId = eventData.ContactId,
                    Status = PublishEventStatus.Error,
                    Description = ex.Message
                };
            }
        }
    }
}