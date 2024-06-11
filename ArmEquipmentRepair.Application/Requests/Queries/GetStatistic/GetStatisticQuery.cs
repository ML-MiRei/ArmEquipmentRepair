using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetStatistic
{
    public class GetStatisticQuery : IRequest<StatisticRequestsDto>
    {
    }
}
