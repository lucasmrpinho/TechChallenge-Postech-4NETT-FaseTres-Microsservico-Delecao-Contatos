using Postech.GroupEight.TechChallenge.ContactManagement.Events.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events
{
    public record ContactDeletedEvent : IApplicationEvent
    {
        public Guid ContactId { get; init; }

        public string EventType { get; init; } = nameof(ContactDeletedEvent);

        public Guid GetEventId() => ContactId;
    }
}