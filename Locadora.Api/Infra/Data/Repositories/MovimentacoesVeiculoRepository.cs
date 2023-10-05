using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;
using Locadora.Api.Infra.Data.Contexts;

namespace Locadora.Api.Infra.Data.Repositories;

public class MovimentacoesVeiculoRepository : IMovimentacoesVeiculoRepository
{
    private readonly LocadoraContext _context;

    public MovimentacoesVeiculoRepository(LocadoraContext context)
    {
        _context = context;
    }

    public async Task InserirMovimentacaoVeiculo(MovimentacoesVeiculo movimentacoesVeiculo)
    {
        movimentacoesVeiculo.SetDateInc(DateTimeOffset.UtcNow);
        movimentacoesVeiculo.SetDateAlter(DateTimeOffset.UtcNow);
        await _context.MovimentacoesVeiculos.AddAsync(movimentacoesVeiculo);
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<MovimentacoesVeiculo>> ObterEventosVeiculo(Guid veiculoId)
    {
        throw new NotImplementedException();
    }
}