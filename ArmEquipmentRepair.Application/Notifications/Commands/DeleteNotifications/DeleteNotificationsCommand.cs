using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Commands.DeleteNotifications
{
    public class DeleteNotificationsCommand : IRequest
    {
        public int[] NotificationsIds { get; set; }
    }
}
