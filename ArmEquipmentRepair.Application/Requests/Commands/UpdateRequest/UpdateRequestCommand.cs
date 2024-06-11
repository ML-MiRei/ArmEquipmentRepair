using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommand : IRequest
    {
        public RequestEnt Request { get; set; }
    }
}
