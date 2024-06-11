using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Workers.Queries.GetWorkers
{
    public class GetWorkersQuery : IRequest<List<WorkerDto>>
    {
    }
}
