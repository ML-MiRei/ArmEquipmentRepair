using ArmEquipmentRepair.Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace ArmEquipmentRepair.Application.Extansions
{
    public static class ExtansionsService
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<NotificationsDeletedHandler>();
            services.AddSingleton<RequestCreatedEventHandler>();
            services.AddSingleton<RequestDeletedEventHandler>();
            services.AddSingleton<RequestUpdatedEventHandler>();
            return services;
        }

        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR((cnf) =>
            {
                cnf.RegisterServicesFromAssemblyContaining(typeof(ExtansionsService));
            });
            return services;
        }
    }
}
