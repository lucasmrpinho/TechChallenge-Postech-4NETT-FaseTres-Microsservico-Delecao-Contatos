using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Requests.Context
{
    public record DefaultRequestCorrelationId : IRequestCorrelationId
    {
        private readonly Guid _correlationId = Guid.NewGuid();

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }
    }
}