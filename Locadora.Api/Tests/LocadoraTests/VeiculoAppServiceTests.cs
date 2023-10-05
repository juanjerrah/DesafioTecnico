using Locadora.Api.Application.Interfaces;
using Locadora.Api.Domain.Interfaces;
using Moq;
using Xunit;

namespace Locadora.Api.Tests.LocadoraTests;

[Collection(nameof(VeiculoAppServiceCollection))]
public class VeiculoAppServiceTests
{
    private readonly IVeiculoAppService _appService;
    private readonly VeiculoAppServiceFixture _fixture;
    private readonly VeiculoAppServiceFactory _factory;

    public VeiculoAppServiceTests(VeiculoAppServiceFixture fixture)
    {
        _fixture = fixture;
        _appService = _fixture.GetVeiculoAppService();
        _factory = new VeiculoAppServiceFactory();
    }

    [Fact]
    public async Task Xyz()
    {
        //Arrange
        var veiculos = new[]
        {
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo()
        };
        //Setup
        _fixture.SetupObterVeiculos(veiculos);
        //Act
        var result = await _appService.ObterVeiculos();
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.ObterVeiculos(),
                Times.Once);
        
        Assert.NotNull(result);
        Assert.Equal(veiculos.Length, result.Count());
    }
}