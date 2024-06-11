using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.UI.Services
{
    internal class EventsService : IEventsService
    {

        public event EventHandler<RequestEnt> RequestCreated;
        public event EventHandler<int> RequestDeleted;
        public event EventHandler<RequestEnt> RequestUpdated;

        //comment, requestId
        public event EventHandler<(CommentDto, int)> CommentCreated;

        //requestId, commentId
        public event EventHandler<(int, int)> CommentDeleted;
        public event EventHandler<int[]> NotificationsDeleted;

        void IEventsService.DeleteComment(int commentId, int requestId)
        {
            CommentDeleted?.Invoke(this, (requestId, commentId));
        }

        void IEventsService.DeleteNotifications(int[] ids)
        {
            NotificationsDeleted?.Invoke(this, ids);
        }

        void IEventsService.DeleteRequest(int requestId)
        {
            RequestDeleted?.Invoke(this, requestId);
        }

        void IEventsService.NewComment(CommentDto comment, int requestID)
        {
            CommentCreated?.Invoke(this, (comment, requestID));
        }

        void IEventsService.NewRequest(RequestEnt request)
        {
            RequestCreated?.Invoke(this, request);
        }

        void IEventsService.UpdateRequest(RequestEnt request)
        {
            RequestUpdated?.Invoke(this, request);
        }
    }

}
