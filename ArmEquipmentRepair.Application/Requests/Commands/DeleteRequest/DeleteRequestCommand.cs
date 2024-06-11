using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.DeleteRequest
{
    public class DeleteRequestCommand : IRequest
    {
        public int RequestId { get; set; }
    }
}
