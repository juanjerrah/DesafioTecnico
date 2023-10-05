using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;
using Locadora.Api.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<MovimentacoesVeiculo>> ObterEventosVeiculo(Guid veiculoId)
    {
        var query = await _context.MovimentacoesVeiculos
            .Include(x => x.Veiculo)
            .Where(x => x.VeiculoId.Equals(veiculoId)).ToListAsync();
        return query;
    }
}