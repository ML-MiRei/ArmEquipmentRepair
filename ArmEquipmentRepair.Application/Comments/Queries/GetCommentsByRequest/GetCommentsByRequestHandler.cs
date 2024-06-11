using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Dtos;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Queries.GetCommentsByRequest
{
    internal class GetCommentsByRequestHandler(ICommentsRepository commentsRepository) : IRequestHandler<GetCommentsByRequestQuery, List<CommentDto>>
    {
        public Task<List<CommentDto>> Handle(GetCommentsByRequestQuery request, CancellationToken cancellationToken)
        {
            return commentsRepository.GetCommentsByRequest(request.RequestId);
        }
    }
}
