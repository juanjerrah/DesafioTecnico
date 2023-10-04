using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels;

public class InserirVeiculoRequest
{
    public string Placa { get; set; }
    public ETiposVeiculos ETipoVeiculo { get; set; }
    public EStatusVeiculo EStatusVeiculo { get; set; }
}