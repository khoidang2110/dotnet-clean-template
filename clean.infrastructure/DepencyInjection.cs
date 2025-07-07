using galaxy_pay.application.Contracts;
using galaxy_pay.infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Đăng ký các service hạ tầng ở đây (nếu có)
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
