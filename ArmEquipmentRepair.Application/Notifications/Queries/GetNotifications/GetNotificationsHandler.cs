using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Queries.GetNotifications
{
    internal class GetNotificationsHandler(INotificationsRepository notificationsRepository) : IRequestHandler<GetNotificationsQuery, List<NotificationEnt>>
    {
        public Task<List<NotificationEnt>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            return notificationsRepository.GetNotifications(request.UserId);
        }
    }
}
