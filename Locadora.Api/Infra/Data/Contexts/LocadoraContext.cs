using Locadora.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Api.Infra.Data.Contexts;

public class LocadoraContext : DbContext
{
    public LocadoraContext(DbContextOptions<LocadoraContext> options): base(options) { }

    public DbSet<Veiculo> Veiculos { get; set; } = default!;
    public DbSet<MovimentacoesVeiculo> MovimentacoesVeiculos { get; set; } = default!;
    
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocadoraContext).Assembly);
    }*/
}