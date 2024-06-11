using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Notifications.Queries.GetCountNotifications;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.UI.Main.Modules.Common;
using ArmEquipmentRepair.UI.Main.Modules.RequestCreater.View;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views;
using ArmEquipmentRepair.UI.Main.Modules.Statistics.Views;
using ArmEquipmentRepair.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;
using System.Windows.Controls;

namespace ArmEquipmentRepair.UI.Main
{
    public partial class NavigationWindowVM : ObservableObject
    {

        private static IUser _user;
        private static IMediator _mediator;
        private static IEventsService _eventsService;

        private static Dictionary<Page, string> _titles;
        private static NotificationVM _notificationVM;

        

        public NavigationWindowVM(IUser user, IMediator mediator, NotificationVM notificationVM, IEventsService eventsService,
            RequestsReviewerPage requestsReviewerPage, RequestCreaterPage requestCreaterPage, StatisticPage statisticPage)
        {

            _user = user;
            _mediator = mediator;
            _eventsService = eventsService;

            RequestReviewerVM.RequestSelected += OnRequestSelected;

            _titles = new Dictionary<Page, string>
            {
                { requestsReviewerPage, "Заявки" },
                { requestCreaterPage, "Создание заявок"},
                {statisticPage, "Статистика" }
            };


            NotificationsIndicator = mediator.Send(new GetCountNotificationsQuery { UserId = user.Employee.Id }).Result;

            _notificationVM = notificationVM;

            eventsService.RequestDeleted += OnRequestDeleted;
            eventsService.NotificationsDeleted += OnNotificationsDeleted;
        }
        private void OnRequestSelected(object? sender, RequestEnt e)
        {
            CurrentPage = new RequestInfoPage(new RequestInfoVM(_mediator, e, _user, _eventsService));
            TitlePage = "Информация о заявке";
        }

        private void OnNotificationsDeleted(object? sender, int[] e)
        {
            App.Current.Dispatcher.Invoke(() => NotificationsIndicator -= e.Count());
        }

        private void OnRequestDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (CurrentPage.GetType() == typeof(RequestInfoPage))
                {
                    CurrentPage = _titles.Keys.OfType<RequestsReviewerPage>().First();
                }
            });
        }

        public Visibility StatisticsPageVisible => _user.Employee.Role == Domain.Enums.EmployeeRoleEnum.Operator ? Visibility.Visible : Visibility.Collapsed;
        public Visibility CreateRequestPageVisible => _user.Employee.Role == Domain.Enums.EmployeeRoleEnum.Administrator ? Visibility.Visible : Visibility.Collapsed;

        [ObservableProperty]
        private int notificationsIndicator;

        [ObservableProperty]
        private string titlePage;

        [ObservableProperty]
        private Page currentPage;

        [RelayCommand]
        private void ChangePage(Page page)
        {
            if (currentPage?.GetType() == typeof(RequestInfoPage))
            {
                if (((currentPage as RequestInfoPage).DataContext as RequestInfoVM).UpdateMode)
                    ((currentPage as RequestInfoPage).DataContext as RequestInfoVM).UpdateMode = false;
            }

            CurrentPage = page;
            TitlePage = _titles[page];
        }

        [RelayCommand]
        private void ShowNotifications()
        {
            NotificationWindow notificationWindow = new NotificationWindow(_notificationVM);
            notificationWindow.Show();
        }


        [RelayCommand]
        private void ShowProfile()
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.ShowDialog();
        }

    }
}
