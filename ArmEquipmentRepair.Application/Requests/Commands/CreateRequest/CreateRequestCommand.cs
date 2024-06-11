using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.CreateRequest
{
    public class CreateRequestCommand : IRequest
    {
        public RequestEnt Request { get; set; }
        public ClientEnt Client { get; set; }
    }
}
