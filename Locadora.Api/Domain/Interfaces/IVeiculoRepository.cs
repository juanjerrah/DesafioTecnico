using Locadora.Api.Domain.Entities;

namespace Locadora.Api.Domain.Interfaces;

public interface IVeiculoRepository
{
    Task<IEnumerable<Veiculo>> ObterVeiculos();
    Task<Veiculo?> ObterVeiculoPorPlaca(string placa);
    Task InserirVeiculo(Veiculo veiculo);
    Task AtualizarVeiculo(Veiculo veiculo);
    Task RemoverVeiculo(Veiculo veiculo);
}