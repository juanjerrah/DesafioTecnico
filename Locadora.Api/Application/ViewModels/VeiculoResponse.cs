using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels;

public class VeiculoResponse
{
    public Guid Id { get; set; }
    public string Placa { get; set; }
    public string ETipoVeiculo { get; set; }
    public string EStatusVeiculo { get; set; }
}