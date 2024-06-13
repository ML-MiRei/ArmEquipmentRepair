using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Application.Extansions;
using ArmEquipmentRepair.Infrastructure.Extansions;
using ArmEquipmentRepair.UI.Login.View;
using ArmEquipmentRepair.UI.Login.ViewModel;
using ArmEquipmentRepair.UI.Main;
using ArmEquipmentRepair.UI.Main.Modules.RequestCreater.View;
using ArmEquipmentRepair.UI.Main.Modules.RequestCreater.ViewModel;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.RequestsReviewer.Views;
using ArmEquipmentRepair.UI.Main.Modules.Statistics.ViewModels;
using ArmEquipmentRepair.UI.Main.Modules.Statistics.Views;
using ArmEquipmentRepair.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ArmEquipmentRepair.UI
{
    class Programm
    {

        private static IHost host;

        [STAThread]
        public static void Main()
        {

            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString))
            {
                MessageBox.Show("Ошибка подключения к базе данных. Проверьте наличие строки подключения в файле конфигурации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return;
            }


            host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddMediatr();
                services.AddEvents();
                services.AddInfrastructure(System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString);

                services.AddSingleton<IEventsService, EventsService>();
                services.AddSingleton<IUser, CurrentUser>();

                services.AddSingleton<App>();

                services.AddSingleton<TaskManager>();
                services.AddSingleton<SigninPageVM>();
                services.AddSingleton<SignupPageVM>();
                services.AddSingleton<SigninPage>();
                services.AddSingleton<SignupPage>();
                services.AddSingleton<LoginWindow>();

                services.AddSingleton<StatisticVM>();
                services.AddSingleton<StatisticPage>();
                services.AddSingleton<RequestReviewerVM>();
                services.AddSingleton<RequestsReviewerPage>();
                services.AddSingleton<RequestCreaterPageVM>();
                services.AddSingleton<RequestCreaterPage>();
                services.AddSingleton<NotificationVM>();
                services.AddSingleton<NavigationWindowVM>();
                services.AddSingleton<NavigationWindow>();

            }).Build();

            if (UserSettings.Default.Login == String.Empty)
            {
                var loginWindow = host.Services.GetRequiredService<LoginWindow>();
                if (!loginWindow.ShowDialog().Value)
                    return;
            }
            else
            {
                host.Services.GetRequiredService<IUser>().Employee = host.Services.GetRequiredService<IAuthorizationService>().AuthorizeAsync(new Application.Dtos.LoginDto
                {
                    Login = UserSettings.Default.Login,
                    Password = UserSettings.Default.Password
                }).Result;
            }

            var app = host.Services.GetRequiredService<App>();
            app.Run();
        }
    }
}
