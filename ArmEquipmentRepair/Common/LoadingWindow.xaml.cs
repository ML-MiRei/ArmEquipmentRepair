using System.Windows;
using System.Windows.Input;

namespace ArmEquipmentRepair.UI.Common
{
    /// <summary>
    /// Логика взаимодействия для LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            Mouse.OverrideCursor = Cursors.Wait;

            if (App.Current != null)
            {
                Width = App.Current.Windows[App.Current.Windows.Count - 1].Width;
                Height = App.Current.Windows[App.Current.Windows.Count - 1].Height;
            }

        }

    }
}
