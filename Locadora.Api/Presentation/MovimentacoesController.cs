using Locadora.Api.Application.Interfaces;
using Locadora.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Presentation;

[Route("api/Locadora/Movimentacao")]
public class MovimentacoesController : CoreController
{
    private readonly IMovimentacoesAppService _appService;
    
    public MovimentacoesController(IMessageBus messageBus, IMovimentacoesAppService appService) : base(messageBus)
    {
        _appService = appService;
    }

    [HttpGet]
    [Route("MovimentacoesVeiculo")]
    public async Task<IActionResult> ObterMovimentacoesVeiculo([FromQuery] Guid veiculoId)
    {
        var movimentacoes = await _appService.ObterMovimentacoesDoVeiculo(veiculoId);

        return Response(movimentacoes);
    }
}