using ArmEquipmentRepair.UI.Main;

namespace ArmEquipmentRepair.UI
{
    public class App : System.Windows.Application
    {
        public App(NavigationWindow navigationWindow)
        {
            MainWindow = navigationWindow;
            navigationWindow.Show();
        }
    }
}
