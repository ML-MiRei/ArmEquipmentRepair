using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Commands.DeleteComment
{
    internal class DeleteCommentHandler(ICommentsRepository commentsRepository) : IRequestHandler<DeleteCommentCommand>
    {
        public Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            return commentsRepository.DeleteAsync(request.CommentId);
        }
    }
}
