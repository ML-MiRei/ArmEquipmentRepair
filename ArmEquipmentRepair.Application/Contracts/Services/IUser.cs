using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.Application.Contracts.Services
{
    public interface IUser
    {
        EmployeeEnt Employee { get; set; }
    }
}
