using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetStatistic
{
    internal class GetStatisticHandler(IRequestsRepository requestsRepository) : IRequestHandler<GetStatisticQuery, StatisticRequestsDto>
    {
        public Task<StatisticRequestsDto> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
        {
            return requestsRepository.GetStatistic();
        }
    }
}
