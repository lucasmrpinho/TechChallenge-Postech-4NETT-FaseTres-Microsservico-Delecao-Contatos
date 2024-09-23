using Postech.GroupEight.TechChallenge.ContactManagement.Events.Interfaces;
using System.Text.Json.Serialization;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Events
{
    public record ContactDeletedEvent : IApplicationEvent
    {
        public Guid ContactId { get; init; }

        [JsonIgnore]
        public string EventType { get; init; } = nameof(ContactDeletedEvent);

        public Guid GetEventId() => ContactId;
    }
}