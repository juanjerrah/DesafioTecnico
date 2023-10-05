using Locadora.Api.Application.ViewModels;

namespace Locadora.Api.Application.Interfaces;

public interface IVeiculoAppService
{
    /// <summary>
    ///     Obtém todos os veículos da base
    /// </summary>
    /// <returns>Os veículos da base</returns>
    Task<IEnumerable<VeiculoResponse>> ObterVeiculos(VeiculoFiltroRequest filtroRequest);
    
    /// <summary>
    ///     Obtém veículo pela placa informada
    /// </summary>
    /// <param name="placa">Placa do veículo que deseja consultar</param>
    /// <returns>Veículo com a placa correspondente</returns>
    Task<VeiculoResponse> ObterVeiculoPorPlaca(string placa);
    Task<VeiculoResponse> InserirVeiculo(InserirVeiculoRequest veiculoRequest);
    Task<VeiculoResponse> AtualizarVeiculo(AtualizarVeiculoRequest veiculoRequest);
    Task<bool> ExcluirVeiculo(string placa);
}