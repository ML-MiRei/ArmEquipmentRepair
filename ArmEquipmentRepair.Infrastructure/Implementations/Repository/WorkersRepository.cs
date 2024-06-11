using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;

namespace ArmEquipmentRepair.Infrastructure.Implementations.Repository
{
    public class WorkersRepository(MyDbContext db) : IWorkersRepository
    {
        public Task<List<WorkerDto>> GetWorkersAsync()
        {
            return Task.FromResult(db.Employees.Where(e => e.RoleId == (int)EmployeeRoleEnum.Worker).Select(e => new WorkerDto
            {
                FormatedPhoneNumber = e.PhoneNumber,
                FullName = e.LastName + " " + e.FirstName + " " + e.SecondName,
                Id = e.Id,
                AmountRequestsNow = db.Requests.Where(r => r.EmployeeId == e.Id && r.StatusId == (int)RequestStatusEnum.InProcess).Count(),
            }).ToList());
        }
    }
}
