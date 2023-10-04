using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Domain.Interfaces;

public interface IMovimentacoesVeiculoRepository
{
    Task InserirMovimentacaoVeiculo(MovimentacoesVeiculo movimentacoesVeiculo);
    Task<IEnumerable<MovimentacoesVeiculo>> ObterEventosVeiculo(Guid veiculoId);
}