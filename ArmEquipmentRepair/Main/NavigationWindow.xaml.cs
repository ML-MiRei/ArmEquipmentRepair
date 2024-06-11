using ArmEquipmentRepair.UI.Main.Modules.RequestCreater.View;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views;
using ArmEquipmentRepair.UI.Main.Modules.Statistics.Views;
using ArmEquipmentRepair.UI.Services;
using System.Windows;
using System.Windows.Input;

namespace ArmEquipmentRepair.UI.Main
{
    public partial class NavigationWindow : Window
    {

        public NavigationWindow(RequestsReviewerPage requestsReviewerPage, NavigationWindowVM navigationWindowVM, 
            RequestCreaterPage requestCreaterPage, StatisticPage statisticPage)
        {
            InitializeComponent();
            DataContext = navigationWindowVM;

            ToStatisticsPage.CommandParameter = statisticPage;
            ToRequestsPage.CommandParameter = requestsReviewerPage;
            ToCreaterPage.CommandParameter = requestCreaterPage;

            Deactivated += OnDeactivated;
            Activated += OnActivated;

            navigationWindowVM.ChangePageCommand.Execute(requestsReviewerPage);



        }


        private void OnActivated(object? sender, EventArgs e)
        {
            DeactivatedGrid.Visibility = Visibility.Collapsed;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void OnDeactivated(object? sender, EventArgs e)
        {
            DeactivatedGrid.Visibility = Visibility.Visible;
        }

        private void MinimizedWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            TaskManager.WaitAllTask();
            base.OnClosed(e);
        }
    }
}
