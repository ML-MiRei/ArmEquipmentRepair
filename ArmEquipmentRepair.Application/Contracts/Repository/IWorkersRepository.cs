using ArmEquipmentRepair.Application.Dtos;

namespace ArmEquipmentRepair.Application.Contracts.Repository
{
    public interface IWorkersRepository
    {
        Task<List<WorkerDto>> GetWorkersAsync();
    }
}
