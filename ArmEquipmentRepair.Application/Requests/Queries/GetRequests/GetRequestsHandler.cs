using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetRequests
{
    internal class GetRequestsHandler(IRequestsRepository requestsRepository) : IRequestHandler<GetRequestsQuery, IEnumerable<RequestEnt>>
    {
        public Task<IEnumerable<RequestEnt>> Handle(GetRequestsQuery request, CancellationToken cancellationToken)
        {
            return requestsRepository.GetAllAsync();
        }
    }
}
