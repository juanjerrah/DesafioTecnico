using Locadora.Api.Application.Interfaces;
using Locadora.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Presentation;

[Route("api/Locadora/Veiculo")]
public class VeiculoController : CoreController
{
    private readonly IVeiculoAppService _appService;
    
    public VeiculoController(IMessageBus messageBus, IVeiculoAppService appService) : base(messageBus)
    {
        _appService = appService;
    }

    /// <summary>
    ///     Obtém todos os veículos da base
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ObterVeiculos()
    {
        var veiculos = await _appService.ObterVeiculos();

        return Response(veiculos);
    }

}