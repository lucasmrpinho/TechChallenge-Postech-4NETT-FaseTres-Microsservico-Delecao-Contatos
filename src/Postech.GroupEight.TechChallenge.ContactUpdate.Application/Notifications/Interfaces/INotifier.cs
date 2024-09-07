using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Enumerators;
using Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Models;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Application.Notifications.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        bool HasNotification(NotificationType notificationType);
        IEnumerable<Notification> GetNotifications();
        void Handle(Notification notification);
        void Handle(IEnumerable<Notification> notifications);
    }
}