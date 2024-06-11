using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.Application.Contracts.Services
{
    public interface IEventsService
    {
        void NewRequest(RequestEnt request);
        void DeleteRequest(int requestId);
        void UpdateRequest(RequestEnt request);
        void NewComment(CommentDto comment, int requestID);
        void DeleteComment(int commentId, int requestId);
        void DeleteNotifications(int[] ids);

        event EventHandler<RequestEnt> RequestCreated;
        event EventHandler<int> RequestDeleted;
        event EventHandler<RequestEnt> RequestUpdated;

        //comment, requestId
        event EventHandler<(CommentDto, int)> CommentCreated;

        //requestId, commentId
        event EventHandler<(int, int)> CommentDeleted;
        event EventHandler<int[]> NotificationsDeleted;

    }
}
