using Locadora.Api.Domain.Repositories;
using Locadora.Api.Infra.Data.Repositories;

namespace Locadora.Api.Infra.IoC;

public abstract class DependencyInjection
{
    public static IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IEventoRepository, EventoRepository>();

        return services;
    }
}