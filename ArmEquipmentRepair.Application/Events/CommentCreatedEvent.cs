using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class CommentCreatedEvent : INotification
    {
        public CommentDto CommentDto { get; set; }
        public int RequestId { get; set; }
    }

    public class CommentCreatedEventHandler(IEventsService eventsService) : INotificationHandler<CommentCreatedEvent>
    {
        public Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
        {
            eventsService.NewComment(notification.CommentDto, notification.RequestId);
            return Task.CompletedTask;
        }
    }
}
