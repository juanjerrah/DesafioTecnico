using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;

namespace Locadora.Api.Infra.Data.Repositories;

public class MovimentacoesVeiculoRepository : IMovimentacoesVeiculoRepository
{
    public Task InserirMovimentacaoVeiculo(MovimentacoesVeiculo movimentacoesVeiculo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MovimentacoesVeiculo>> ObterEventosVeiculo(Guid veiculoId)
    {
        throw new NotImplementedException();
    }
}