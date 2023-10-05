using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.ViewModels;
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

    [Fact(DisplayName = "ObterVeiculo - Obtém Todos os Veiculos - Sucesso")]
    public async Task ObterVeiculo_ObtemTodosVeiculos_Sucesso()
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

    [Theory(DisplayName = "Obter Veiculo Por Placa - Obtem Veiculo Por Placa - Sucesso")]
    [InlineData("nme5046")]
    [InlineData("nme5a46")]
    public async Task ObterVeiculoPorPlaca_ObtemVeiculoPorPlaca_Sucesso(string placaVeiculo)
    {
        //Arrange
        var placa = placaVeiculo;
        
        var veiculos = new[]
        {
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo(placa: placa),
            _factory.GerarVeiculo()
        };
        //Setup
        _fixture.SetupObterVeiculoPorPlaca(veiculos);
        //Act
        var result = await _appService.ObterVeiculoPorPlaca(placa);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.ObterVeiculoPorPlaca(
                    It.Is<string>(y => y.ToLower().Equals(placa.ToLower()))),
                Times.Once);

        Assert.NotNull(result);
        Assert.Equal(result.Placa, placa);
    }
    
    [Fact(DisplayName = "Obter Veiculo Por Placa - Obtem Veiculo Por Placa - Falha")]
    public async Task ObterVeiculoPorPlaca_ObtemVeiculoPorPlaca_Falha()
    {
        //Arrange
        var placa = "xyz4321";
        var veiculos = new[]
        {
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo()
        };
        //Setup
        _fixture.SetupObterVeiculoPorPlaca(veiculos);
        //Act
        var result = await _appService.ObterVeiculoPorPlaca(placa);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.ObterVeiculoPorPlaca(
                    It.Is<string>(y => y.ToLower().Equals(placa.ToLower()))),
                Times.Once);
        _fixture.Mocker.GetMock<IMessageBus>()
            .Verify(x => x.RaiseValidationError($"Veículo de placa {placa} não consta na base",
                    StatusCodes.Status400BadRequest),
                Times.Once);
        Assert.IsAssignableFrom<VeiculoResponse>(result);
    }
}