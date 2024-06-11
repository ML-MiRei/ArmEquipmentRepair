using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Notifications.Queries.GetCountNotifications
{
    internal class GetCountNotificationsHandler(INotificationsRepository notificationsRepository) : IRequestHandler<GetCountNotificationsQuery, int>
    {
        public Task<int> Handle(GetCountNotificationsQuery request, CancellationToken cancellationToken)
        {
            return notificationsRepository.GetCountNotifications(request.UserId);
        }
    }
}
