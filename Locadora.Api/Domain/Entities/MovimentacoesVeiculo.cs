using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Domain.Entities;

public class MovimentacoesVeiculo : Entity<MovimentacoesVeiculo>
{
    public string Descricao { get; private set; }
    public EMovimentacaoVeiculo MovimentacaoVeiculo { get; private set; }
    public Guid VeiculoId { get; private set; }
    public Veiculo Veiculo { get; set; }

    public MovimentacoesVeiculo() { }
    public MovimentacoesVeiculo(string descricao, EMovimentacaoVeiculo movimentacaoVeiculo, 
        Guid veiculoId)
    {
        Descricao = descricao;
        MovimentacaoVeiculo = movimentacaoVeiculo;
        VeiculoId = veiculoId;
    }
    public MovimentacoesVeiculo(Guid id, string descricao, EMovimentacaoVeiculo movimentacaoVeiculo, 
        Guid veiculoId) : this(descricao, movimentacaoVeiculo, veiculoId)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}