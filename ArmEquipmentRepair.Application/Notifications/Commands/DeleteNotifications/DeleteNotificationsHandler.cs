using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Commands.DeleteNotifications
{
    internal class DeleteNotificationsHandler(INotificationsRepository notificationsRepository) : IRequestHandler<DeleteNotificationsCommand>
    {
        public Task Handle(DeleteNotificationsCommand request, CancellationToken cancellationToken)
        {
            return notificationsRepository.DeleteNotifications(request.NotificationsIds);
        }
    }
}
