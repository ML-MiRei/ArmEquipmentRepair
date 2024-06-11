using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Events;
using ArmEquipmentRepair.Domain.Entities.Identity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels
{
    public partial class NotificationVM : ObservableObject
    {
        private static INotificationsRepository _notificationsRepository;



        [ObservableProperty]
        private ObservableCollection<NotificationEnt> notifications;


        public NotificationVM(IUser user, INotificationsRepository notificationsRepository, IEventsService eventsService)
        {
            _notificationsRepository = notificationsRepository;

            notifications = new ObservableCollection<NotificationEnt>(notificationsRepository.GetNotifications(user.Employee.Id).Result);

            eventsService.NotificationsDeleted += OnNotificationsDeleted;
        }

        private void OnNotificationsDeleted(object? sender, int[] ids)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (int id in ids)
                {
                    Notifications.Remove(Notifications.First(n => n.Id == id));
                }
            });

        }

        [RelayCommand]
        private void ClearAll()
        {
            _notificationsRepository.DeleteNotifications(notifications.Select(n => n.Id).ToArray());
        }

        public void DeleteNotification(int id)
        {
            if (MessageBox.Show("Удалить?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _notificationsRepository.DeleteNotifications([id]);
            }
        }

    }
}
