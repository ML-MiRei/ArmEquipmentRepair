using ArmEquipmentRepair.Domain.Entities.Identity;

namespace ArmEquipmentRepair.Application.Contracts.Repository
{
    public interface INotificationsRepository
    {
        Task<List<NotificationEnt>> GetNotifications(int userId);
        Task<int> GetCountNotifications(int userId);
        Task DeleteNotifications(int[] ids);
        Task CreateNoification(string text, int recipientId);
        Task CreateNoification(string text, int[] recipientsIds);

    }
}
