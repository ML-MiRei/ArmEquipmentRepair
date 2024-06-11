using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Events;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;
using MediatR;

namespace ArmEquipmentRepair.Infrastructure.Implementations.Repository
{
    public class NotificationsRepository(MyDbContext db, IMediator mediator, IUser user) : INotificationsRepository
    {
        public Task DeleteNotifications(int[] ids)
        {
            db.RemoveRange(db.Notifications.Where(n => ids.Contains(n.Id)));
            db.SaveChanges();

            mediator.Publish(new NotificationsDeletedEvent(ids));

            return Task.CompletedTask;
        }

        public Task<int> GetCountNotifications(int userId)
        {
            return Task.FromResult(db.Notifications.Where(n => n.EmployeeId == userId).Count());
        }

        public Task<List<NotificationEnt>> GetNotifications(int userId)
        {
            return Task.FromResult(db.Notifications.Where(n => n.EmployeeId == userId).Select(n => new NotificationEnt
            {
                CreatedDate = n.CreatedDate.Value,
                Id = n.Id,
                Text = n.Text
            }).ToList());
        }


        public Task CreateNoification(string text, int recipientId)
        {
            var notification = new Persistence.Model.Notification
            {
                CreatedDate = DateTime.Now,
                Text = text,
                EmployeeId = recipientId
            };

            db.Notifications.Add(notification);
            db.SaveChanges();

            return Task.CompletedTask;
        }

        public Task CreateNoification(string text, int[] recipientsIds)
        {
            foreach (var recipienId in recipientsIds)
            {
                if (recipienId == user.Employee.Id)
                    continue;

                var notification = new Persistence.Model.Notification
                {
                    CreatedDate = DateTime.Now,
                    Text = text,
                    EmployeeId = recipienId
                };

                db.Notifications.Add(notification);
                db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
