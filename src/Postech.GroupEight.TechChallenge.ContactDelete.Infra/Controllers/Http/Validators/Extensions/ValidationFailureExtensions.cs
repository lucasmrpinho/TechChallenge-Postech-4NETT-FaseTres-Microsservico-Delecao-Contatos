using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Validators.Extensions
{
    [ExcludeFromCodeCoverage]
    internal static class ValidationFailureExtensions
    {
        public static IEnumerable<Notification> AsNotifications(this List<ValidationFailure> failures)
        {
            return failures.Select(failure => new Notification() {
                Message = failure.ErrorMessage,
                Type = NotificationType.Error
            });
        }
    }
}