using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Exceptions;
using ArmEquipmentRepair.UI.Common;
using ArmEquipmentRepair.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace ArmEquipmentRepair.UI.Login.ViewModel
{
    public partial class SigninPageVM(IAuthorizationService authorizationService) : ObservableObject
    {
        [ObservableProperty]
        private string login;
        [ObservableProperty]
        private string password;

        public event EventHandler<EmployeeEnt> SigninCompleted;

        [RelayCommand]
        private void Signin()
        {
   
            EmployeeEnt employee = null;

            TaskManager.WaitTaskWithLoadingWindow (() =>
            {
                    employee = authorizationService.AuthorizeAsync(new Application.Dtos.LoginDto { Login = login, Password = password }).Result;
            });

            

            if(employee != null) 
                SigninCompleted?.Invoke(this, employee);

        }
    }
}
