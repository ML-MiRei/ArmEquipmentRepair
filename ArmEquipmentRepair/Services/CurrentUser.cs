using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.UI.Services
{
    public class CurrentUser : IUser
    {
        public EmployeeEnt Employee { get; set; }
    }
}
