namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context.Interfaces
{
    public interface IRequestCorrelationId
    {
        Guid GetCorrelationId();
    }
}