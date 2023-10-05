using System.Linq.Expressions;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Interfaces;
using Locadora.Api.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Infra.Data.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly LocadoraContext _context;

    public VeiculoRepository(LocadoraContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Veiculo>> ObterVeiculos(Expression<Func<Veiculo, bool>> predicate)
    {
        var veiculos =  await _context.Veiculos
            .Where(predicate)
            .Include(x => x.MovimentacaoVeiculo)
            .ToListAsync();
        
        return veiculos;
    }

    public async Task<Veiculo?> ObterVeiculoPorPlaca(string placa)
    {
        var veiculo = await _context.Veiculos
            .Include(x => x.MovimentacaoVeiculo)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Placa.ToLower().Equals(placa.ToLower()));

        return veiculo;
    }

    public async Task InserirVeiculo(Veiculo veiculo)
    {
        veiculo.SetDateInc(DateTimeOffset.UtcNow);
        veiculo.SetDateAlter(DateTimeOffset.UtcNow);
        await _context.Veiculos.AddAsync(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarVeiculo(Veiculo veiculo)
    {
        veiculo.SetDateAlter(DateTimeOffset.UtcNow);
        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverVeiculo(Veiculo veiculo)
    {
        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync();
    }
}