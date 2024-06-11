using System.Windows;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Login.View
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
        }
    }
}
