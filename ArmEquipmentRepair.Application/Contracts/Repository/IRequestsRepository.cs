using ArmEquipmentRepair.Application.Contracts.Repository.Common;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;

namespace ArmEquipmentRepair.Application.Contracts.Repository
{
    public interface IRequestsRepository : IBaseRepositoryQueries<RequestEnt>
    {
        Task<RequestEnt> CreateAsync(RequestEnt requestEnt, ClientEnt clientEnt);
        Task DeleteAsync(int entityId);
        Task UpdateAsync(RequestEnt entity);
        Task<RequestFullInfoDto> GetInfoByRequestId(int id);
        Task<StatisticRequestsDto> GetStatistic();
        Task<IEnumerable<RequestEnt>> GetRequestsByFilters(FiltersDto filters);
        Task SetExecutor(int requestId, int employeeId);
        Task ChangeStatus(int requestId, RequestStatusEnum status);
    }
}
