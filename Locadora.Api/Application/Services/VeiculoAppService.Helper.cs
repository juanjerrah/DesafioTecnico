using System.Text;
using System.Text.RegularExpressions;

namespace Locadora.Api.Application.Services;

public partial class VeiculoAppService
{
    /// <summary>
    ///     Verifica se a placa é válida
    /// </summary>
    /// <param name="placa">Placa a ser validada</param>
    /// <returns>True ou False conforme a validade da placa</returns>
    public static bool ValidarPlaca(string placa)
    {
        var regexPattern = @"^[A-z]{3}\d[A-j0-9]\d{2}$";

        return Regex.IsMatch(placa, regexPattern);
    }
    
    /// <summary>
    ///     Converte placa para o novo modelo mercosul
    /// </summary>
    /// <remarks>
    /// Modelo antigo: XXX0000
    /// Modelo novo: XXX0X00
    /// </remarks>
    /// <param name="placa">Placa que deseja alterar</param>
    /// <returns>Placa no novo modelo</returns>
    public string ConverterPlacaMercosul(string placa)
    {
        if (!ValidarPlaca(placa))
        {
            _bus.RaiseValidationError($"Placa {placa} inválida.", StatusCodes.Status400BadRequest);
            return String.Empty;
        }
        
        var placaMercosul = new StringBuilder(placa.ToUpper());
            
        placaMercosul[4] = Convert.ToChar(placaMercosul[4].ToString()
            .Replace('0', 'A')
            .Replace('1', 'B')
            .Replace('2', 'C')
            .Replace('3', 'D')
            .Replace('4', 'E')
            .Replace('5', 'F')
            .Replace('6', 'G')
            .Replace('7', 'H')
            .Replace('8', 'I')
            .Replace('9', 'J')
        );

        return placaMercosul.ToString();
    }
}