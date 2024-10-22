using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results.Enumerators;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results 
{
    [ExcludeFromCodeCoverage]
    public record PublishedEventResult
    {
        public Guid EventId { get; init; }
        public PublishEventStatus Status { get; init; }
        public DateTime? PublishedAt { get; init; }
        public required string Description { get; init; }

        internal bool IsEventPublished()
        {
            return Status.Equals(PublishEventStatus.Success);
        }
    }
}