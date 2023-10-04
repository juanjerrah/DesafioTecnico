using Locadora.Api.Domain.Interfaces;

namespace Locadora.Api.Domain.Bus;

public class MessageBus : IMessageBus
{
    private ValidationError? ValidationErrors { get; set; }
    
    public ValidationError? GetValidationError()
    {
        ValidationErrors ??= new ValidationError(string.Empty, 400);
        return ValidationErrors;
    }

    public void RaiseValidationError(string message, int statusCode)
    {
        ValidationErrors ??= new ValidationError(message, statusCode);
    }
}