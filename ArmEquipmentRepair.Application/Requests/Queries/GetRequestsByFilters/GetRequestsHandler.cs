using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetRequestsByFilters
{
    internal class GetRequestsByFiltersHandler(IRequestsRepository requestsRepository) : IRequestHandler<GetRequestsByFiltersQuery, IEnumerable<RequestEnt>>
    {
        public Task<IEnumerable<RequestEnt>> Handle(GetRequestsByFiltersQuery request, CancellationToken cancellationToken)
        {
            return requestsRepository.GetRequestsByFilters(request.FiltersDto);
        }
    }
}
