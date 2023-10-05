using Locadora.Api.Application.AutoMapper;
using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.Services;
using Locadora.Api.Domain.Bus;
using Locadora.Api.Domain.Interfaces;
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
        services.AddScoped<IMessageBus, MessageBus>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IVeiculoAppService, VeiculoAppService>();
        services.AddScoped<IMovimentacoesVeiculoRepository, MovimentacoesVeiculoRepository>();
        services.AddScoped<IMovimentacoesAppService, MovimentacaoAppService>();
        
        //AutoMapper injection
        var mapper = AutoMapperConfiguration.RegisterMappings().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}