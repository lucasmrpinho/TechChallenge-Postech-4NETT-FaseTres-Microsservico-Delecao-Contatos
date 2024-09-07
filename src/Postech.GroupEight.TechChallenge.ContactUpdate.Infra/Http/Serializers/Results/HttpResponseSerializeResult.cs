namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Serializers.Results
{
    public record HttpResponseSerializeResult
    {
        public required string Data { get; init; }
        public required string ContentType { get; init; }
    }
}