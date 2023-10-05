using System.Linq.Expressions;
using Locadora.Api.Application.AutoMapper;
using Locadora.Api.Application.Services;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Locadora.Api.UnitTests.LocadoraTests;

[CollectionDefinition(nameof(VeiculoAppServiceCollection))]
public class VeiculoAppServiceCollection : ICollectionFixture<VeiculoAppServiceFixture>{ }
public class VeiculoAppServiceFixture
{
    private VeiculoAppService VeiculoAppService { get; set; }
    public AutoMocker Mocker { get; set; }

    public VeiculoAppService GetVeiculoAppService()
    {
        Mocker = new AutoMocker();

        Mocker.Use(AutoMapperConfiguration.RegisterMappings().CreateMapper());
        
        VeiculoAppService = Mocker.CreateInstance<VeiculoAppService>();

        return VeiculoAppService;
    }

    public void SetupObterVeiculos(IEnumerable<Veiculo> veiculos)
    {
        Mocker.GetMock<IVeiculoRepository>()
            .Setup(x => x.ObterVeiculos(
                It.IsAny<Expression<Func<Veiculo, bool>>>()))
            .ReturnsAsync((Expression<Func<Veiculo, bool>> predicate) 
                => veiculos.Where(predicate.Compile()));
    }
    
    public void SetupObterVeiculoPorPlaca(IEnumerable<Veiculo> veiculos)
    {
        Mocker.GetMock<IVeiculoRepository>()
            .Setup(x => x.ObterVeiculoPorPlaca(It.IsAny<string>()))
            .ReturnsAsync((string placa) =>
                veiculos.FirstOrDefault(x => x.Placa.ToLower().Equals(placa.ToLower())));
    }
}