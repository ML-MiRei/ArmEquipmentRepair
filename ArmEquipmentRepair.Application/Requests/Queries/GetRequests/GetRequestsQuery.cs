using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetRequests
{
    public class GetRequestsQuery : IRequest<IEnumerable<RequestEnt>>
    {
    }
}
