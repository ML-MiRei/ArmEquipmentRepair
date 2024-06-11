using ArmEquipmentRepair.Application.Contracts.Services;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class RequestDeletedEvent(int requestId) : INotification
    {
        public int RequestId { get; } = requestId;
    }

    public class RequestDeletedEventHandler(IEventsService notificationService) : INotificationHandler<RequestDeletedEvent>
    {
        public Task Handle(RequestDeletedEvent notification, CancellationToken cancellationToken)
        {
            notificationService.DeleteRequest(notification.RequestId);
            return Task.CompletedTask;
        }
    }
}
