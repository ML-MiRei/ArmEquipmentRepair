using ArmEquipmentRepair.Application.Contracts.Repository;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Commands.CreateComment
{
    internal class CreateCommentHandler(ICommentsRepository commentsRepository) : IRequestHandler<CreateCommentCommand>
    {
        public Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            return commentsRepository.CreateAsync(request.CommentEnt);
        }
    }
}
