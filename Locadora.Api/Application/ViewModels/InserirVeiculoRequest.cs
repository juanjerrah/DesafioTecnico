using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels;

public class InserirVeiculoRequest
{
    public string Placa { get; set; }

    /// <summary>
    /// Hatch = 1
    /// Sedan = 2
    /// Suv = 3
    /// Coupe = 4
    /// Pickup = 5
    /// </summary>
    public ETiposVeiculos TipoDoVeiculo { get; set; }

    /// <summary>
    /// Disponivel = 1,
    /// Locado = 2
    /// </summary>
    public EStatusVeiculo StatusDoVeiculo { get; set; }
}