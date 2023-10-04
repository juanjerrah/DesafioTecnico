using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels;

public class VeiculoResponse
{
    public Guid Id { get; set; }
    public string Placa { get; set; }
    public ETiposVeiculos ETipoVeiculo { get; set; }
    public EStatusVeiculo EStatusVeiculo { get; set; }
    public Guid MovimentacaoVeiculoId { get; set; }
}