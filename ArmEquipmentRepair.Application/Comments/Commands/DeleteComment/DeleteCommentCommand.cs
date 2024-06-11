using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public int CommentId { get; set; }
    }
}
