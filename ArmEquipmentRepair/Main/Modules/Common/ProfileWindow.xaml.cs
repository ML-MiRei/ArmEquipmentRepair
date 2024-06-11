using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.Common
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExitAccount(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из аккаунта ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                UserSettings.Default.Login = String.Empty;
                UserSettings.Default.Password = String.Empty;
                UserSettings.Default.Save();

                App.Current.Shutdown();
            }
        }
    }
}
