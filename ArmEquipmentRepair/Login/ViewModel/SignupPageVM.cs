using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using ArmEquipmentRepair.Domain.Exceptions;
using ArmEquipmentRepair.UI.Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ArmEquipmentRepair.UI.Login.ViewModel
{
    public partial class SignupPageVM(IAuthorizationService authorizationService) : ObservableObject
    {
        public event EventHandler<EmployeeEnt> SignupCompleted;

        [ObservableProperty]
        private List<EmployeeRoleEnum> roles = Enum.GetValues(typeof(EmployeeRoleEnum)).Cast<EmployeeRoleEnum>().ToList();

        [ObservableProperty]
        private EmployeeRoleEnum role = EmployeeRoleEnum.Operator;

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string secondName;

        [ObservableProperty]
        private string phoneNumber;

        [RelayCommand]
        private void Signup()
        {
            EmployeeEnt employee = new EmployeeEnt
            {
                Login = login,
                Password = password,
                FirstName = firstName,
                SecondName = secondName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Role = (EmployeeRoleEnum)role
            };

            if (!IsValide(employee))
                return;

            EmployeeEnt newEmployee = null;

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            var task = Task.Run(() =>
            {
                try
                {
                    newEmployee = authorizationService.RegisterAsync(employee).Result;
                }
                catch (LoginExistException)
                {
                    MessageBox.Show("Введённый логин занят. Введите другой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });


            Task.WaitAll(task);

            if(newEmployee != null)
                SignupCompleted?.Invoke(this, newEmployee);

            loadingWindow.Close();
        }

        private bool IsValide(EmployeeEnt employee)
        {
            var context = new ValidationContext(employee);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(employee, context, results))
            {
                string messages = string.Join('\n', results.Select(r => r.ErrorMessage));
                MessageBox.Show("Данные введены неверно\n" + messages, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
