using Locadora.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locadora.Api.Infra.Data.EntityMappings;

public class MovimentacoesVeiculoMappingConfiguration : IEntityTypeConfiguration<MovimentacoesVeiculo>
{
    public void Configure(EntityTypeBuilder<MovimentacoesVeiculo> builder)
    {
        builder.ToTable("Mov_MovimentacoesVeiculo", "Locadora");

        builder.HasKey(v => v.Id)
            .HasName("PK_Mov_MovimentacoesVeiculoId");
        
        builder.Property(v => v.Id)
            .HasColumnName("Mov_MovimentacoesVeiculoId")
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasColumnName("Mov_Descricao")
            .IsRequired();

        builder.Property(x => x.VeiculoId)
            .HasColumnName("Vei_VeiculoId");

        builder.HasOne(m => m.Veiculo)
            .WithMany(v => v.MovimentacaoVeiculo)
            .HasForeignKey(m => m.VeiculoId);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.CascadeMode);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);

    }
}