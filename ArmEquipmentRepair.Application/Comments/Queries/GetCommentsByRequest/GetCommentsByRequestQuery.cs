using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Queries.GetCommentsByRequest
{
    public class GetCommentsByRequestQuery : IRequest<List<CommentDto>>
    {
        public int RequestId { get; set; }
    }
}
