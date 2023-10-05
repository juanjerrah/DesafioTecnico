using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Domain.Entities;

public class Veiculo : Entity<Veiculo>
{
    public string Placa { get; private set; }
    public ETiposVeiculos TipoVeiculo { get; private set; }
    public EStatusVeiculo StatusVeiculo { get; private set; }
    public IEnumerable<MovimentacoesVeiculo> MovimentacaoVeiculo { get; private set; }

    public Veiculo(){}
    
    public Veiculo(Guid id, string placa, ETiposVeiculos tipoVeiculo, EStatusVeiculo statusVeiculo)
    {
        Id = id;
        Placa = placa;
        TipoVeiculo = tipoVeiculo;
        StatusVeiculo = statusVeiculo;
    }

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}