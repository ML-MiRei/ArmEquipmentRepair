using ArmEquipmentRepair.Application.Contracts.Services;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class NotificationsDeletedEvent(int[] ids) : INotification
    {
        public int[] Ids { get; } = ids;
    }

    public class NotificationsDeletedHandler(IEventsService notificationService) : INotificationHandler<NotificationsDeletedEvent>
    {
        public Task Handle(NotificationsDeletedEvent notification, CancellationToken cancellationToken)
        {
            notificationService.DeleteNotifications(notification.Ids);
            return Task.CompletedTask;
        }
    }
}
