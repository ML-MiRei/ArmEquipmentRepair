using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.SetExecutor
{
    internal class SetExecutorHandler(IRequestsRepository requestsRepository) : IRequestHandler<SetExecutorCommand>
    {
        public Task Handle(SetExecutorCommand request, CancellationToken cancellationToken)
        {
            return requestsRepository.SetExecutor(request.RequestId, request.EmployeeId); ;
        }
    }
}
