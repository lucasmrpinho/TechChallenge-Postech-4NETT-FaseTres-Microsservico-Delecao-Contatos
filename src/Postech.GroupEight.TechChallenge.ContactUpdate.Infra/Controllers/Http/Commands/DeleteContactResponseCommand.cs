using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record DeleteContactResponseCommand
    {
        [JsonPropertyName("contactId")]
        public Guid ContactId { get; init; }

        [JsonPropertyName("isContactNotifiedForDelete")]
        public bool IsContactNotifiedForDelete { get; init; }

        [JsonPropertyName("contactNotifiedForUpdateAt")]
        public DateTime? ContactNotifiedForDeleteAt { get; init; }
    }
}