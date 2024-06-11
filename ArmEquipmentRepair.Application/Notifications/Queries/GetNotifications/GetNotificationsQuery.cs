using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<List<NotificationEnt>>
    {
        public int UserId { get; set; }
    }
}
