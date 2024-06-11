using ArmEquipmentRepair.Domain.Entities.Identity;
using MediatR;

namespace ArmEquipmentRepair.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public CommentEnt CommentEnt { get; set; }
    }
}
