using Locadora.Api.Application.ViewModels.Movimentacao;

namespace Locadora.Api.Application.Interfaces;

public interface IMovimentacoesAppService
{
    Task InserirMovimentacao(InserirMovimentacaoRequest movimentacaoRequest);
}