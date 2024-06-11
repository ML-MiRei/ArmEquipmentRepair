using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.DeleteRequest
{
    internal class DeleteRequestHandler(IRequestsRepository requestsRepository) : IRequestHandler<DeleteRequestCommand>
    {
        public Task Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            return requestsRepository.DeleteAsync(request.RequestId);
        }
    }
}
