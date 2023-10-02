using Locadora.Api.Domain.Repositories;
using Locadora.Api.Infra.Data.Contexts;
using Locadora.Api.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Infra.IoC;

public abstract class DependencyInjection
{
    public static IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
    {
        //Connection
        services.AddDbContext<LocadoraContext>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        //DI_LifeCycling
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IEventoRepository, EventoRepository>();

        return services;
    }
}