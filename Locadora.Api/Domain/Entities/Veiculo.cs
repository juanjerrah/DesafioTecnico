using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Domain.Entities;

public class Veiculo : Entity<Veiculo>
{
    public string Placa { get; private set; }
    public TiposVeiculos TipoVeiculo { get; private set; }
    public StatusVeiculo StatusVeiculo { get; private set; }

    public Veiculo(){}
    
    public Veiculo(string placa, TiposVeiculos tipoVeiculo, StatusVeiculo statusVeiculo)
    {
        Placa = placa;
        TipoVeiculo = tipoVeiculo;
        StatusVeiculo = statusVeiculo;
    }

    public Veiculo(Guid id, string placa, TiposVeiculos tipoVeiculo, StatusVeiculo statusVeiculo)
    : this(placa, tipoVeiculo, statusVeiculo) 
        => Id = id;

    public override bool IsValid()
    {
        throw new NotImplementedException();
    }
}