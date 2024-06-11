using ArmEquipmentRepair.Application.Events;
using ArmEquipmentRepair.UI.Login.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArmEquipmentRepair.UI.Login.View
{
    public partial class LoginWindow : Window
    {
        private static SignupPage _signupPage;
        private static SigninPage _signinPage;
        private static Page _currentPage;


        public LoginWindow(SigninPage signinPage, SignupPage signupPage)
        {
            InitializeComponent();

            _signinPage = signinPage;
            _signupPage = signupPage;
            _currentPage = _signinPage;

            PageContainer.NavigationService.Navigate(_currentPage);
            Deactivated += OnDeactivated;
            Activated += OnActivated;

            (signinPage.DataContext as SigninPageVM).SigninCompleted += OnAuthorizedCompleted; ;
            (signupPage.DataContext as SignupPageVM).SignupCompleted += OnAuthorizedCompleted; ;
        }

        private void OnAuthorizedCompleted(object? sender, Domain.Entities.Identity.EmployeeEnt e)
        {
            if (MessageBox.Show("Сохранить данные о входе?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UserSettings.Default.Login = e.Login;
                UserSettings.Default.Password = e.Password;
                UserSettings.Default.Save();
            }

           DialogResult = true;
        }

        private void OnActivated(object? sender, EventArgs e)
        {
            DeactivateRectangle.Visibility = Visibility.Collapsed;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void OnDeactivated(object? sender, EventArgs e)
        {
            DeactivateRectangle.Visibility = Visibility.Visible;
        }

        private void ToAnotherPage(object sender, RoutedEventArgs e)
        {
            if (Equals(_currentPage, _signinPage))
            {
                _currentPage = _signupPage;
                TitlePage.Text = "Регистрация";
                TitlePage.FontSize = 30;
            }
            else
            {
                _currentPage = _signinPage;
                TitlePage.Text = "Вход";
                TitlePage.FontSize = 45;
            }
            PageContainer.NavigationService.Navigate(_currentPage);
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
