using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Inputs;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record DeleteContactRequestCommand
    {
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        internal DeleteContactInput AsDeleteContactInput()
        {
            return new()
            {
                ContactId = ContactId,
            };
        }

        internal bool HasSameContactId(Guid contactId)
        {
            return ContactId.Equals(contactId);
        }
    }
}