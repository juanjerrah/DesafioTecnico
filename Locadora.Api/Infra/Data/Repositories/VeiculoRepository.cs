using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Repositories;

namespace Locadora.Api.Infra.Data.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    public Task<IEnumerable<Veiculo>> ObterVeiculos()
    {
        throw new NotImplementedException();
    }

    public Task<Veiculo> ObterVeiculoPorPlaca(string placa)
    {
        throw new NotImplementedException();
    }

    public Task InserirVeiculo(Veiculo veiculo)
    {
        throw new NotImplementedException();
    }

    public Task AtualizarVeiculo(Veiculo veiculo)
    {
        throw new NotImplementedException();
    }

    public Task RemoverVeiculo(Veiculo veiculo)
    {
        throw new NotImplementedException();
    }
}