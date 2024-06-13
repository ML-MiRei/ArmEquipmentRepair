using ArmEquipmentRepair.Application.Contracts.Repository;
using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Infrastructure.Implementations.Repository;
using ArmEquipmentRepair.Infrastructure.Implementations.Services;
using ArmEquipmentRepair.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ArmEquipmentRepair.Infrastructure.Extansions
{
    public static class ExtansionsService
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRequestsRepository, RequestsRepository>();
            services.AddTransient<INotificationsRepository, NotificationsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<IWorkersRepository, WorkersRepository>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddSingleton(c => new MyDbContext(connectionString));

            return services;
        }
    }
}
