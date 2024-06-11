using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Queries.GetCountNotifications
{
    public class GetCountNotificationsQuery : IRequest<int>
    {
        public int UserId { get; set; }
    }
}
