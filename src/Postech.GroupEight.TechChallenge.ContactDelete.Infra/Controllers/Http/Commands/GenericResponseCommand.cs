using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands
{
    [ExcludeFromCodeCoverage]
    public record GenericResponseCommand<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; init; }

        [JsonPropertyName("messages")]
        public IEnumerable<Notification>? Messages { get; init; }

        [JsonPropertyName("isSuccessfullyProcessed")]
        public bool IsSuccessfullyProcessed => Data is not null;

        [JsonIgnore]
        private int StatusCode { get; set; }

        public GenericResponseCommand<T> IfProcessedSuccessfully(int successfulStatusCode)
        {
            if (IsSuccessfullyProcessed)
            {
                StatusCode = successfulStatusCode;
            }
            return this;
        }

        public GenericResponseCommand<T> IfProcessedWithError(int failureStatusCode)
        {
            if (!IsSuccessfullyProcessed)
            {
                StatusCode = failureStatusCode;
            }
            return this;
        }

        public int GetResponseStatusCode() => StatusCode;
    }
}