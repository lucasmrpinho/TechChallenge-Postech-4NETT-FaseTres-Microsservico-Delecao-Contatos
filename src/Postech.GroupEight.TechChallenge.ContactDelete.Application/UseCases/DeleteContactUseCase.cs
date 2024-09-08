using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Extensions;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Events.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Inputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.Entities;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases
{
    public class DeleteContactUseCase(IEventPublisher<ContactDeletedEvent> eventPublisher) : IDeleteContactUseCase
    {
        private readonly IEventPublisher<ContactDeletedEvent> _eventPublisher = eventPublisher;

        public async Task<DeleteContactOutput> ExecuteAsync(DeleteContactInput input)
        {
            ContactEntity contactEntity = input.AsContactEntity();
            contactEntity.Inactivate();
            PublishedEventResult eventResult = await _eventPublisher.PublishEventAsync(contactEntity.AsContactDeletedEvent());
            return DeleteContactOutput.CreateUsing(contactEntity, eventResult);
        }
    }
}