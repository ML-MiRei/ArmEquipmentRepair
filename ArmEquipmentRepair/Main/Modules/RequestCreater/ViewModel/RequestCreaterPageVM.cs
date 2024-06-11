using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Requests.Commands.CreateRequest;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Windows;

namespace ArmEquipmentRepair.UI.Main.Modules.RequestCreater.ViewModel
{
    public partial class RequestCreaterPageVM(IMediator mediator, IEventsService eventsService) : ObservableObject
    {

        [ObservableProperty]
        private string equipment;
        public List<TypeFaultEnum> TypeFaults => Enum.GetValues(typeof(TypeFaultEnum)).Cast<TypeFaultEnum>().ToList();
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private TypeFaultEnum typeFault;
        [ObservableProperty]
        private string firstName;
        [ObservableProperty]
        private string secondName;
        [ObservableProperty]
        private string lastName;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string phoneNumber;


        [RelayCommand]
        private async Task CreateRequestAsync()
        {
            if (MessageBox.Show("Вы уверены, что хотите оформить заявку?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                var request = new RequestEnt
                {
                    CreatedDate = DateTime.Now,
                    Description = description,
                    Status = RequestStatusEnum.Waiting,
                    TypeFault = typeFault,
                    Equipment = equipment
                };

                if (!ValidationService.IsValide(request)) return;

                var client = new ClientEnt
                {
                    FirstName = firstName,
                    SecondName = secondName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = Email
                };

                if(!ValidationService.IsValide(client)) return;

                TaskManager.AddTask(async () =>
                {
                    await mediator.Send(new CreateRequestCommand
                    {
                        Request = request,
                        Client = client
                    });
                    MessageBox.Show("Заявка создана");
                });

                ClearData();
            }
        }

        


        [RelayCommand]
        private void ClearData()
        {
            Equipment = String.Empty;
            Description = String.Empty;
            TypeFault = TypeFaultEnum.DoesntTurnOn;
            FirstName = String.Empty;
            LastName = String.Empty;
            SecondName = String.Empty;
            PhoneNumber = String.Empty;
            Email = String.Empty;
        }
    }
}
