using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels;

public class VeiculoFiltroRequest
{
    public Guid? VeiculoId { get; set; }
    public string Placa { get; set; }
    public ETiposVeiculos? TipoVeiculo { get; set; }
    public EStatusVeiculo? StatusVeiculo { get; set; }
}