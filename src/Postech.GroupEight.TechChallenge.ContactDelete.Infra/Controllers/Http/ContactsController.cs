using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Models;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Interfaces;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.UseCases.Outputs;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands.Extensions;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Validators.Extensions;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http
{
    [ExcludeFromCodeCoverage]
    public class ContactsController
    {
        public ContactsController(IHttp http)
        {
            http.On<DeleteContactRequestCommand, DeleteContactResponseCommand>("PATCH", "/contacts/{contactId}", async (body, routeValues, serviceProvider) =>
            {
                INotifier notifier = serviceProvider.GetRequiredService<INotifier>();
                if (body is null)
                {
                    notifier.Handle(new Notification() { Message = "Unable to read request body", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }
                _ = Guid.TryParse(routeValues["contactId"]?.ToString(), out Guid contactId);
                if (!body.HasSameContactId(contactId))
                {
                    notifier.Handle(new Notification() { Message = "Contact identifier is different between route and body parameter", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }
                IValidator<DeleteContactRequestCommand> validator = serviceProvider.GetRequiredService<IValidator<DeleteContactRequestCommand>>();
                ValidationResult validationResult = await validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    notifier.Handle(validationResult.Errors.AsNotifications());
                    return new() { Messages = notifier.GetNotifications() };
                }
                IDeleteContactUseCase useCase = serviceProvider.GetRequiredService<IDeleteContactUseCase>();
                DeleteContactOutput? deleteContactOutput = null;
                
                deleteContactOutput = await useCase.ExecuteAsync(body.AsDeleteContactInput());
                if (!deleteContactOutput.IsContactNotifiedForDelete)
                {
                    notifier.Handle(new Notification() { Message = "Unable to request contact deletion. Please try again.", Type = NotificationType.Error });
                    return new() { Messages = notifier.GetNotifications() };
                }
                
                return new() { Data = deleteContactOutput?.AsDeleteContactResponseCommand() };
            });
        }
    }
}