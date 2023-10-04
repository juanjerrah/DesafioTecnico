using System.Net;
using Locadora.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Presentation;

public class CoreController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    public CoreController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    protected new IActionResult Response<T>(T? data, int successStatusCode = StatusCodes.Status200OK)
    {
        var validationError = _messageBus.GetValidationError();

        if (string.IsNullOrEmpty(validationError?.Mensagem))
            return new ObjectResult(new ResponseBodySuccess<T?>(data, HttpStatusCode.OK))
            {
                StatusCode = successStatusCode
            };

        return new ObjectResult(new ResponseBodyFailure(validationError.Mensagem, HttpStatusCode.BadRequest))
        {
            StatusCode = validationError.StatusCode
        };
    }
}