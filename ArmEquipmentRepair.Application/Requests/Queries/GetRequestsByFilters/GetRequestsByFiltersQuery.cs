using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetRequestsByFilters
{
    public class GetRequestsByFiltersQuery : IRequest<IEnumerable<RequestEnt>>
    {
        public FiltersDto FiltersDto { get; set; }
    }
}
