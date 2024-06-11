using MediatR;

namespace ArmEquipmentRepair.Application.Requests.Commands.SetExecutor
{
    public class SetExecutorCommand : IRequest
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
    }
}
