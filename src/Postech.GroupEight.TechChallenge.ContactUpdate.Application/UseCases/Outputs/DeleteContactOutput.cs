using System.Diagnostics.CodeAnalysis;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Entities;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs
{
    [ExcludeFromCodeCoverage]
    public record DeleteContactOutput
    {
        public Guid ContactId { get; init; }
        public bool IsContactNotifiedForDelete { get; init; }
        public DateTime? ContactNotifiedForDeleteAt { get; init; }

        internal static DeleteContactOutput CreateUsing(ContactEntity contactEntity, PublishedEventResult eventResult)
        {
            return new()
            {
                ContactId = contactEntity.Id,
                IsContactNotifiedForDelete = eventResult.IsEventPublished(),
                ContactNotifiedForDeleteAt = eventResult.PublishedAt
            };
        }
    }
}