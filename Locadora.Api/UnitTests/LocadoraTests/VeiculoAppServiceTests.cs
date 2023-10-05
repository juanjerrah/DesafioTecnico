using System.Linq.Expressions;
using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.ViewModels;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;
using Locadora.Api.Domain.Interfaces;
using Moq;
using Xunit;

namespace Locadora.Api.UnitTests.LocadoraTests;

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
        var veiculoId = Guid.NewGuid();
        var placa = "qwe3e87";
        var tipoVeiculo = ETiposVeiculos.Suv;
        var statusVeiculo = EStatusVeiculo.Disponivel;
        
        var veiculos = new[]
        {
            _factory.GerarVeiculo(),
            _factory.GerarVeiculo(veiculoId, placa, tipoVeiculo, statusVeiculo),
            _factory.GerarVeiculo()
        };

        var filtro = _factory.GerarVeiculoFiltroRequest(veiculoId, placa, statusVeiculo, tipoVeiculo);
        
        //Setup
        _fixture.SetupObterVeiculos(veiculos);
        
        //Act
        var result = await _appService.ObterVeiculos(filtro);
        
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.ObterVeiculos(
                    It.IsAny<Expression<Func<Veiculo,bool>>>()),
                Times.Once);
        
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(placa, result.First().Placa);
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

    [Fact]
    public async Task InserirVeiculo_InsereOVeiculoNaBase_Sucesso()
    {
        //Arrange
        var placa = "iaa1324";
        var placaMercosul = "iaa1d24";
        var tipoVeiculo = ETiposVeiculos.Suv;
        var statusVeiculo = EStatusVeiculo.Disponivel;
        
        var veiculoRequest = _factory.GerarInserirVeiculoRequest(placa, tipoVeiculo, statusVeiculo);

        var movimentacaoVeiculo = _factory.GerarMovimentacoesVeiculo();
        //Setup
        //Act
        var result = await _appService.InserirVeiculo(veiculoRequest);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.InserirVeiculo(
                    It.IsAny<Veiculo>()), 
                Times.Once);
        _fixture.Mocker.GetMock<IMovimentacoesVeiculoRepository>()
            .Verify(x => x.InserirMovimentacaoVeiculo(
                    It.IsAny<MovimentacoesVeiculo>()), 
                Times.Once);
        
        Assert.Equal(result.Placa.ToLower(), placaMercosul.ToLower());
    }
    
    [Theory]
    [InlineData(ETiposVeiculos.Suv, null)]
    [InlineData(null, EStatusVeiculo.Disponivel)]
    public async Task InserirVeiculo_StatusETipoDoVeiculoNaoInformado_Falha(ETiposVeiculos? tiposVeiculos, 
        EStatusVeiculo? estatusVeiculo)
    {
        //Arrange
        var placa = "iaa1324";
        var placaMercosul = "iaa1d24";
        var tipoVeiculo = tiposVeiculos;
        var statusVeiculo = estatusVeiculo;
        
        var veiculoRequest = _factory.GerarInserirVeiculoRequest(placa, tipoVeiculo, statusVeiculo);

        var movimentacaoVeiculo = _factory.GerarMovimentacoesVeiculo();
        //Setup
        //Act
        var result = await _appService.InserirVeiculo(veiculoRequest);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.InserirVeiculo(
                    It.IsAny<Veiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMovimentacoesVeiculoRepository>()
            .Verify(x => x.InserirMovimentacaoVeiculo(
                    It.IsAny<MovimentacoesVeiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMessageBus>()
            .Verify(x => x.RaiseValidationError("Tipo e status do veículo são obrigatórios", 
                StatusCodes.Status400BadRequest), 
                Times.Once);
        
        Assert.IsAssignableFrom<VeiculoResponse>(result);
    }
    
    [Fact]
    public async Task InserirVeiculo_PlacaNaoInformada_Falha()
    {
        //Arrange
        
        var placa = "iaa1k24";
        var tipoVeiculo = ETiposVeiculos.Suv;
        var statusVeiculo = EStatusVeiculo.Disponivel;
        
        var veiculoRequest = _factory.GerarInserirVeiculoRequest(placa, tiposVeiculos: tipoVeiculo, 
            statusVeiculo:statusVeiculo);

        var movimentacaoVeiculo = _factory.GerarMovimentacoesVeiculo();
        
        //Setup
        //Act
        var result = await _appService.InserirVeiculo(veiculoRequest);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.InserirVeiculo(
                    It.IsAny<Veiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMovimentacoesVeiculoRepository>()
            .Verify(x => x.InserirMovimentacaoVeiculo(
                    It.IsAny<MovimentacoesVeiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMessageBus>()
            .Verify(x => x.RaiseValidationError($"A placa {veiculoRequest.Placa} é inválida." +
                                                $" Formatos válidos: XXX0000 ou XXX0X00"
                    , StatusCodes.Status400BadRequest), 
                Times.Once);
        
        Assert.IsAssignableFrom<VeiculoResponse>(result);
    }
    
    [Fact]
    public async Task InserirVeiculo_PlacaInvalida_Falha()
    {
        //Arrange
        var tipoVeiculo = ETiposVeiculos.Suv;
        var statusVeiculo = EStatusVeiculo.Disponivel;
        
        var veiculoRequest = _factory.GerarInserirVeiculoRequest(string.Empty, tiposVeiculos: tipoVeiculo, 
            statusVeiculo:statusVeiculo);

        var movimentacaoVeiculo = _factory.GerarMovimentacoesVeiculo();
        
        //Setup
        //Act
        var result = await _appService.InserirVeiculo(veiculoRequest);
        //Assert
        _fixture.Mocker.GetMock<IVeiculoRepository>()
            .Verify(x => x.InserirVeiculo(
                    It.IsAny<Veiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMovimentacoesVeiculoRepository>()
            .Verify(x => x.InserirMovimentacaoVeiculo(
                    It.IsAny<MovimentacoesVeiculo>()), 
                Times.Never);
        _fixture.Mocker.GetMock<IMessageBus>()
            .Verify(x => x.RaiseValidationError("Placa é um campo obrigatório", 
                    StatusCodes.Status400BadRequest), 
                Times.Once);
        
        Assert.IsAssignableFrom<VeiculoResponse>(result);
    }
}