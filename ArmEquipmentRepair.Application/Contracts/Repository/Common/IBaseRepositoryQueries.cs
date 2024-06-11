using ArmEquipmentRepair.Domain.Entities.Common;

namespace ArmEquipmentRepair.Application.Contracts.Repository.Common
{
    public interface IBaseRepositoryQueries<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
