using ArmEquipmentRepair.Domain.Entities.Common;

namespace ArmEquipmentRepair.Application.Contracts.Repository.Common
{
    public interface IBaseRepositoryCommands<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(int entityId);
        Task UpdateAsync(T entity);
    }
}
