using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetInfoByRequest
{
    internal class GetInfoByRequestHandler(IRequestsRepository requestsRepository) : IRequestHandler<GetInfoByRequestQuery, RequestFullInfoDto>
    {
        public Task<RequestFullInfoDto> Handle(GetInfoByRequestQuery request, CancellationToken cancellationToken)
        {
            return requestsRepository.GetInfoByRequestId(request.RequestId);
        }
    }
}
