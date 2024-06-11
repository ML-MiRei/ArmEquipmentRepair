using ArmEquipmentRepair.Application.Contracts.Repository.Common;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.Application.Contracts.Repository
{
    public interface ICommentsRepository : IBaseRepositoryCommands<CommentEnt>
    {
        Task<List<CommentDto>> GetCommentsByRequest(int requestId);
    }
}
