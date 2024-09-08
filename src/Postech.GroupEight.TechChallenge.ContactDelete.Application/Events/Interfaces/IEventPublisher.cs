using Postech.GroupEight.TechChallenge.ContactManagement.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Interfaces 
{
    public interface IEventPublisher<T> 
        where T : IApplicationEvent
    {
        Task<PublishedEventResult> PublishEventAsync(T eventData); 
    }
}