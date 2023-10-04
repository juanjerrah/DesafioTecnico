using Locadora.Api.Domain.Bus;

namespace Locadora.Api.Domain.Interfaces;

public interface IMessageBus
{
    ValidationError? GetValidationError();
    void RaiseValidationError(string message, int statusCode);
}