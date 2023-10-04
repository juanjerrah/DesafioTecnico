using Locadora.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Api.Infra.Data.EntityMappings;

public class VeiculoMappingConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("Vei_Veiculo", "Locadora");

        builder.HasKey(v => v.Id)
            .HasName("PK_Vei_Veiculo");

        builder.Property(x => x.Id)
            .HasColumnName("Vei_VeiculoId");
        
        builder.Property(v => v.Placa)
            .HasColumnName("Vei_Placa")
            .IsRequired();

        builder.Property(v => v.StatusVeiculo)
            .HasColumnName("Vei_Status")
            .IsRequired();
        
        builder.Property(v => v.TipoVeiculo)
            .HasColumnName("Vei_Tipo")
            .IsRequired();

        builder.HasOne(x => x.MovimentacoesVeiculo)
            .WithMany(x => x.Veiculos)
            .HasForeignKey(x => x.MovimentacoesVeiculo);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.CascadeMode);
    }
}