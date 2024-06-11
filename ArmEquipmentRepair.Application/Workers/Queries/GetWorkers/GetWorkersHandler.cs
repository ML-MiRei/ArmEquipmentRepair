using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Workers.Queries.GetWorkers
{
    internal class GetWorkersHandler(IWorkersRepository workersRepository) : IRequestHandler<GetWorkersQuery, List<WorkerDto>>
    {
        public Task<List<WorkerDto>> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
        {
            return workersRepository.GetWorkersAsync();
        }
    }
}
