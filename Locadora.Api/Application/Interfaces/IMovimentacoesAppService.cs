using Locadora.Api.Application.ViewModels.Movimentacao;
using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Application.Interfaces;

public interface IMovimentacoesAppService
{
    /// <summary>
    ///     Obtem as movimentações de um veículo específico
    /// </summary>
    /// <param name="veiculoId">Id do veículo a consultar</param>
    /// <returns>Todas as movimentações do veículo</returns>
    Task<IEnumerable<MovimentacaoResponse>> ObterMovimentacoesDoVeiculo(Guid veiculoId);
}