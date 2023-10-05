using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;

namespace Locadora.Api.Application.ViewModels.Movimentacao;

public class MovimentacaoResponse
{
    public string Descricao { get;  set; }
    public string MovimentacaoVeiculo { get;  set; }
}