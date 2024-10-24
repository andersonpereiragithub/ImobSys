using Microsoft.Extensions.DependencyInjection;
using ImobSys.Infrastructure.Repositories;

namespace ImobSys.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IImovelRepository, JsonImovelRepository>();

            return services;
        }
    }
}
