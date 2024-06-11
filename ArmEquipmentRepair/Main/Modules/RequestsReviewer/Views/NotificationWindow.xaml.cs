using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views.Templates;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views
{
    public partial class NotificationWindow : Window
    {
        private static NotificationVM _notificationVM;

        public NotificationWindow(NotificationVM notificationVM)
        {
            InitializeComponent();
            DataContext = notificationVM;

            _notificationVM = notificationVM;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NotificationTemplate_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _notificationVM.DeleteNotification(((sender as NotificationTemplate).DataContext as NotificationEnt).Id);
        }
    }
}
