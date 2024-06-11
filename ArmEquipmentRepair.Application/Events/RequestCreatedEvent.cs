using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Events
{
    public class RequestCreatedEvent(RequestEnt request) : INotification
    {
        public RequestEnt Request { get; } = request;
    }

    public class RequestCreatedEventHandler(IEventsService notificationService) : INotificationHandler<RequestCreatedEvent>
    {
        public Task Handle(RequestCreatedEvent notification, CancellationToken cancellationToken)
        {
            notificationService.NewRequest(notification.Request);
            return Task.CompletedTask;
        }

    }


}
