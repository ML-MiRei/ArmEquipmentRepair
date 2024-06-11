using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class RequestUpdatedEvent(RequestEnt request) : INotification
    {
        public RequestEnt Request { get; } = request;
    }

    public class RequestUpdatedEventHandler(IEventsService notificationService) : INotificationHandler<RequestUpdatedEvent>
    {
        public Task Handle(RequestUpdatedEvent notification, CancellationToken cancellationToken)
        {
            notificationService.UpdateRequest(notification.Request);
            return Task.CompletedTask;
        }
    }
}
