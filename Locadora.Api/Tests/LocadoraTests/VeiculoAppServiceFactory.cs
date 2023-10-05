using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Tests.LocadoraTests;

public class VeiculoAppServiceFactory
{
    public Veiculo GerarVeiculo(Guid? id = null, string? placa = null, ETiposVeiculos? tipoVeiculo = null, 
        EStatusVeiculo? statusVeiculo = null)
    {
        return new Veiculo(id ?? Guid.NewGuid(), placa ?? "qwe1234", tipoVeiculo ?? ETiposVeiculos.Suv, 
            statusVeiculo ?? EStatusVeiculo.Disponivel);
    }
}