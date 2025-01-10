using Flunt.Notifications;

namespace LivrosAPI.Application.Contracts.Persistence
{
    public interface IRequest
    {
        void AddValue(object @object);
        void AddNotification(Notification notification);
        void AddNotifications(IEnumerable<Notification> notifications);
    }
}
