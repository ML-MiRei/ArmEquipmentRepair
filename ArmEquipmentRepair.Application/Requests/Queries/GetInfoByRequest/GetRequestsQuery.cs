using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Queries.GetInfoByRequest
{
    public class GetInfoByRequestQuery : IRequest<RequestFullInfoDto>
    {
        public int RequestId { get; set; }
    }
}
