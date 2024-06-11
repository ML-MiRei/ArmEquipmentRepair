using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.Application.Contracts.Services
{
    public interface IAuthorizationService
    {
        Task<EmployeeEnt> AuthorizeAsync(LoginDto loginDto);
        Task<EmployeeEnt> RegisterAsync(EmployeeEnt employee);
    }
}
