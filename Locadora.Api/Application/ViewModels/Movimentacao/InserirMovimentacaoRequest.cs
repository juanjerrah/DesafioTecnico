using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels.Movimentacao;

public class InserirMovimentacaoRequest
{
    public string Descricao { get; set; }
    public EMovimentacaoVeiculo MovimentacaoVeiculo { get; set; }
    public Guid VeiculoId { get; set; }
}