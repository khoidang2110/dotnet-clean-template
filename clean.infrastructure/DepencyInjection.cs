using Microsoft.Extensions.DependencyInjection;

namespace infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Đăng ký các service hạ tầng ở đây (nếu có)
        return services;
    }
}
