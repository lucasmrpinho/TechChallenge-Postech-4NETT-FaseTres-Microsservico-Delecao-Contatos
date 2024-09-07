using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging.Headers.Interfaces;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Messaging 
{
    public interface IQueue
    {
        Task PublishMessageAsync<T>(T message, IQueueMessageHeader header) where T : notnull; 
    }
}