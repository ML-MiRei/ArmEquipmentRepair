using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Dtos;
using ArmEquipmentRepair.Application.Requests.Queries.GetRequests;
using ArmEquipmentRepair.Application.Requests.Queries.GetRequestsByFilters;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.UI.Common;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views;
using ArmEquipmentRepair.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels
{
    public partial class RequestReviewerVM : ObservableObject
    {
        private static FiltersDto _filters = new FiltersDto();
        private static IMediator _mediator;


        public static event EventHandler<RequestEnt> RequestSelected;


        public RequestReviewerVM(IMediator mediator, IUser user, IEventsService eventsService)
        {
            _mediator = mediator;

            requests = new ObservableCollection<RequestEnt>(mediator.Send(new GetRequestsQuery()).Result);

            eventsService.RequestUpdated += OnRequestUpdated;
            eventsService.RequestCreated += OnRequestCreated;
            eventsService.RequestDeleted += OnRequestDeleted;
        }

        private void OnRequestDeleted(object? sender, int e)
        {
            App.Current.Dispatcher.Invoke(() => Requests.Remove(Requests.First(r => r.Id == e)));
        }

        private void OnRequestCreated(object? sender, RequestEnt e)
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                Requests.Add(e);
            });
        }

        private void OnRequestUpdated(object? sender, RequestEnt request)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var oldRequest = Requests.First(r => r.Id == request.Id);
                Requests[Requests.IndexOf(oldRequest)] = request;
            });
        }


        [ObservableProperty]
        private ObservableCollection<RequestEnt> requests;


        [RelayCommand]
        private void SetFilters()
        {
            FiltersWindow filtersWindow = new FiltersWindow(_filters);
            if (filtersWindow.ShowDialog().Value)
            {
                Requests = new ObservableCollection<RequestEnt>(_mediator.Send(new GetRequestsByFiltersQuery { FiltersDto = _filters }).Result);

                if (Requests.Count == 0)
                {
                    MessageBox.Show("Заявки не найдены");
                    Requests = new ObservableCollection<RequestEnt>(_mediator.Send(new GetRequestsQuery()).Result);
                }
                OnPropertyChanged(nameof(Requests));

            }

        }

        public void ShowRequestInfo(RequestEnt request)
        {
            RequestSelected?.Invoke(this, request);
        }

    }
}
