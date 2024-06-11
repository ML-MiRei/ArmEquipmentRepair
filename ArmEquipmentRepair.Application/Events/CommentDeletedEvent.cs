using ArmEquipmentRepair.Application.Contracts.Services;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class CommentDeletedEvent : INotification
    {
        public int CommentId { get; set; }
        public int RequestId { get; set; }
    }

    public class CommentDeletedEventHandler(IEventsService notificationService) : INotificationHandler<CommentDeletedEvent>
    {
        public Task Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
        {
            notificationService.DeleteComment(notification.CommentId, notification.RequestId);
            return Task.CompletedTask;
        }
    }
}
