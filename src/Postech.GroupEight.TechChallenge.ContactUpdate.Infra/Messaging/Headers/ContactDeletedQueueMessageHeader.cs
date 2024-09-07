using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Headers
{
    [ExcludeFromCodeCoverage]
    public class ContactDeletedQueueMessageHeader(
        Guid correlationId,
        Guid messageId) 
        : IQueueMessageHeader
    {
        public Guid CorrelationId { get; } = correlationId;

        public Guid MessageId { get; } = messageId;

        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public string Source { get; } = "contact.delete.microservice";

        public string MessageType { get; } = nameof(ContactDeletedEvent);

        public override bool Equals(object? obj)
        {
            return obj is ContactDeletedQueueMessageHeader header && MessageId.Equals(header.MessageId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MessageId);
        }
    }
}